using System;
using System.Collections.Generic;
using System.IO;
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
            var path = IOManager.ReadSetting("SaveFilePath") + "\\imageSorterSave.xml";
            var dirs = new List<DirectoryModel>();

            if (!File.Exists(path)) return dirs;
            try
            {
                using (var read = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var xs = new XmlSerializer(dirs.GetType());
                    dirs = (List<DirectoryModel>) xs.Deserialize(read);
                }
            }
            catch (Exception)
            {
                //TODO display error
            }

            return dirs;
        }


        public static void Save(List<DirectoryModel> list)
        {
            try
            {
                var path = IOManager.ReadSetting("SaveFilePath") + "\\imageSorterSave.xml";
                using (TextWriter writer = new StreamWriter(path))
                {
                    var sr = new XmlSerializer(list.GetType());
                    sr.Serialize(writer, list);
                }
            }
            catch (Exception)
            {
                //TODO display error
            }
        }
    }
}