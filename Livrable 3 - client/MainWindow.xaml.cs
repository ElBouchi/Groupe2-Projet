using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		Client client;
		int progression;

		public MainWindow()
        {
			client = new Client();
			client.SeConnecter("127.0.0.1", 80);
			Debug.WriteLine("la console de debug est la");
			progression = 0;
		}
		private void demandeInfoServer()
        {
			progression = client.sendData("demande d info");
        }
		// Méthode qui initialise la barre de progression 
		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			(sender as BackgroundWorker).ReportProgress(progression);
		}

		void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{   
			//initialisation de la barre de progression avec le pourcentage de progression
			pbstatus1.Value   = e.ProgressPercentage;

			//Affichage de la progression sur un label
			lb_etat_prog_server.Content = pbstatus1.Value.ToString() +"%";

			
			
		}

		// lancer la barre de progression en créant un objet de type BackgroundWorker
		//BackgroundWorker :
		private void Button_Click(object sender, RoutedEventArgs e)
        {
            //création, initialisation et mise à jour de l'objet BackgroundWorker
			demandeInfoServer();
			BackgroundWorker worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.DoWork += worker_DoWork;
			worker.ProgressChanged += worker_ProgressChanged;
			worker.RunWorkerAsync();
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			client.sendData("pause");
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			client.sendData("play");
		}

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
			client.sendData("stop");
		}
    }
}
