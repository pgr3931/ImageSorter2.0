using ImageSorter2._0.Model;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0
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
    }
}