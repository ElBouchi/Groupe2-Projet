using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace Projet
{
    class EasySave
    {
        // a method that will allow to create a backupwork
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Travail.filePath); //Read the JSON file
            var workList = JsonConvert.DeserializeObject<List<Travail>>(jsonDataWork) ?? new List<Travail>(); //convert a string into an object for JSON

            if (workList.Count < 5) // this condition limits the number of backupwork that can be created (5 max)
            {
                workList.Add(new Travail() //parameter that the JSON file will contains
                {
                    name = theName,
                    repS = theRepS,
                    repC = theRepC,
                    type = theType,
                });


                string strResultJsonWork = JsonConvert.SerializeObject(workList, Formatting.Indented); //convert an object into a string for JSON
                File.WriteAllText(Travail.filePath, strResultJsonWork); // write in the JSON file

                var jsonDataState = File.ReadAllText(Etat.filePath); //Read the JSON file
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>(); //convert a string into an object for JSON


                stateList.Add(new Etat() //parameter that the JSON file will contains
                {
                    Name = theName,
                    SourceFilePath = theRepC,
                    TargetFilePath = theRepS,
                    Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    State = "INACTIVE",
                    TotalFilesToCopy = countfile.ToString(),
                    TotalFilesSize = filesize.ToString(),
                    NbFilesLeftToDo = "0",
                    Progression = "0"
                });

                string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented); //convert an object into a string for JSON
                File.WriteAllText(Etat.filePath, strResultJsonState); // write in the JSON file

                Console.WriteLine("Travail ajouté avec succès !\n");
            }
            else
            {
                Console.WriteLine("Nombre maximal de travaux atteint\n");
            }
        }
        public void displayWorks() // a method that will allow to display all our backupwork
        {
            var jsonData = File.ReadAllText(Travail.filePath); //Read the JSON file
            var stateList = JsonConvert.DeserializeObject<List<Travail>>(jsonData) ?? new List<Travail>(); //convert a string into an object for JSON

            var dt = new ConsoleTable("index", "Name", "SourceFilePath", "TargetFilePath", "Type");
            foreach (var (state, i) in stateList.Select((el, i) => (el, i)))
            {
                dt.AddRow(i + 1, state.name, state.repS, state.repC, state.type);
            }

            dt.Write();
        }
        public void ExecuteWork(string inputUtilisateur) // a method that will allow to execute a backupwork created
        {
            var jsonData = File.ReadAllText(Travail.filePath); //Read the JSON file
            var workList = JsonConvert.DeserializeObject<List<Travail>>(jsonData) ?? new List<Travail>(); //convert a string into an object for JSON

            if (workList.Count >= Convert.ToInt32(inputUtilisateur)) //this condition allow to the user to choose the exact row in order to execute the backupwork chosen
            {
                int index = Convert.ToInt32(inputUtilisateur) - 1;
                string sourceDir = workList.ElementAt(index).repS;
                string backupDir = workList.ElementAt(index).repC;
                string name = workList.ElementAt(index).name;
                long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                //this condition is used to execute the type of backup chosen in the creation 
                if (workList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).type == "Differentielle") 
                {
                    // differential backup
                    SauvegardeDifferentielle SD = new SauvegardeDifferentielle(); 
                    SD.Sauvegarde(sourceDir, backupDir, true, true, filesNum, index, name);

                }
                else
                {
                    // complete backup
                    SauvegardeComplete SD = new SauvegardeComplete();
                    SD.Sauvegarde(sourceDir, backupDir, true, true, filesNum, index, name);

                }

            }
            else
            {
                Console.WriteLine("Aucun travail de sauvegarde avec l'entrée " + inputUtilisateur + " trouvée\n");
            }
        }
        public long GetFileSizeSumFromDirectory(string searchDirectory) //a method that allow to calculate the size of a directory (subdirrectory included)
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