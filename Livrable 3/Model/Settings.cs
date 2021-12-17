using System;
using System.Collections.Generic;
using System.Text;

namespace Projet.Model
{
    class Settings
    {
        public static string filePathCryptExtensions = @"..\..\..\extensions.json";
        public static string filePathPriorityExtensions = @"..\..\..\extensionsPriority.json";
        public string extensionsAccepted { get; set; }
    }
}