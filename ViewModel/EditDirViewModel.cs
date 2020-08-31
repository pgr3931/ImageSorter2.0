using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ImageSorter2._0.Annotations;
using ImageSorter2._0.Model;

namespace ImageSorter2._0.ViewModel
{
    public class EditDirViewModel : INotifyPropertyChanged
    {
        private int _index;

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }
        
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

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _shortcut;

        public string Shortcut
        {
            get => _shortcut;
            set
            {
                _shortcut = value;
                OnPropertyChanged();
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
                            DirPath = IOUtils.ChooseDir(DirPath);
                            if (string.IsNullOrEmpty(Name))
                            {
                                Name = DirPath.Split('\\').Last();
                            }
                        },
                        (x) => true));
            }
        }

        private RelayCommand _editDirCommand;

        public RelayCommand EditDirCommand
        {
            get
            {
                return _editDirCommand ?? (
                    _editDirCommand = new RelayCommand(
                        (x) =>
                        {
                            var mainViewModel = (MainViewModel) ((Window) x).Owner.DataContext;
                            
                            if (mainViewModel.Directories.Any(d => d.Path == DirPath && d.Index != Index))
                            {
                                MessageBox.Show("The directory you're trying to add already exists",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            var dir = new DirectoryModel
                            {
                                Name = Name,
                                Path = DirPath,
                                Shortcut = "📂" + (string.IsNullOrEmpty(Shortcut) ? "" : " " + Shortcut),
                                Index = Index
                            };
                            
                            if (mainViewModel.Directories.Any(item => item.Index == Index))
                            {
                                mainViewModel.Directories.RemoveAt(Index);
                                mainViewModel.Directories.Insert(Index, dir);
                                
                                if (!string.IsNullOrEmpty(Shortcut))
                                {
                                    var shortcutArray = Shortcut.Split('+');

                                    Enum.TryParse(shortcutArray.Last(), true, out Key key);
                                    var modifier = ModifierKeys.None;
                                    if (shortcutArray.Length == 2)
                                    {
                                        Enum.TryParse(shortcutArray.First().Replace("Ctrl", "Control"), true,
                                            out modifier);
                                    }

                                    if (modifier != ModifierKeys.None)
                                    {
                                        ((Window) x).Owner.InputBindings.Add(new KeyBinding
                                        {
                                            Command = mainViewModel.MoveCommand,
                                            CommandParameter = Index,
                                            Key = key,
                                            Modifiers = modifier
                                        });
                                    }
                                    else
                                    {
                                        ((Window) x).Owner.InputBindings.Add(new KeyBinding
                                        {
                                            Command = mainViewModel.MoveCommand,
                                            CommandParameter = Index,
                                            Key = key
                                        });
                                    }
                                }
                                
                                IOUtils.Save(mainViewModel.Directories.ToList());
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong while editing the directory",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            ((Window) x).Close();
                        },
                        (x) => !string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_dirPath)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}