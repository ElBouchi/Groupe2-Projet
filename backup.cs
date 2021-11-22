using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace Projet
{
    class Backup
    {
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Travail.filePath);
            var workList = JsonConvert.DeserializeObject<List<Travail>>(jsonDataWork) ?? new List<Travail>();

            if (workList.Count < 5)
            {
                workList.Add(new Travail()
                {
                    name = theName,
                    repS = theRepS,
                    repC = theRepC,
                    type = theType,
                });


                string strResultJsonWork = JsonConvert.SerializeObject(workList);
                File.WriteAllText(Travail.filePath, strResultJsonWork);

                var jsonDataState = File.ReadAllText(Etat.filePath);
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>();

                stateList.Add(new Etat()
                {
                    Name = theName,
                    SourceFilePath = theRepC,
                    TargetFilePath = theRepS,
                    State = "INACTIVE",
                    TotalFilesToCopy = countfile.ToString(),
                    TotalFilesSize = filesize.ToString(),
                    NbFilesLeftToDo = "0",
                    Progression = "0"
                });

                string strResultJsonState = JsonConvert.SerializeObject(stateList);
                File.WriteAllText(Etat.filePath, strResultJsonState);

                Console.WriteLine("Travail ajouté avec succès !\n");
            }
            else
            {
                Console.WriteLine("Nombre maximal de travaux atteint\n");
            }
        }
        public void displayWorks()
        {
            var jsonData = File.ReadAllText(Etat.filePath);
            var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) ?? new List<Etat>();

            var dt = new ConsoleTable("index", "Name", "SourceFilePath", "TargetFilePath", "State", "TotalFilesToCopy", "TotalFilesSize", "NbFilesLeftToDo", "Progression");
            foreach (var (state, i) in stateList.Select((el, i) => (el, i)))
            {
                dt.AddRow(i + 1, state.Name, state.SourceFilePath, state.TargetFilePath, state.State, state.TotalFilesToCopy, state.TotalFilesSize, state.NbFilesLeftToDo, state.Progression);
            }

            dt.Write();

        }
        public void ExecuteWork(string inputUtilisateur)
        {
            var jsonData = File.ReadAllText(Travail.filePath);
            var stateList = JsonConvert.DeserializeObject<List<Travail>>(jsonData) ?? new List<Travail>();

            if (stateList.Count >= Convert.ToInt32(inputUtilisateur))
            {
                string sourceDir = stateList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).repC;
                string backupDir = stateList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).repS;

                if (stateList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).type == "Différentielle")
                {

                }
                else
                {
                    Console.WriteLine("En cours de développement");
                }

            }
            else
            {
                Console.WriteLine("Aucun travail de sauvegarde avec l'entrée " + inputUtilisateur + " trouvée\n");
            }

            /*try
            {
                string[] txtList = Directory.GetFiles(sourceDir);

                // Copy text files.
                foreach (string f in txtList)
                {

                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    // Will not overwrite if the destination file already exists.
                    File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
            DirectoryCopy(sourceDir, backupDir, true);
            static void DirectoryCopy(string src, string dest, bool copySubDir)
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(src);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + src);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();

                // If the destination directory doesn't exist, create it.       
                Directory.CreateDirectory(dest);

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tempPath = Path.Combine(dest, file.Name);
                    file.CopyTo(tempPath, true);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDir)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tempPath = Path.Combine(dest, subdir.Name);
                        DirectoryCopy(subdir.FullName, tempPath, copySubDir);
                    }
                }
            } */
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