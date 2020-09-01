namespace ImageSorter2._0.Model
{
    public class DefaultDirectoryModel
    {
        public DefaultDirectoryModel(){}

        public DefaultDirectoryModel(DirectoryModel model)
        {
            Name = model.Name;
            Path = model.Path;
            Shortcut = model.Shortcut;
            Index = model.Index;
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Shortcut { get; set; }
        public int Index { get; set; }
    }
}