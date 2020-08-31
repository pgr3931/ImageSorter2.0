using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    public partial class CreateSubFolder : Window
    {
        public CreateSubFolder(string path, string subName)
        {
            InitializeComponent();
            DataContext = new CreateSubViewModel
            {
                Name = subName,
                DirPath = path
            };
        }
        
        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }
    }
}