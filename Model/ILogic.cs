﻿using System.Collections.Generic;

 namespace ImageSorter2._0.Model
{
    public interface ILogic
    {
        string Path { get; set; }
        List<string> Images { get; set; }
        int CurrentImage { get; set; }
        void LoadImages();
    }
}