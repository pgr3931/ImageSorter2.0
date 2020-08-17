using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ImageSorter2._0.Model
{
    public interface ILogic
    {
        string Path { get; set; }
        List<string> Images { get; set; }
        int CurrentImage { get; set; }
        void LoadImages();
        void OpenDir();
        void Next();
        void Prev();
        void Delete();
        void Undo();
    }
}