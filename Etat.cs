using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    class Etat
    {
        public static string filePath = @"C:\Users\Nazim GAOUA\source\repos\Groupe2-Projett\state.json";

        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string TotalFilesToCopy { get; set; }
        public string TotalFilesSize { get; set; }
        public string NbFilesLeftToDo { get; set; }
        public string Progression { get; set; }
    }
}