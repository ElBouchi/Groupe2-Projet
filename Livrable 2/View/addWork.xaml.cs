using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet.View
{
    /// <summary>
    /// Logique d'interaction pour addWork.xaml
    /// </summary>
    public partial class addWork : Window
    {
        public addWork()
        {
            InitializeComponent();
        }

        private void uploadSource_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog openDlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                sourcePATH.Text = openDlg.SelectedPath;
                // TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Source path empty");
                }
                else
                {
                    MessageBox.Show("Le répertoire source précisé est vide");
                }        
            }
        }

        private void uploadDestination_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog openDlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                destPATH.Text = openDlg.SelectedPath;
                // TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Target path empty");
                }
                else
                {
                    MessageBox.Show("Le répertoire de destination précisé est vide");
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && sourcePATH.Text != "" && destPATH.Text != "" && backupType.Text != "")
            {
                ViewModel.EasySave addWork = new ViewModel.EasySave();

                int fCount = Directory.GetFiles(sourcePATH.Text, "*", SearchOption.AllDirectories).Length;
                long size = addWork.GetFileSizeSumFromDirectory(sourcePATH.Text);

                try
                {
                    addWork.addWork(size, fCount, Name.Text, sourcePATH.Text, destPATH.Text, backupType.Text);
                    Name.Text = "";
                    sourcePATH.Text = "";
                    destPATH.Text = "";
                    backupType.Text = "";

                }
                catch
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("An error occured");
                    }
                    else
                    {
                        MessageBox.Show("une erreur est survenue");
                    }

                    Name.Text = "";
                    sourcePATH.Text = "";
                    destPATH.Text = "";
                    backupType.Text = "";
                }
            }
            else
            {
                if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                {
                    MessageBox.Show("Fill all the fields");
                }
                else
                {
                    MessageBox.Show("Veuillez remplir tout les champs");
                }  
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
            {
                nameLabel.Content = "Name";
                sourceLabel.Content = "Source Path";
                destLabel.Content = "Destination Path";
                backupTypeLabel.Content = "Backup Type";
                uploadSource.Content = "Upload";
                uploadDestination.Content = "Upload";
                backupType.Items.Add("Full");
                backupType.Items.Add("Differential");
                Add.Content = "Add";
            }
            else
            {
                nameLabel.Content = "Nom";
                sourceLabel.Content = "Chemin Source";
                destLabel.Content = "Chemin Cible";
                backupTypeLabel.Content = "Type de sauvegarde";
                uploadSource.Content = "Parcourir";
                uploadDestination.Content = "Parcourir";
                backupType.Items.Add("Full");
                backupType.Items.Add("Differential");
                Add.Content = "Ajouter";
            }
        }
    }
}
