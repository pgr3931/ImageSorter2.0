using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageSorter2._0.Annotations;
using ImageSorter2._0.Model;

namespace ImageSorter2._0.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ICommand> _history;
        private ILogic _logic;

        private string _imagePath;

        public string ImageSource
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
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

                            SetPage();
                            ImageSource = _logic.Images[_logic.CurrentImage];
                        },
                        (x) => true));
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

                            SetPage();
                            ImageSource = _logic.Images[_logic.CurrentImage];
                        },
                        (x) => true));
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
            _history = new List<ICommand>();
            _logic = logic;
            _logic.Path = @"D:\Bilder\Anime\ImageDump";
            logic.LoadImages();
            SetPage();
            ImageSource = _logic.Images[_logic.CurrentImage];
        }

        private void SetPage()
        {
            Page = $"{(_logic.CurrentImage + 1)}/{_logic.Images.Count}";
        }
    }
}