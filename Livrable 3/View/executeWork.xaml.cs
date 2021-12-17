using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Projet.View
{
    /// <summary>
    /// Logique d'interaction pour executeWork.xaml
    /// </summary>
    public partial class executeWork : Window
    {
        public executeWork()
        {
            InitializeComponent();
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
            }
            else
            {
                uniqueExec.Content = "Executer un travail de sauvegarde";
                sequentialExec.Content = "Execution séquentielle";
            }

            ViewModel.EasySave execWork = new ViewModel.EasySave();
            Works.ItemsSource = execWork.displayWorks();
        }

        private void uniqueExec_Click(object sender, RoutedEventArgs e)
        {
            if (Works.SelectedIndex != -1)
            {
                ViewModel.EasySave execWork = new ViewModel.EasySave();
                execWork.ExecuteWork(Works.SelectedIndex, false);

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
    }
}