using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace Projet
{
    class Travail : Etat
    {
        public Travail()
        { }
        public Travail(string theName, string therepS, string therepC, string theType)
        {
            name = theName;
            repS = therepS;
            repC = therepC;
            type = theType;
        }
        private string name { get; set; }
        private string repS { get; set; }
        private string repC { get; set; }
        private string type { get; set; }
        public void addWork(long filesize, int countfile)
        {
            var jsonData = File.ReadAllText(filePath);
            var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData)
                      ?? new List<Etat>();
            if (stateList.Count < 5)
            {
                stateList.Add(new Etat()
                {
                    Name = name,
                    SourceFilePath = repS,
                    TargetFilePath = repC,
                    Type = type,
                    State = "INACTIVE",
                    TotalFilesToCopy = countfile.ToString(),
                    TotalFilesSize = filesize.ToString(),
                    NbFilesLeftToDo = "0",
                    Progression = "0"

                });

                string strResultJson = JsonConvert.SerializeObject(stateList);
                File.WriteAllText(filePath, strResultJson);

                Console.WriteLine("Travail ajouté avec succès !\n");
            }
            else
            {
                Console.WriteLine("Nombre maximal de travaux atteint\n");
            }
        }
        public void displayWorks()
        {
            var jsonData = File.ReadAllText(filePath);
            var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) ?? new List<Etat>();
            ConsoleTable.From(stateList).Write();
        }
        public long GetFileSizeSumFromDirectory(string searchDirectory)
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // get the sizeof all files in the current directory
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // get the size of all files in all subdirectories
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();

            return currentSize + subDirSize;
        }
    }
}