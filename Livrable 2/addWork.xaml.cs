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
                MessageBox.Show("Le répertoire source précisé est vide");
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
                MessageBox.Show("Le répertoire destination précisé est vide");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EasySave addWork = new ViewModel.EasySave();

            int fCount = Directory.GetFiles(sourcePATH.Text, "*", SearchOption.AllDirectories).Length;
            long size = addWork.GetFileSizeSumFromDirectory(sourcePATH.Text);

            try
            {
                addWork.addWork(size, fCount, Name.Text, sourcePATH.Text, destPATH.Text, backupType.Text);
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue");
            }           
        }
    }
}
