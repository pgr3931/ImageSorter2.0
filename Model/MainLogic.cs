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
            if (!Directory.Exists(Path)) return;
            
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

        public void RemoveCurrentImage()
        {
            Images.RemoveAt(CurrentImage);
            if (Images.Count == 1)
            {
                CurrentImage = 0;
            }
            else if(CurrentImage == Images.Count)
            {
                CurrentImage--;
            }
        }

        public string GetCurrentImage()
        {
            return Images[CurrentImage];
        }

        public bool HasImages()
        {
            return Images.Count > 0;
        }
    }
}