using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ImageSorter2._0.Model;
using ImageSorter2._0.ViewModel;

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

        private new void Drop(object sender, DragEventArgs e)
        {
            var dirSource = e.Data.GetData("DirSource");
            if (dirSource != null)
            {
                var newIndex = ((DirectoryModel) ((Directory) sender).DataContext).Index;
                if (dirSource is DirectoryModel dir && newIndex != dir.Index)
                {
                    if (Directories.ItemsSource is ObservableCollection<DirectoryModel> list)
                    {
                        list.RemoveAt(dir.Index);
                        list.Insert(newIndex, dir);
                        for (var i = Math.Min(newIndex, dir.Index); i <= Math.Max(Math.Max(newIndex, dir.Index), list.Count - 1); i++)
                        {
                            list[i].Index = i;
                        }
                        IOUtils.Save(list.ToList());
                    }
                }
            }
        }

        private new void PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is Menu menu && menu.Cursor == Cursors.SizeAll)
                {
                    var data = new DataObject();
                    data.SetData("DirSource", menu.DataContext);
                    DragDrop.DoDragDrop(menu, data, DragDropEffects.Move);
                    e.Handled = true;
                }
            }
        }
    }
}