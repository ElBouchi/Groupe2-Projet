using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace Projet
{
    class Travail
    {
        public static string filePath = @"C:\Users\Nazim GAOUA\source\repos\Groupe2-Projett\state.json";
        public string name { get; set; }
        public string repS { get; set; }
        public string repC { get; set; }
        public string type { get; set; }
    }
}