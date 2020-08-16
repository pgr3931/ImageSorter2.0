using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            List<TodoItem> items = new List<TodoItem>();
            for (var i = 0; i < 20; i++)
            {
                items.Add(new TodoItem()
                    {Title = "Anime", Completion = 45});
            }

            Directories.ItemsSource = items;
        }
        public class TodoItem
        {
            public string Title { get; set; }
            public int Completion { get; set; }
        }
    }
}
