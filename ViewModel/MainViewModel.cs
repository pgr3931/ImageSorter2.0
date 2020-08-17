using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageSorter2._0.Annotations;
using ImageSorter2._0.Model;
using Microsoft.VisualBasic.FileIO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ImageSorter2._0.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private List<RelayCommand> _history;
        private readonly ILogic _logic;

        private string _dirPath;

        public string DirPath
        {
            get => _dirPath;
            set
            {
                _dirPath = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        private string _page;

        public string Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged();
            }
        }

        private string _imageName;

        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _nextCommand;

        public RelayCommand NextCommand
        {
            get
            {
                return _nextCommand ?? (
                    _nextCommand = new RelayCommand(
                        (x) =>
                        {
                            _logic.CurrentImage++;
                            if (_logic.CurrentImage == _logic.Images.Count)
                            {
                                _logic.CurrentImage = 0;
                            }

                            SetMetaInfos();
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.UriSource = new Uri(_logic.Images[_logic.CurrentImage]);
                            image.EndInit();
                            ImageSource = image;
                        },
                        (x) => _logic.Images.Count > 1));
            }
        }

        private RelayCommand _prevCommand;

        public RelayCommand PrevCommand
        {
            get
            {
                return _prevCommand ?? (
                    _prevCommand = new RelayCommand(
                        (x) =>
                        {
                            _logic.CurrentImage--;
                            if (_logic.CurrentImage == -1)
                            {
                                _logic.CurrentImage = _logic.Images.Count - 1;
                            }

                            SetMetaInfos();
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.UriSource = new Uri(_logic.Images[_logic.CurrentImage]);
                            image.EndInit();
                            ImageSource = image;
                        },
                        (x) => _logic.Images.Count > 1));
            }
        }

        private RelayCommand _chooseDirCommand;

        public RelayCommand ChooseDirCommand
        {
            get
            {
                return _chooseDirCommand ?? (
                    _chooseDirCommand = new RelayCommand(
                        (x) =>
                        {
                            var dialog = new CommonOpenFileDialog
                            {
                                InitialDirectory = _logic.Path, IsFolderPicker = true
                            };
                            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                            {
                                _logic.Path = dialog.FileName;
                                _logic.LoadImages();
                                _logic.CurrentImage = 0;
                                DirPath = _logic.Path;
                                SetMetaInfos();
                                if (_logic.Images.Count > 0)
                                {
                                    var image = new BitmapImage();
                                    image.BeginInit();
                                    image.CacheOption = BitmapCacheOption.OnLoad;
                                    image.UriSource = new Uri(_logic.Images[_logic.CurrentImage]);
                                    image.EndInit();
                                    ImageSource = image;
                                }
                                else
                                {
                                    ImageSource = null;
                                }
                            }
                        },
                        (x) => true));
            }
        }

        private RelayCommand _deleteFileCommand;

        public RelayCommand DeleteFileCommand
        {
            get
            {
                return _deleteFileCommand ?? (
                    _deleteFileCommand = new RelayCommand(
                        (x) =>
                        {
                            try
                            {
                                FileSystem.DeleteFile(_logic.Images[_logic.CurrentImage], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                _logic.Images.RemoveAt(_logic.CurrentImage);
                                if (_logic.CurrentImage == _logic.Images.Count)
                                {
                                    _logic.CurrentImage--;
                                }

                                if (_logic.Images.Count > 0)
                                {
                                     var image = new BitmapImage();
                                    image.BeginInit();
                                    image.CacheOption = BitmapCacheOption.OnLoad;
                                    image.UriSource = new Uri(_logic.Images[_logic.CurrentImage]);
                                    image.EndInit();
                                    ImageSource = image;
                                }
                                else
                                {
                                    ImageSource = null;
                                }

                                SetMetaInfos();
                            }
                            catch (IOException)
                            {
                                //TODO add dialog maybe
                                Console.WriteLine("IO Exception");
                            }
                            catch (OperationCanceledException)
                            {
                                //TODO add dialog maybe
                            }
                        },
                        (x) => _logic.Images.Count > 0));
            }
        }
        
        private RelayCommand _undoCommand;

        public RelayCommand UndoCommand
        {
            get
            {
                return _undoCommand ?? (
                    _undoCommand = new RelayCommand(
                        (x) =>
                        {
                            _history.Last().Undo(null);
                            _history.RemoveAt(_history.Count - 1);
                        },
                        (x) => _history.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel(ILogic logic)
        {
            _history = new List<RelayCommand>();
            _logic = logic;
            _logic.Path = IOManager.ReadSetting("DefaultPath");
            DirPath = _logic.Path;
            logic.LoadImages();
            SetMetaInfos();
            if (_logic.Images.Count > 0)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_logic.Images[_logic.CurrentImage]);
                image.EndInit();
                ImageSource = image;
            }
        }

        private void SetPage()
        {
            if (_logic.Images.Count > 0)
            {
                Page = $"{(_logic.CurrentImage + 1)}/{_logic.Images.Count}";
            }
            else
            {
                Page = "0/0";
            }
        }

        private void SetImageName()
        {
            if (_logic.Images.Count > 0)
            {
                ImageName = _logic.Images[_logic.CurrentImage].Split('\\').Last().Split('.')
                    .First();
            }
        }

        private void SetMetaInfos()
        {
            SetPage();
            SetImageName();
        }
    }
}