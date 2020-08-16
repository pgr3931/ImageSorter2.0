using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageSorter2._0.Annotations;

namespace ImageSorter2._0.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ICommand> history;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        MainViewModel()
        {
            history = new List<ICommand>();
        }
    }
}
