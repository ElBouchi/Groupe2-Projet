using System;
using System.IO;

namespace Projet
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
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
                        try
                        {
                            var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories);
                            int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                            Backup backup = new Backup();
                            long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                            string inputType = "Complete";
                            backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);
                        }
                        catch
                        {
                            Console.Write("Le répertoire source ou de destination précisé est erroné");
                        }
                    }
                    else if (input == "2")
                    {
                        try
                        {
                            var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories);
                            int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                            Backup backup = new Backup();
                            long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                            string inputType = "Différentielle";
                            backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);
                        }
                        catch
                        {
                            Console.WriteLine("Le répertoire source ou de destination précisé est erroné\n");
                        }
                    }
                    else
                    {

                        Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3>\n");

                    }

                }
                else if (input == "2")
                {
                    Console.WriteLine("Voici les différents travaux de sauvegardes :");

                    Backup backup = new Backup();
                    backup.displayWorks();
                    Console.WriteLine("");

                    Console.Write("1. Execution d'un des travaux de sauvegarde \t");
                    Console.WriteLine("2. Execution séquentielle de l'ensemble des travaux \n");

                    input = Console.ReadLine();

                    if (input == "1")
                    {
                        backup.displayWorks();
                        Console.WriteLine("Veuillez sélectionner l'index correspondant au travail de sauvegarde souhaité \n");

                        input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 1");
                                backup.ExecuteWork(input);
                                break;
                            case "2":
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 2");
                                backup.ExecuteWork(input);
                                break;
                            case "3":
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 3");
                                backup.ExecuteWork(input);
                                break;
                            case "4":
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 4");
                                backup.ExecuteWork(input);
                                break;
                            case "5":
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 5\n");
                                backup.ExecuteWork(input);
                                break;
                            default:
                                Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3> ou <4> ou <5>");
                                break;
                        }
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("En Cours de développement\n");
                    }
                    else
                    {
                        Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2>\n");
                    }
                }
                else if (input == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3>\n");
                }
            }
        }
    }
}