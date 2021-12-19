using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        private Socket socket;
        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void SeConnecter(string ip, int port)
        {
            IPAddress ipAddress2 = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(ipAddress2, port);
            try
            {
                socket.Connect(endPoint);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Connexion impossible");
            }
        }
        public int sendData(string data)
        {
            byte[] dataRep = new Byte[1024];
            int i = 0;
            dataRep = Encoding.ASCII.GetBytes(data);
            socket.Send(dataRep, dataRep.Length, SocketFlags.None);
            while (true)
            {
                dataRep = new Byte[1024];
                int bytesRec = socket.Receive(dataRep);
                string a = Encoding.ASCII.GetString(dataRep, 0, bytesRec);
                Debug.WriteLine(a);
                if (a != "")
                {
                    i = Convert.ToInt32(a);
                    break;
                }
            }
            return i;
        }
        private void Deconnecter()
        {
            // Afficher le message « Déconnexion du serveur en cours … "  
            Console.WriteLine("Deconexion en cours");
            // Arrêter la communication socket passé en paramètre dans les deux sens entre le Serveur et le Client
            socket.Shutdown(SocketShutdown.Both);
            // Fermer le socket passé en paramètre 
            socket.Close();
            // Afficher le message « Déconnexion su serveur terminée. "  
            Console.WriteLine("Deconnexion finie");
        }
    }
}
