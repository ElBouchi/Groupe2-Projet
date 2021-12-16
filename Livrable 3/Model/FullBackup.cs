using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Threading;

namespace Projet.Model
{
    class FullBackup : IBackup
    {
        Stopwatch crpytTimer = new Stopwatch();
        Stopwatch stopWatchTimer = new Stopwatch();
        private static Object _locker = new Object();
        // a method that will be used for the full backup
        public void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName)
        {
            stopWatchTimer.Start();
            Etat state = new Etat();
            Log log = new Log();
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourcePATH);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourcePATH);
            }
            //condition in order to create the source subdirectories in the destination directory
            if (copyDirs)
            {
                CreateDirs(destPATH, dir.GetDirectories());
            }

            FileInfo[] files = copyDirs ? dir.GetFiles("*", SearchOption.AllDirectories) : dir.GetFiles();
            var i = 0;

            var json = File.ReadAllText(Settings.filePath);
            var List = JsonConvert.DeserializeObject<List<Settings>>(json) ?? new List<Settings>();
            string[] extensions = new string[] { List[0].extensionsAccepted };
            extensions = extensions[0].Split(',', ' ');
            TimeSpan TimeToCrypt = TimeSpan.Zero;

                foreach (var file in files)
                {
                    if (extensions.Contains(file.Extension))
                    {
                    var p = new Process();
                    p.StartInfo.FileName = @"..\..\..\CryptoSoft\CryptoSoft.exe";
                    p.StartInfo.Arguments = $"{file.FullName} {file.FullName.Replace(sourcePATH, destPATH)}";

                    crpytTimer.Start();
                    p.Start();
                    crpytTimer.Stop();
                }
                else
                {
                    lock (_locker)
                    {
                        file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true); //Copies an existing file to a new file.
                    }

                    stopWatchTimer.Stop();
                }
                i++;
                int filesLeftToDo = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - i;
                string progress = Convert.ToString((100 - (filesLeftToDo * 100) / fileCount)) + "%";

                List<Etat> stateList = state.readOnlyState();


                stateList[getStateIndex].NbFilesLeftToDo = filesLeftToDo.ToString();
                stateList[getStateIndex].Progression = progress;


                state.writeOnlyState(stateList);

                string theTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                log.writeLog(getName, file.FullName, file.FullName.Replace(sourcePATH, destPATH), file.Length.ToString(), stopWatchTimer.Elapsed.ToString(), crpytTimer.Elapsed.ToString(), theTime);
            }
            List<Etat> modifyStateList = state.readOnlyState();

            modifyStateList[getStateIndex].Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            modifyStateList[getStateIndex].State = "END";

            state.writeOnlyState(modifyStateList);
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