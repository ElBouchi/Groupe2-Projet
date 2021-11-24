using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace Projet
{
    class SauvegardeComplete : ISauvegarde
    {
        public void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, bool createFirstFolder, long fileCount, int getIndex, string getName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourcePATH);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourcePATH);
            }

            if (copyDirs)
            {
                CreateDirs(destPATH, dir.GetDirectories());
            }

            FileInfo[] files = copyDirs ? dir.GetFiles("*", SearchOption.AllDirectories) : dir.GetFiles();
            foreach (var file in files)
            {
                file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true);
                var filesLeftToDo = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - Directory.GetFiles(destPATH, "*", SearchOption.AllDirectories).Length;
                string progress = Convert.ToString((100 - (filesLeftToDo * 100) / fileCount)) + "%";
                var jsonData = File.ReadAllText(Etat.filePath);
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) ?? new List<Etat>();

                stateList[getIndex].NbFilesLeftToDo = filesLeftToDo.ToString();
                stateList[getIndex].Progression = progress;

                string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented);
                File.WriteAllText(Etat.filePath, strResultJsonState);

                Console.Write("Nombre de fichiers restants: " + filesLeftToDo + "\t");
                Console.WriteLine("Progression: " + progress + "\n");


            }
            var jsonDataState2 = File.ReadAllText(Etat.filePath);
            var stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState2) ?? new List<Etat>();

            stateList2[getIndex].Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            stateList2[getIndex].State = "END";

            string strResultJsonState2 = JsonConvert.SerializeObject(stateList2, Formatting.Indented);
            File.WriteAllText(Etat.filePath, strResultJsonState2);

            var jsonDataState3 = File.ReadAllText(Travail.filePath);
            var stateList3 = JsonConvert.DeserializeObject<List<Travail>>(jsonDataState3) ?? new List<Travail>();

            stateList3.Remove(stateList3[getIndex]);

            string strResultJsonState3 = JsonConvert.SerializeObject(stateList3, Formatting.Indented);
            File.WriteAllText(Travail.filePath, strResultJsonState3);
        }
        private void CreateDirs(string path, DirectoryInfo[] dirs)
        {
            foreach (var dir in dirs)
            {

                if (!Directory.Exists(Path.Combine(path, dir.Name)))
                {
                    Directory.CreateDirectory(Path.Combine(path, dir.Name));
                }
                CreateDirs(Path.Combine(path, dir.Name), dir.GetDirectories());
            }
        }
    }
}
