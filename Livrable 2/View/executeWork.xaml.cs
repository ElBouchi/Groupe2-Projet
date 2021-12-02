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
            ViewModel.EasySave execWork = new ViewModel.EasySave();
            Works.ItemsSource = execWork.displayWorks();
        }

        private void uniqueExec_Click(object sender, RoutedEventArgs e)
        {
            if (index.Text != "") 
            {
                ViewModel.EasySave execWork = new ViewModel.EasySave();
                execWork.ExecuteWork(index.Text);
            } else 
            {
                MessageBox.Show("Please enter the row of a backup work");
            }


        }

        private void sequentialExec_Click(object sender, RoutedEventArgs e)
        {
            string workNumbers = Convert.ToString(Works.Items.Count - 1);
            MessageBox.Show(workNumbers);

            ViewModel.EasySave execAllWork = new ViewModel.EasySave();
            execAllWork.ExecuteAllWork();
        }
    }
}
