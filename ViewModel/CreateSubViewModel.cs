using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using ImageSorter2._0.Annotations;
using ImageSorter2._0.View;
using Directory = System.IO.Directory;

namespace ImageSorter2._0.ViewModel
{
    public class CreateSubViewModel : INotifyPropertyChanged
    {
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

        private string _path;

        public string DirPath
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _createSubCommand;

        public RelayCommand CreateSubCommand
        {
            get
            {
                return _createSubCommand ?? (
                    _createSubCommand = new RelayCommand(
                        (x) =>
                        {
                            Directory.CreateDirectory(Path.Combine(DirPath, Name));
                            var modalWindow =
                                new AddDirectory(new AddDirViewModel {DirPath = Path.Combine(DirPath, Name)})
                                    {Owner = ((Window) x).Owner};
                            modalWindow.ShowDialog();
                            ((Window) x).Close();
                        },
                        (x) => !string.IsNullOrWhiteSpace(Name)));
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