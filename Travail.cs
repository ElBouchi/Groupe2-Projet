﻿using Newtonsoft.Json;
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
        public void ExecuteWork()
        {
            var jsonData = File.ReadAllText(filePath);
            var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) 
                ?? new List<Etat>();
            ConsoleTable.From(stateList).Write();
            Console.WriteLine("Souhaitez vous executer ?    1-oui     2-non");
            string reponse = Console.ReadLine();
            if(reponse == "1")
            {
                string sourceDir = stateList.ElementAt(0).SourceFilePath;
                
                string backupDir = stateList.ElementAt(0).TargetFilePath;

                try
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
                }

            }
            else if (reponse == "2")
            {
                
            }
            else
            {
                Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2>\n");
            }
            
            
        }
    }
}