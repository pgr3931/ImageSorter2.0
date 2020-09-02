using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using ImageSorter2._0.Model;
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

        public static List<DirectoryModel> Load()
        {
            var dirs = new List<DirectoryModel>();
            var oldDirs = new List<DefaultDirectoryModel>();

            try
            {
                var path = IOManager.ReadSetting("SaveFilePath") + "\\imageSorterSave.xml";

                if (!File.Exists(path)) return dirs;

                using (var read = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var xs = new XmlSerializer(oldDirs.GetType());
                    oldDirs = (List<DefaultDirectoryModel>) xs.Deserialize(read);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the directories.\n" + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            dirs.AddRange(oldDirs.Select(defaultDirectoryModel => new DirectoryModel(defaultDirectoryModel)));

            return dirs;
        }


        public static void Save(List<DirectoryModel> list)
        {
            try
            {
                var oldList = list.Select(directoryModel => new DefaultDirectoryModel(directoryModel)).ToList();
                var path = IOManager.ReadSetting("SaveFilePath") + "\\imageSorterSave.xml";
                if (path == "\\imageSorterSave.xml") return;

                using (TextWriter writer = new StreamWriter(path))
                {
                    var sr = new XmlSerializer(oldList.GetType());
                    sr.Serialize(writer, oldList);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while saving the directories.\n" + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}