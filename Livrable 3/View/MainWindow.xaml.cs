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
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
                settings.Content = "Réglages";
            }
        }
    }
}
