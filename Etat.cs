using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    class Etat
    {
        public static string filePath = @"..\..\..\state.json";

        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string Time { get; set; }
        public string State { get; set; }
        public string TotalFilesToCopy { get; set; }
        public string TotalFilesSize { get; set; }
        public string NbFilesLeftToDo { get; set; }
        public string Progression { get; set; }
    }
}