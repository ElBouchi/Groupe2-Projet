using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
            {
                changeExt.Content = "Define extensions to be encrypted";
                changeExtPrio.Content = "Define priority files extensions";
                bussSoftware.Content = "Define business software";
                sizeMax.Content = "Determine the size of a heavy file";
            }
            else
            {
                changeExt.Content = "Définir extensions à crypté";
                changeExtPrio.Content = "Définir extensions des fichiers prioritaires";
                bussSoftware.Content = "Définir les logiciels métiers";
                sizeMax.Content = "Déterminer la taille d'un fichier lourd";
            }
        }

        private void changeExt_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", @"..\..\..\extensions.json");
        }

        private void lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void changeExtPrio_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", @"..\..\..\extensionsPriority.json");
        }

        private void bussSoftware_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", @"..\..\..\BussSoftware.json");
        }
        
        private void sizeMax_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", @"..\..\..\sizeMax.json");
        }
    }
}