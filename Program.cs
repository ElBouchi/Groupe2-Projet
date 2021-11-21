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
                            Travail sizeF = new Travail();
                            long size = sizeF.GetFileSizeSumFromDirectory(inputSourcePath);
                            string inputType = "Complete";
                            Travail travail = new Travail(inputName, inputSourcePath, inputDestinationPath, inputType);
                            travail.addWork(size, fCount);
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
                            Travail sizeF = new Travail();
                            long size = sizeF.GetFileSizeSumFromDirectory(inputSourcePath);
                            string inputType = "Differentielle";
                            Travail travail = new Travail(inputName, inputSourcePath, inputDestinationPath, inputType);
                            travail.addWork(size, fCount);
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

                    Travail travail = new Travail();
                    travail.displayWorks();
                    Console.WriteLine("");

                    Console.Write("1. Execution d'un des travaux de sauvegarde \t");
                    Console.WriteLine("2. Execution séquentielle de l'ensemble des travaux \n");

                    input = Console.ReadLine();

                    if (input == "1")
                    {
                        travail.displayWorks();
                        Console.WriteLine("Veuillez sélectionner le numéro de la ligne correspondante au travail de sauvegarde souhaité \n");

                        input = Console.ReadLine();

                        switch (input)
                        {
                            case "1" :
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 1");
                                travail.ExecuteWork(input);
                                break;
                            case "2" :
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 2");
                                travail.ExecuteWork(input);
                                break;
                            case "3" :
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 3");
                                travail.ExecuteWork(input);
                                break;
                            case "4" :
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 4");
                                travail.ExecuteWork(input);
                                break;
                            case "5" :
                                Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 5\n");
                                travail.ExecuteWork(input);
                                break;
                            default:
                                Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3> ou <4> ou <5>");
                                break;
                        }
                    }
                    else if(input == "2"){
                        Console.WriteLine("En Cours de développement\n");
                    }else
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
                    Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2>\n");
                }
            }
        }
    }
}