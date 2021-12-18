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

namespace Projet.View
{
    /// <summary>
    /// Logique d'interaction pour executeWork.xaml
    /// </summary>
    public partial class executeWork : Window
    {
        private ObservableCollection<Model.Etat> worksState;
        private int inputUser = -1;

        public executeWork()
        {
            InitializeComponent();
            Thread Recup_Etat = new Thread(Suivit_Loaded);
            Recup_Etat.Start();
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
                sequentialExec.Content = "Sequential execution";
                Play.Content = "Play";
                Pause.Content = "Pause";
                Stop.Content = "Stop";
                Delete.Content = "Delete Work";
            }
            else
            {
                uniqueExec.Content = "Executer un travail de sauvegarde";
                sequentialExec.Content = "Execution séquentielle";
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

                ViewModel.EasySave execWork = new ViewModel.EasySave();
                execWork.Execute(inputUser, false);

                executeWork_Loaded(sender, e);

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

        private void sequentialExec_Click(object sender, RoutedEventArgs e)
        {
            string workNumbers = Convert.ToString(Works.Items.Count - 1);

            ViewModel.EasySave execAllWork = new ViewModel.EasySave();
            execAllWork.ExecuteAllWork();

            executeWork_Loaded(sender, e);
        }

        private void Works_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        private void Works_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Works.SelectedIndex != -1)
            {
                inputUser = Works.SelectedIndex;
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EasySave pause = new ViewModel.EasySave();
            pause.pause();
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
    }
}