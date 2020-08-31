using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ImageSorter2._0.Model;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    /// <summary>
    /// Interaction logic for Directory.xaml
    /// </summary>
    public partial class Directory : UserControl
    {
        public Directory()
        {
            InitializeComponent();
        }

        private void OpenEditDir(object sender, RoutedEventArgs e)
        {
            var modalWindow =
                new EditDirectory(Window.GetWindow(this)?.DataContext, ((DirectoryModel) DataContext).Index)
                    {Owner = Window.GetWindow(this)};
            modalWindow.ShowDialog();
        }
        
        private void OpenCreateSub(object sender, RoutedEventArgs e)
        {
            var path = ((DirectoryModel) DataContext).Path;
            var name = "Sub" + ((DirectoryModel) DataContext).Path.Split('\\').Last();
            var modalWindow =
                new CreateSubFolder(path, name)
                    {Owner = Window.GetWindow(this)};
            modalWindow.ShowDialog();
        }
    }
}