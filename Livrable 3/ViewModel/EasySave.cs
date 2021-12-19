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
using System.Collections.ObjectModel;

namespace Projet.ViewModel
{
    class EasySave
    {
        private static EasySave instance = null;
        public static EasySave Getinstance()
        {
            if (instance == null)
            {
                instance = new EasySave();
            }
            return instance;
        }

        private static Object _locker = new Object();
        public List<Model.FullBackup> fullB = new List<Model.FullBackup>();
        public List<Model.DifferentialBackup> diffB = new List<Model.DifferentialBackup>();
        // a method that will allow to create a backupwork
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            Model.Work verifyName = new Model.Work();
            var nameStateList = verifyName.readOnlyWork();

            bool nameExist = false;
            for (int i = 0; i < nameStateList.Count; i++)
            {
                if (nameStateList[i].name == theName)
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
                createState.writeState(theName, theRepS, theRepC, theTime, "INACTIVE", theTFTC, theTFS, null, "0%");

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
        }//
        public ObservableCollection<Model.Etat> displayWorksState() // a method that will allow to display all our backupwork
        {
            Model.Etat Works = new Model.Etat();
            ObservableCollection<Model.Etat> stateList = Works.readOnlyStateDynamic();

            return stateList;
        }
        public void ExecuteWork(int inputUtilisateur, bool paral) // a method that will allow to execute a backupwork created
        {
            var json = File.ReadAllText(Model.BussSoftware.filepathBussSoftware);
            var List = JsonConvert.DeserializeObject<List<Model.BussSoftware>>(json) ?? new List<Model.BussSoftware>();
            string Business = List[0].bussSoftware;

            if (Process.GetProcessesByName(Business).Length == 0)
            {
                Model.Work Works = new Model.Work();
                var workList = Works.readOnlyWork();

                if (workList.Count >= inputUtilisateur) //this condition allow to the user to choose the exact row in order to execute the backupwork chosen
                {

                    string workName = workList.ElementAt(inputUtilisateur).name;
                    string sourceDir = workList.ElementAt(inputUtilisateur).repS;
                    string backupDir = workList.ElementAt(inputUtilisateur).repC;
                    long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                    Model.Etat state = new Model.Etat();
                    var stateList = state.readOnlyState();

                    int indexState = 0;
                    for (int i = 0; i < stateList.Count; i++)
                    {
                        if (stateList[i].Name == workList[inputUtilisateur].name)
                        {
                            indexState = i;
                            break;
                        }
                    }

                    //this condition is used to execute the type of backup chosen in the creation 
                    if (workList.ElementAt(inputUtilisateur).type == "Differential")
                    {
                        stateList[indexState].State = "Active";

                        state.writeOnlyState(stateList);
                        // differential backup
                        Model.DifferentialBackup diff = new Model.DifferentialBackup();
                        diff.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, inputUtilisateur, workName);

                    }
                    else
                    {
                        stateList[inputUtilisateur].State = "Active";

                        state.writeOnlyState(stateList);
                        // complete backup

                        fullB.Add(new Model.FullBackup());
                        fullB[fullB.Count - 1].Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, inputUtilisateur, workName);

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
            else
            {

                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Cant launch back work because a business software is detected");
                }
                else
                {
                    MessageBox.Show("Impossible de lancer un travail sauvegarde, logiciel métier detecté");
                }
            }
        }
        public void deleteWork(int index)
        {
            Model.Work delWork = new Model.Work();
            Model.Etat delState = new Model.Etat();
            //
            var workList = delWork.readOnlyWork();
            workList.Remove(workList[index]);
            delWork.writeOnlyWork(workList);

            var stateList = delState.readOnlyState();
            stateList.Remove(stateList[index]);
            delState.writeOnlyState(stateList);

        }
        public void ExecuteAllWork()
        { }
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
