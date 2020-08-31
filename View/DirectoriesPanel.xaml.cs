using System.Windows;
using System.Windows.Controls;

namespace ImageSorter2._0.View
{
    /// <summary>
    /// Interaction logic for Directory.xaml
    /// </summary>
    public partial class DirectoriesPanel : UserControl
    {
        public DirectoriesPanel()
        {
            InitializeComponent();
        }
       
        private void OpenAddDir(object sender, RoutedEventArgs e)
        {
            var modalWindow = new AddDirectory {Owner = Window.GetWindow(Parent)};
            modalWindow.ShowDialog();
        }
        
    }
}
