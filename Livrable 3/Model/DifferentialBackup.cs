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

    class DifferentialBackup : IBackup
    {
        Stopwatch crpytTimer = new Stopwatch();
        Stopwatch stopWatchTimer = new Stopwatch();
        private static Object _locker = new Object();
        private static Mutex mutex = new Mutex();
        Etat state = new Etat();
        Log log = new Log();
        // a method that will be used for the differential backup
        public void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName)
        {
            //Initialize a timer which determine the backup time
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
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
            files = OrderFiles(files.ToList()).ToArray();
            var i = 0;
            foreach (var file in files)
            {
                if (File.Exists(file.FullName.Replace(sourcePATH, destPATH)))
                {
                    using (var sourcef = File.OpenRead(file.FullName))
                    {
                        // opening the destination PATH
                        using (var destinationf = File.OpenRead(file.FullName.Replace(sourcePATH, destPATH)))
                        {
                            var hash1 = BitConverter.ToString(MD5.Create().ComputeHash(sourcef));
                            var hash2 = BitConverter.ToString(MD5.Create().ComputeHash(destinationf));
                            if (hash1 == hash2)
                            {
                                i++;
                                var filesLeftToDo2 = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - i;
                                string progress2 = Convert.ToString((100 - (filesLeftToDo2 * 100) / fileCount)) + "%";
                                List<Etat> stateList2 = state.readOnlyState();


                                stateList2[getStateIndex].NbFilesLeftToDo = filesLeftToDo2.ToString();
                                stateList2[getStateIndex].Progression = progress2;

                                state.writeOnlyState(stateList2);
                                continue;
                            };
                        }
                    }
                }
                TimeSpan TimeToCrypt = TimeSpan.Zero;

                if (extPrio().Contains(file.Extension))
                {
                    var p = new Process();
                    p.StartInfo.FileName = @"..\..\..\CryptoSoft\CryptoSoft.exe";
                    p.StartInfo.Arguments = $"{file.FullName} {file.FullName.Replace(sourcePATH, destPATH)}";

                    crpytTimer.Start();

                    if (file.Length < sizeMax())
                    {
                        mutex.WaitOne();
                        p.Start();
                        mutex.ReleaseMutex();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            p.Start();
                        }
                    }

                    crpytTimer.Stop();
                }
                else
                {
                    if (file.Length < sizeMax())
                    {
                        mutex.WaitOne();
                        file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true); //Copies an existing file to a new file.
                        mutex.ReleaseMutex();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true); //Copies an existing file to a new file.
                        }
                    }

                    stopWatchTimer.Stop();
                }
                i++;
                var filesLeftToDo = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - i;
                string progress = Convert.ToString((100 - (filesLeftToDo * 100) / fileCount)) + "%";
                List<Etat> stateList = state.readOnlyState();


                stateList[getStateIndex].NbFilesLeftToDo = filesLeftToDo.ToString();
                stateList[getStateIndex].Progression = progress;


                state.writeOnlyState(stateList);
                string theTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                log.writeLog(getName, file.FullName, file.FullName.Replace(sourcePATH, destPATH), file.Length.ToString(), stopWatchTimer.Elapsed.ToString(), crpytTimer.Elapsed.ToString(), theTime);
                log.writeXML(getName, file.FullName, file.FullName.Replace(sourcePATH, destPATH), file.Length.ToString(), stopWatchTimer.Elapsed.ToString(), crpytTimer.Elapsed.ToString(), theTime);
            }
            List<Etat> modifyStateList = state.readOnlyState();

            modifyStateList[getStateIndex].Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            modifyStateList[getStateIndex].State = "END";

            state.writeOnlyState(modifyStateList);
        }
        private List<FileInfo> OrderFiles(List<FileInfo> l)
        {

            List<FileInfo> lp = l.Where(el => cryptExt().Contains(el.Extension)).ToList();
            foreach (var t in lp) l.Remove(t);
            lp.AddRange(l);
            return new List<FileInfo>(lp);
        }
        private string[] extPrio()
        {
            var json = File.ReadAllText(Settings.filePathCryptExtensions);
            var List = JsonConvert.DeserializeObject<List<Settings>>(json) ?? new List<Settings>();
            string[] extensions = new string[] { List[0].extensionsAccepted };
            extensions = extensions[0].Split(',', ' ');

            return extensions;
        }
        private string[] cryptExt()
        {
            var json = File.ReadAllText(Settings.filePathPriorityExtensions);
            var List = JsonConvert.DeserializeObject<List<Settings>>(json) ?? new List<Settings>();
            string[] extensions = new string[] { List[0].extensionsAccepted };
            extensions = extensions[0].Split(',', ' ');

            return extensions;
        }
        private long sizeMax()
        {
            var json = File.ReadAllText(SizeMax.filepathSizeMax);
            var List = JsonConvert.DeserializeObject<List<SizeMax>>(json) ?? new List<SizeMax>();
            long taile = long.Parse(List[0].Size);

            return taile;
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