using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ImageSorter2._0.Annotations;

namespace ImageSorter2._0.ViewModel
{
    public class InitialSaveViewModel : INotifyPropertyChanged
    {
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
                        },
                        (x) => true));
            }
        }
        
        private RelayCommand _addInitialSave;

        public RelayCommand AddInitialSave
        {
            get
            {
                return _addInitialSave ?? (
                    _addInitialSave = new RelayCommand(
                        (x) =>
                        {
                            IOManager.AddUpdateAppSettings("SaveFilePath", DirPath);
                        },
                        (x) => !string.IsNullOrWhiteSpace(DirPath)));
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