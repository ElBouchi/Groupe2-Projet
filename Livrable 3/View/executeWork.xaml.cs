using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using Projet.Model;
using System.Diagnostics;

namespace Projet.View
{
    /// <summary>
    /// Logique d'interaction pour executeWork.xaml
    /// </summary>
    public partial class executeWork : Window
    {
        private ObservableCollection<Model.Etat> worksState;
        private int inputUser = -1;
        private string progression = "";
        private string stateWork = "";
        public static List<(string Nom, Thread t)> ls_thread = new List<(string Nom, Thread t)>();

        public executeWork()
        {
            InitializeComponent();
            Thread Recup_Etat = new Thread(Suivit_Loaded);
            Recup_Etat.Start();
            Thread th = new Thread(serverEcoute);
            th.Start();
        }
        public void serverEcoute()
        {
            Server server = new Server();
            server.SeConnecter("127.0.0.1", 80);
            server.AccepterConnection();
            MessageBox.Show("Le client est connecté");
            string dataReceive = "";
            while (true)
            {
                dataReceive = server.EcouterReseau();
                Debug.WriteLine(dataReceive);
                if (dataReceive == "demande d info")
                {
                    server.envoiData(progression.ToString());
                }
                else if (dataReceive == "pause")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].pause(inputUser);
                    //
                    server.envoiData("0");
                }
                else if (dataReceive == "play")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].play(inputUser);
                    //
                    server.envoiData("0");
                }
                else if (dataReceive == "stop")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].stop(inputUser);
                    //
                    server.envoiData("0");
                }
            }
        }
        private void Suivit_Loaded()
        {
            while (true)
            {
                //
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new Action(delegate ()
                {
                    ViewModel.EasySave watch = new ViewModel.EasySave();
                    worksState = watch.displayWorksState();
                    progression = worksState[0].Progression.Replace("%", "");
                    Works.ItemsSource = worksState;

                }));
                Thread.Sleep(800);
            }
        }
        private void Works_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void executeWork_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
            {
                uniqueExec.Content = "Running a single backup job";
                Play.Content = "Play";
                Pause.Content = "Pause";
                Stop.Content = "Stop";
                Delete.Content = "Delete Work";
            }
            else
            {
                uniqueExec.Content = "Executer un travail de sauvegarde";
                Play.Content = "Lancer";
                Pause.Content = "Pause";
                Stop.Content = "Arrêtez";
                Delete.Content = "Supprimez un travail";
            }
        }

        private void uniqueExec_Click(object sender, RoutedEventArgs e)
        {

            if (inputUser != -1 && inputUser <= Works.Items.Count)
            {
                Model.Etat obj = new Model.Etat();
                var workList = obj.readOnlyState();
                stateWork = workList[inputUser].State;
                if (stateWork != "Active")
                {
                    ViewModel.EasySave execWork = ViewModel.EasySave.Getinstance();
                    Thread th_Exe_Work = new Thread(() => execWork.ExecuteWork(inputUser, false));
                    string nm = "thread" + inputUser;
                    th_Exe_Work.Name = nm;
                    th_Exe_Work.Start();
                    ls_thread.Add((nm, th_Exe_Work));
                }
                else
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("Backup work already in execution");
                    }
                    else
                    {
                        MessageBox.Show("Travail de sauvegarde en cours d'execution");
                    }
                }


            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Please select the row of a backup work");
                }
                else
                {
                    MessageBox.Show("Sélectionnez la ligne du travail de sauvegarde souhaité");
                }
            }


        }
        private void Works_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Works.SelectedIndex != -1)
            {
                inputUser = Works.SelectedIndex;
            }
        }
        public void Pause_Click(object sender, RoutedEventArgs e)
        {
            Model.Etat obj = new Model.Etat();
            var workList = obj.readOnlyState();
            stateWork = workList[inputUser].State;

            if (inputUser != -1 && inputUser <= Works.Items.Count)
            {
                if (stateWork == "Active")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].pause(inputUser);
                }
                else
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("Backup work not in execution");
                    }
                    else
                    {
                        MessageBox.Show("Travail de sauvegarde pas en execution");
                    }
                }
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Please select the row of a backup work");
                }
                else
                {
                    MessageBox.Show("Sélectionnez la ligne du travail de sauvegarde souhaité");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (inputUser != -1 && inputUser <= Works.Items.Count)
            {
                ViewModel.EasySave del = new ViewModel.EasySave();
                del.deleteWork(inputUser);
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Please select the row of a backup work");
                }
                else
                {
                    MessageBox.Show("Sélectionnez la ligne du travail de sauvegarde souhaité");
                }
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Model.Etat obj = new Model.Etat();
            var workList = obj.readOnlyState();
            stateWork = workList[inputUser].State;

            if (inputUser != -1 && inputUser <= Works.Items.Count)
            {
                if (stateWork == "Pause")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].play(inputUser);
                }
                else
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("Please select the row of a backup work");
                    }
                    else
                    {
                        MessageBox.Show("Sélectionnez la ligne du travail de sauvegarde souhaité");
                    }
                }
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Backup work not in pause");
                }
                else
                {
                    MessageBox.Show("Travail de sauvegarde pas en pause");
                }
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Model.Etat obj = new Model.Etat();
            var workList = obj.readOnlyState();
            stateWork = workList[inputUser].State;

            if (inputUser != -1 && inputUser <= Works.Items.Count)
            {
                if (stateWork != "Interrupted")
                {
                    ViewModel.EasySave exec = ViewModel.EasySave.Getinstance();
                    exec.fullB[inputUser].stop(inputUser);
                }
                else
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("Backup work not in pause");
                    }
                    else
                    {
                        MessageBox.Show("Travail de sauvegarde pas en pause");
                    }
                }
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Please select the row of a backup work");
                }
                else
                {
                    MessageBox.Show("Sélectionnez la ligne du travail de sauvegarde souhaité");
                }
            }
        }
    }
}
