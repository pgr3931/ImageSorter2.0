using System.Collections.Generic;
using System.IO;

namespace ImageSorter2._0.Model
{
    public class MainLogic : ILogic
    {
        public string Path { get; set; }
        public List<string> Images { get; set; }
        public int CurrentImage { get; set; }

        public MainLogic()
        {
            Images = new List<string>();
            CurrentImage = 0;
        }

        public void LoadImages()
        {
            if (Directory.Exists(Path))
            {
                Images.Clear();
                var files = Directory.GetFiles(Path);
                foreach (var f in files)
                {
                    if (System.Web.MimeMapping.GetMimeMapping(f).StartsWith("image/"))
                    {
                        Images.Add(f);
                    }
                }
            }
        }

        public void OpenDir()
        {
            throw new System.NotImplementedException();
        }

        public void Next()
        {
            throw new System.NotImplementedException();
        }

        public void Prev()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}