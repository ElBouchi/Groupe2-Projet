using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    class Log
    {
        public static string filePath = @"..\..\..\log.json";

        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string FileSize { get; set; }
        public string FileTransferTime { get; set; }
        public string time { get; set; }

    }
}