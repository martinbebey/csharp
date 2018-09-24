using System;
using System.IO;
using System.Linq;
using BinarySearchTree;

public class UserApp
{
    public void TransDataProcessing()
    {
        TheLog theLog = new TheLog();
        theLog.StatusUpdate("UserApp started", 0);
        StreamWriter file = new StreamWriter(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310 Ass1\CS3310 Ass1\bin\Debug\TheLog.txt");
        int count = 0;
        bool countryFound;
        int numberOfNodesVisited = 0;
        TransData transData = new TransData();
        transData.Data = transData.GetTransData();
        CountryDataTable<string> countries = new CountryDataTable<string>();

        foreach (string transCode in transData.Data)
        {
            Console.WriteLine(transCode);              
            file.WriteLine(transCode);
            ++count;
            switch(transCode.Split(' ')[0])
            {
                case "SN":

                    countryFound = countries.Contains(transCode.Split(' ')[1], ref numberOfNodesVisited);
                    if(countryFound)
                    {
                        Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                    else 
                    {
                        Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                  break;

                case "SA":

                    theLog.StatusUpdate("Snapshot started", count);
                    Console.WriteLine("CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                    theLog.Files.WriteLine("CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");   
                    countries.Inorder(countries.Root);
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    theLog.Files.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                  break;

                case "IN":

                    countryFound = countries.Contains(transCode.Split(' ')[1], ref numberOfNodesVisited);
                    if (!countryFound)
                    {
                        countries.Add(transCode.Split(' ')[1]);
                        Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                    else
                    {
                        Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                    break;

                case "DN"://static delete
                    if (countries.Contains(transCode.Split(' ')[1], ref numberOfNodesVisited))
                    {
                        countries.Remove(transCode.Split(' ')[1]);
                        Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                    else
                    {
                        Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                        file.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                    }

                    break;

                default:
                    break;
            }
        }

        theLog.StatusUpdate("UserApp finished", count);
    }
}