using System.Windows;
using System.Windows.Input;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    public partial class InitialSave : Window
    {
        public InitialSave()
        {
            InitializeComponent();
            DataContext = new InitialSaveViewModel();
        }
        
        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }
    }
}