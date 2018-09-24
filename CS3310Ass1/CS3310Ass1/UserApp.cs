/*This is the userAPP Procedural class used to process the country datatable according to the transData obtained from the transdatafiles
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using BST;

public class UserApp
{
    private int count, numberOfNodesVisited;//count used to count the number of transactionsprocessed
    private bool countryFound;//determines whether or not a country exists in the country table
    private TransData transData = new TransData();//transdata object instance used to access transdata files

    public UserApp()//constructor
    {
        count = 0;
    }

    //this basically processes transdata1 file or calls other identical methods to process other transdata files respectively
    public void TransDataProcessing(bool processTransData1, bool processTransData2, bool processTransData3, bool processTransData4, TheLog theLog, BSTree countries)
    {
        
        theLog.displayThis("CODE STATUS > UserApp started");
        
        if (processTransData1)
        {
            
            numberOfNodesVisited = 0;
            transData.Data = transData.GetTransData1(theLog);//gets transdata from the transdata1 file

            foreach (string transCode in transData.Data)//loop to process transactions
            {
                if (transCode != null)
                {
                    Console.WriteLine(transCode);
                    theLog.displayThis(transCode);
                    ++count;//counts the number of transactions

                    //this if else statement ensure the transaction code is in a correct format for processing
                    if (transCode.Length < 3 && transCode != "SA")
                    {
                        countryFound = false;
                    }

                    else
                    {
                        if (transCode.Split(' ')[0] == "IN")
                        {

                            countryFound = countries.Contains((System.Text.RegularExpressions.Regex.Replace(transCode.Split(',')[1], "'", "")), ref numberOfNodesVisited, countries);
                        }

                        else if (transCode != "SA")
                        {
                            countryFound = countries.Contains(transCode.Substring(3), ref numberOfNodesVisited, countries);
                        }
                    }

                    switch (transCode.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SN":

                            if (countryFound)
                            {
                                Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tOK, country found\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;//resetsnumber of nodes visited to 0 so its starts again for the next transaction
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;
                            }

                            break;

                        case "SA"://this uses inorder traversal
                      
                            Console.WriteLine("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            theLog.displayThis("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            countries.Inorder(countries.CountryDataTable[countries.RootPointer], theLog);
                            Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                            theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                            break;

                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(transCode.Split('(')[1], countries);
                                Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tOK, country inserted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tSORRY, country data already exists\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;
                            }

                            break;

                        case "DN"://static delete
                            if (countryFound)
                            {
                                countries.Remove(transCode.Substring(3), countries);
                                Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tOK, country deleted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                                theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                                numberOfNodesVisited = 0;
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data\n\t\t>> {0} nodes visited", numberOfNodesVisited);

                            break;
                    }
                }
            }
        }

        //calls other procedures to process other transdata files as needed
        if(processTransData2)
        {
            TransDataProcessing2(theLog, countries);
        }

        if (processTransData3)
        {
            TransDataProcessing3(theLog, countries);
        }

        if (processTransData4)
        {
            TransDataProcessing4(theLog, countries);
        }       

        theLog.displayThis("CODE STATUS > UserApp finished - " + count + " transactions processed");
    }

    //below are 3 similar procedures that use other transdata files as needed
    public void TransDataProcessing2(TheLog theLog, BSTree countries)
    {
       
        theLog.displayThis("CODE STATUS > UserApp started");       
        numberOfNodesVisited = 0;
        transData.Data = transData.GetTransData2(theLog);

        foreach (string transCode in transData.Data)
        {
            if (transCode != null)
            {
                Console.WriteLine(transCode);
                theLog.displayThis(transCode);
                ++count;

                if (transCode.Length < 3 && transCode != "SA")
                {
                    countryFound = false;
                }

                else
                {
                    if(transCode.Split(' ')[0] == "IN")
                    {
                        
                        countryFound = countries.Contains((System.Text.RegularExpressions.Regex.Replace(transCode.Split(',')[1], "'", "")), ref numberOfNodesVisited, countries);
                    }

                    else if (transCode != "SA")
                    {
                        countryFound = countries.Contains(transCode.Substring(3), ref numberOfNodesVisited, countries);
                    }
                }

                switch (transCode.Split(' ')[0])
                {
                    case "SN":

                        
                        if (countryFound)
                        {
                            Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country found\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "SA":


                        Console.WriteLine("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        theLog.displayThis("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        countries.Inorder(countries.CountryDataTable[countries.RootPointer], theLog);
                        Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                        theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                        break;

                    case "IN":

                        
                        if (!countryFound)
                        {
                            countries.Add(transCode.Split('(')[1], countries);
                            Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country inserted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, country data already exists\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "DN"://static delete
                        if (countryFound)
                        {
                            countries.Remove(transCode.Substring(3), countries);
                            Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country deleted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    default:

                        Console.WriteLine("\tSORRY, invalid transaction data\n\t\t>> {0} nodes visited", numberOfNodesVisited);

                        break;
                }
            }
        }
    }

    public void TransDataProcessing3(TheLog theLog, BSTree countries)
    {
        theLog.displayThis("CODE STATUS > UserApp started");
        numberOfNodesVisited = 0;
        transData.Data = transData.GetTransData3(theLog);

        foreach (string transCode in transData.Data)
        {
            if (transCode != null)
            {
                Console.WriteLine(transCode);
                theLog.displayThis(transCode);
                ++count;

                if (transCode.Length < 3 && transCode != "SA")
                {
                    countryFound = false;
                }

                else
                {
                    if (transCode.Split(' ')[0] == "IN")
                    {

                        countryFound = countries.Contains((System.Text.RegularExpressions.Regex.Replace(transCode.Split(',')[1], "'", "")), ref numberOfNodesVisited, countries);
                    }

                    else if (transCode != "SA")
                    {
                        countryFound = countries.Contains(transCode.Substring(3), ref numberOfNodesVisited, countries);
                    }
                }

                switch (transCode.Split(' ')[0])
                {
                    case "SN":


                        if (countryFound)
                        {
                            Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country found\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "SA":
                      
                        Console.WriteLine("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        theLog.displayThis("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        countries.Inorder(countries.CountryDataTable[countries.RootPointer], theLog);
                        Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                        theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                        break;

                    case "IN":


                        if (!countryFound)
                        {
                            countries.Add(transCode.Split('(')[1], countries);
                            Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country inserted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, country data already exists\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "DN"://static delete
                        if (countryFound)
                        {
                            countries.Remove(transCode.Substring(3), countries);
                            Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country deleted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    default:

                        Console.WriteLine("\tSORRY, invalid transaction data\n\t\t>> {0} nodes visited", numberOfNodesVisited);

                        break;
                }
            }
        }
    }

    public void TransDataProcessing4(TheLog theLog, BSTree countries)
    {
        theLog.displayThis("CODE STATUS > UserApp started");
        numberOfNodesVisited = 0;
        transData.Data = transData.GetTransData4(theLog);

        foreach (string transCode in transData.Data)
        {
            if (transCode != null)
            {
                Console.WriteLine(transCode);
                theLog.displayThis(transCode);
                ++count;

                if (transCode.Length < 3 && transCode != "SA")
                {
                    countryFound = false;
                }

                else
                {
                    if (transCode.Split(' ')[0] == "IN")
                    {

                        countryFound = countries.Contains((System.Text.RegularExpressions.Regex.Replace(transCode.Split(',')[1], "'", "")), ref numberOfNodesVisited, countries);
                    }

                    else if (transCode != "SA")
                    {
                        countryFound = countries.Contains(transCode.Substring(3), ref numberOfNodesVisited, countries);
                    }
                }

                switch (transCode.Split(' ')[0])
                {
                    case "SN":


                        if (countryFound)
                        {
                            Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country found\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "SA":

                        //theLog.StatusUpdate("Snapshot started", count);
                        Console.WriteLine("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        theLog.displayThis("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                        countries.Inorder(countries.CountryDataTable[countries.RootPointer], theLog);
                        Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                        theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                        break;

                    case "IN":


                        if (!countryFound)
                        {
                            countries.Add(transCode.Split('(')[1], countries);
                            Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country inserted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, country data already exists\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    case "DN"://static delete
                        if (countryFound)
                        {
                            countries.Remove(transCode.Substring(3), countries);
                            Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tOK, country deleted\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        else
                        {
                            Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfNodesVisited);
                            theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfNodesVisited + " nodes visited");
                            numberOfNodesVisited = 0;
                        }

                        break;

                    default:

                        Console.WriteLine("\tSORRY, invalid transaction data\n\t\t>> {0} nodes visited", numberOfNodesVisited);

                        break;
                }
            }
        }
    }
}