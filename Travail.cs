using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    class Travail
    {
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
        public void addWork()
        {
            var filePath = @"C:\Users\deada\source\repos\Projet\state.json";
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
                    TotalFilesToCopy= "0",
                    TotalFilesSize= "0",
                    NbFilesLeftToDo= "0",
                    Progression= "0"

                });

                string strResultJson = JsonConvert.SerializeObject(@stateList);
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

        }
    }
}
