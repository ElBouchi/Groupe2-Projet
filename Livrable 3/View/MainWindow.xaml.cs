using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Ookii;

namespace Projet.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addWork openAddView = new addWork();
            openAddView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            executeWork openExecuteView = new executeWork();
            openExecuteView.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var jsonDataWork = File.ReadAllText(Model.Language.filePath); //Read the JSON file
            var lg = JsonConvert.DeserializeObject<List<Model.Language>>(jsonDataWork) ?? new List<Model.Language>(); //convert a string into an object for JSON

            if (lg[0].language == "English" || lg[0].language == "")
            {
                Model.Language.verifLg = lg[0].language;
                addButton.Content = "Add a backup work";
                execButton.Content = "Execute backup works";
                settings.Content = "Settings";
            }
            else
            {
                Model.Language.verifLg = lg[0].language;
                addButton.Content = "Ajouter un travail de  sauvegarde";
                execButton.Content = "Executer des travaux de saveugardes";
                settings.Content = "RÃ©glages";
            }
        }

        private void lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void changeLG_Click(object sender, RoutedEventArgs e)
        {
            if (lang.Text != "")
            {
                var jsonDataWork = File.ReadAllText(Model.Language.filePath); //Read the JSON file
                var changLg = JsonConvert.DeserializeObject<List<Model.Language>>(jsonDataWork) ?? new List<Model.Language>(); //convert a string into an object for JSON

                if (changLg.Count == 0)
                {
                    Model.Language.verifLg = lang.Text;
                    changLg.Add(new Model.Language() //parameter that the JSON file will contains
                    {
                        language = lang.Text
                    });
                }
                else
                {
                    Model.Language.verifLg = changLg[0].language;
                    changLg[0].language = lang.Text;
                    string strResultJsonState = JsonConvert.SerializeObject(changLg, Formatting.Indented);  //convert an object into a string for JSON
                    File.WriteAllText(Model.Language.filePath, strResultJsonState);
                    this.Window_Loaded(sender, e);
                }
            }
        }
    }
}