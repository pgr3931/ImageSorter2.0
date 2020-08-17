using System.Windows;
using System.Windows.Input;
using ImageSorter2._0.Model;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            var logic = new MainLogic();
            DataContext = new MainViewModel(logic);
            InitializeComponent();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }
        
        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            var modalWindow = new Settings {Owner = this};
            modalWindow.ShowDialog();
        }
    }
}