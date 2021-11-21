using System;
using System.IO;
using System.Linq;

namespace Projet
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                string input = "";

                Console.Write("1. Ajouter un travail de sauvegarde \t");
                Console.Write("2. Executer un travail de sauvegarde\n");
                Console.WriteLine("3. Quittez l'application\n");


                    string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write("Entrez le nom d'un travail de sauvegarde :");
                    string inputName = Console.ReadLine();
                    Console.WriteLine("");
                    Console.Write("Entrer le chemin répertoire source :");
                    string inputSourcePath = Console.ReadLine();
                    int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                    Console.Write("la taille du repertoire est de : ");
                    Travail sizeF = new Travail();
                    var size = sizeF.GetFileSizeSumFromDirectory(inputSourcePath);
                    Console.WriteLine(size);
                    Console.Write("Le nombre de fichier a l'interieur du répertoire est de : ");
                    Console.WriteLine(fCount);
                    Console.WriteLine("");
                    Console.Write("Entrer le chemin répertoire cible :");
                    string inputDestinationPath = Console.ReadLine();
                    Console.WriteLine("");
                    Console.WriteLine("Choisissez un type de sauvegarde :\n");
                    Console.Write("1. Sauvegarde complète \t");
                    Console.WriteLine("2. Sauvegarde Différentielle\n");

                    input = Console.ReadLine();

                    if (input == "1")
                    {

                        string inputType = "Complète";
                        Travail travail = new Travail(inputName, inputSourcePath, inputDestinationPath, inputType);
                        travail.addWork();

                    }
                    else if (input == "2")
                    {

                        string inputType = "Différentielle";
                        Travail travail = new Travail(inputName, inputSourcePath, inputDestinationPath, inputType);
                        travail.addWork();

                    }
                    else
                    {

                        Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3>\n");

                    }

                }
                else if (input == "2")
                {
                    Travail travail = new Travail();
                    travail.displayWorks();
                }
                else if (input == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2>\n");
                }
            }
        }
    }
}
