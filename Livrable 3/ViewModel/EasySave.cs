using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Projet.ViewModel
{
    class EasySave
    {
        private static Object _locker = new Object();
        // a method that will allow to create a backupwork
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {

            Model.Etat verifyName = new Model.Etat();
            var nameStateList = verifyName.readOnlyState();

            bool nameExist = false;
            for (int i = 0; i < nameStateList.Count; i++)
            {
                if (nameStateList[i].Name == theName)
                {
                    nameExist = true;
                    break;
                }
                else
                {
                    nameExist = false;
                }
            }

            if (!nameExist)
            {
                Model.Work createWork = new Model.Work();
                createWork.writeWork(theName, theRepS, theRepC, theType);

                string theTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string theTFTC = countfile.ToString();
                string theTFS = filesize.ToString();

                Model.Etat createState = new Model.Etat();
                createState.writeState(theName, theRepS, theRepC, theTime, "INACTIVE", theTFTC, theTFS, "0", "0%");

                // Switch the language of the outpoot according to the choice of the user when he started the program

                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Work created successfully");
                }
                else
                {
                    MessageBox.Show("Travail ajouté avec succès");
                }
            }
            else
            {   // Switch the language of the outpoot according to the choice of the user when he started the program
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("A work with the same name already exists");
                }
                else
                {
                    MessageBox.Show("Un travail avec le meme nom existe déjà !");
                }
            }
        }
        public List<Model.Work> displayWorks() // a method that will allow to display all our backupwork
        {
            Model.Work Works = new Model.Work();
            var workList = Works.readOnlyWork();

            return workList;
        }
        public void ExecuteWork(int inputUtilisateur, bool paral) // a method that will allow to execute a backupwork created
        {
            Model.Work Works = new Model.Work();
            var workList = Works.readOnlyWork();

            if (workList.Count >= inputUtilisateur) //this condition allow to the user to choose the exact row in order to execute the backupwork chosen
            {
                int index = inputUtilisateur - 1;

                string workName = workList.ElementAt(index).name;
                string sourceDir = workList.ElementAt(index).repS;
                string backupDir = workList.ElementAt(index).repC;                
                long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                Model.Etat state = new Model.Etat();
                var stateList = state.readOnlyState();

                int indexState = 0;
                for (int i = 0; i < stateList.Count; i++)
                {
                    if (stateList[i].Name == workList[index].name)
                    {
                        indexState = i;
                        break;
                    }
                }

                //this condition is used to execute the type of backup chosen in the creation 
                if (workList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).type == "Differential")
                {
                    stateList[indexState].State = "Active";

                    state.writeOnlyState(stateList);
                    // differential backup
                    Model.DifferentialBackup SD = new Model.DifferentialBackup();
                    SD.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, index, workName);

                    if (!paral)
                    {

                        var workListDelete = Works.readOnlyWork();

                        workListDelete.Remove(workListDelete[index]);

                        Works.writeOnlyWork(workListDelete);
                    }

                }
                else
                {

                    stateList[indexState].State = "Active";

                    state.writeOnlyState(stateList);
                    // complete backup
                    Model.FullBackup SD = new Model.FullBackup();
                    SD.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, index, workName);

                    if (!paral)
                    {

                        var workListDelete = Works.readOnlyWork();

                        workListDelete.Remove(workListDelete[index]);

                        Works.writeOnlyWork(workListDelete);
                    }

                }

            }
            else
            {   // Switch the language of the outpoot according to the choice of the user when he started the program

                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("No backup job with entry " + inputUtilisateur + " found !");
                }
                else
                {
                    MessageBox.Show("La ligne " + inputUtilisateur + " ne contient aucun travail de sauvegarde !");
                }              
            }
        }
        public void ExecuteAllWork()
        {
            Model.Work Works = new Model.Work();
            List<Model.Work> workList = Works.readOnlyWork();
            Dictionary<string, Thread> threadList = new Dictionary<string, Thread>(workList.Count);

            for (int i = 0; i < workList.Count; i++)
            {
                int index = workList.Count - i;
                string threadname = "thread" + index.ToString();
                threadList.Add(threadname, new Thread(() => ExecuteWork(index, true)));
                threadList[threadname].Start();
            }

            while (threadList.All(x => x.Value.IsAlive) && threadList.Count == workList.Count) {}

            for (int i = 0; i < workList.Count; i++)
            {
                var workListDelete = Works.readOnlyWork();

                workListDelete.Remove(workListDelete[0]);

                Works.writeOnlyWork(workListDelete);
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
