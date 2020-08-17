using System.Windows;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    public partial class Settings : Window
    {
        public Settings()
        {
            DataContext = new SettingsViewModel();
            InitializeComponent();
        }
    }
}