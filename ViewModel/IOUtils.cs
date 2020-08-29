using Microsoft.WindowsAPICodePack.Dialogs;

namespace ImageSorter2._0.ViewModel
{
    public class IOUtils
    {
        public static string ChooseDir(string root)
        {
            var dialog = new CommonOpenFileDialog
            {
                InitialDirectory = root, IsFolderPicker = true
            };
            return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : root;
        }
    }
}