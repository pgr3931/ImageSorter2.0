using System.Windows;
using ImageSorter2._0.View;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            var saveFilePath = IOManager.ReadSetting("SaveFilePath");
            if (saveFilePath != "") return;
            
            var modalWindow = new InitialSave() {Owner = mainWindow};
            modalWindow.ShowDialog();
        }
    }
}