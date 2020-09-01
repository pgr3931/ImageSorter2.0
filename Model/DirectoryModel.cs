using System.Windows.Input;

namespace ImageSorter2._0.Model
{
    public class DirectoryModel
    {
        public DirectoryModel(){}

        public DirectoryModel(DefaultDirectoryModel oldModel)
        {
            Name = oldModel.Name;
            Path = oldModel.Path;
            Shortcut = oldModel.Shortcut;
            Index = oldModel.Index;
        }
        
        public string Name { get; set; }
        public string Path { get; set; }
        public string Shortcut { get; set; }
        public KeyBinding KeyBinding { get; set; }
        public int Index { get; set; }
    }
}