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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(lang.Text != "")
            {
                var jsonDataWork = File.ReadAllText(Model.Language.filePath); //Read the JSON file
                var changLg = JsonConvert.DeserializeObject<List<Model.Language>>(jsonDataWork) ?? new List<Model.Language>(); //convert a string into an object for JSON

                if(changLg.Count == 0)
                {
                    changLg.Add(new Model.Language() //parameter that the JSON file will contains
                    {
                        language = lang.Text
                    });
                }
                else
                {
                    if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
                    {
                        MessageBox.Show("the application will close to apply the changes", MessageBoxButton.OK.ToString());
                    }
                    else
                    {
                        MessageBox.Show("l'application vas etre fermé pour appliquer les changements", MessageBoxButton.OK.ToString());
                    }
                    changLg[0].language = lang.Text;
                    Application.Current.Shutdown();
                }

                string strResultJsonState = JsonConvert.SerializeObject(changLg, Formatting.Indented);  //convert an object into a string for JSON
                File.WriteAllText(Model.Language.filePath, strResultJsonState);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model.Language.verifLg == "English" || Model.Language.verifLg == "")
            {
                langLabel.Content = "Change language";
                saveLabel.Content = "Save";
                changeExt.Content = "Changer extensions";
            }
            else
            {
                langLabel.Content = "Changer de language";
                saveLabel.Content = "Sauvegarder";
                changeExt.Content = "Change extensions";
            }
        }

        private void changeExt_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", @"..\..\..\extensions.json");
        }
    }
}
