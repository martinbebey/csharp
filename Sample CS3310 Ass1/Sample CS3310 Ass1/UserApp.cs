/*This is the userAPP Procedural class used to process the country datatable according to the transData obtained from the transdatafiles
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using Countries;

public class UserApp
{
    private int count;//, numberOfCountriesVisited;//count used to count the number of transactionsprocessed
    private bool countryFound;//determines whether or not a country exists in the country table
    private TransData transData = new TransData();//transdata object instance used to access transdata files
    private TheLog theLog = new TheLog();
    private CountryDataTable countries;

    public UserApp(Setup setup)//constructor
    {
        countries = new CountryDataTable(theLog);
        count = 0;
        countries = setup.Countries;
    }

    //this basically processes transdata1 file or calls other identical methods to process other transdata files respectively
    public void TransDataProcessing(bool processTransData5, bool processTransData6, bool processTransData7)
    {
        theLog.Open();//opens the log file
        theLog.displayThis("CODE STATUS > UserApp started");
        
        if (processTransData5)
        {
            
            //numberOfCountriesVisited = 0;
            transData.Data = transData.GetTransData5(theLog);//gets transdata from the transdata1 file

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

                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(33).Split(',')[0].Trim()), countries, theLog);
                        }

                        else if (transCode != "SA")
                        {
                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(3).Trim()), countries, theLog);
                        }
                    }

                    switch (transCode.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SI":

                            if (countryFound)
                            {
                                Console.WriteLine("\tOK, country found");
                                theLog.displayThis("\tOK, country found");
                                //numberOfCountriesVisited = 0;//resetsnumber of nodes visited to 0 so its starts again for the next transaction
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with  that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "SA"://this uses inorder traversal
                      
                            Console.WriteLine("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            theLog.displayThis("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            countries.GetAll(countries, theLog);
                            Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                            theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                           

                            break;

                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(Convert.ToInt16(transCode.Split('(')[1].Split(',')[0]), transCode.Substring(3), countries, theLog);
                                Console.WriteLine("\tOK, country inserted");
                                theLog.displayThis("\tOK, country inserted");
                                //numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, another country has that id");
                                theLog.displayThis("\tSORRY, another country has that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "DI"://static delete
                            if (countryFound)
                            {
                                countries.Remove(Convert.ToInt32(transCode.Split(' ')[1]), countries, theLog);
                                Console.WriteLine("\tOK, country deleted");
                                theLog.displayThis("\tOK, country deleted");
                               // numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data");

                            break;
                    }
                }
            }
        }

        //calls other procedures to process other transdata files as needed
        if(processTransData6)
        {
            TransDataProcessing6(theLog, countries);
        }

        if (processTransData7)
        {
            TransDataProcessing7(theLog, countries);
        }

        //if (processTransData4)
        //{
        //    TransDataProcessing4(theLog, countries);
        //}       

        theLog.displayThis("CODE STATUS > UserApp finished - " + count + " transactions processed");
        countries.FinishUp(countries, theLog, true);
        theLog.FinishUp();
    }

    //below are 3 similar procedures that use other transdata files as needed
    public void TransDataProcessing6(TheLog theLog, CountryDataTable countries)
    {
       
        theLog.displayThis("CODE STATUS > UserApp started");       
        //numberOfCountriesVisited = 0;
        transData.Data = transData.GetTransData6(theLog);

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

                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(33).Split(',')[0].Trim()), countries, theLog);
                        }

                        else if (transCode != "SA")
                        {
                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(3).Trim()), countries, theLog);
                        }
                    }

                    switch (transCode.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SI":

                            if (countryFound)
                            {
                                Console.WriteLine("\tOK, country found");
                                theLog.displayThis("\tOK, country found");
                                //numberOfCountriesVisited = 0;//resetsnumber of nodes visited to 0 so its starts again for the next transaction
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with  that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "SA"://this uses inorder traversal
                      
                             Console.WriteLine("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            theLog.displayThis("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            countries.GetAll(countries, theLog);
                            Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                            theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                            break;

                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(Convert.ToInt16(transCode.Split('(')[1].Split(',')[0]), transCode.Substring(3), countries, theLog);
                                Console.WriteLine("\tOK, country inserted");
                                theLog.displayThis("\tOK, country inserted");
                                //numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, another country has that id");
                                theLog.displayThis("\tSORRY, another country has that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "DI"://static delete
                            if (countryFound)
                            {
                                countries.Remove(Convert.ToInt32(transCode.Split(' ')[1]), countries, theLog);
                                Console.WriteLine("\tOK, country deleted");
                                theLog.displayThis("\tOK, country deleted");
                               // numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data");

                            break;
                }
            }
        }
    }

    public void TransDataProcessing7(TheLog theLog, CountryDataTable countries)
    {
        theLog.displayThis("CODE STATUS > UserApp started");
        //numberOfCountriesVisited = 0;
        transData.Data = transData.GetTransData7(theLog);

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

                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(33).Split(',')[0].Trim()), countries, theLog);
                        }

                        else if (transCode != "SA")
                        {
                            countryFound = countries.Contains(Convert.ToInt32(transCode.Substring(3).Trim()), countries, theLog);
                        }
                    }

                    switch (transCode.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SI":

                            if (countryFound)
                            {
                                Console.WriteLine("\tOK, country found");
                                theLog.displayThis("\tOK, country found");
                                //numberOfCountriesVisited = 0;//resetsnumber of nodes visited to 0 so its starts again for the next transaction
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with  that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "SA"://this uses inorder traversal
                      
                             Console.WriteLine("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            theLog.displayThis("   ID CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                            countries.GetAll(countries, theLog);
                            Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                            theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                            break;

                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(Convert.ToInt16(transCode.Split('(')[1].Split(',')[0]), transCode.Substring(3), countries, theLog);
                                Console.WriteLine("\tOK, country inserted");
                                theLog.displayThis("\tOK, country inserted");
                                //numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, another country has that id");
                                theLog.displayThis("\tSORRY, another country has that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        case "DI"://static delete
                            if (countryFound)
                            {
                                countries.Remove(Convert.ToInt32(transCode.Split(' ')[1]), countries, theLog);
                                Console.WriteLine("\tOK, country deleted");
                                theLog.displayThis("\tOK, country deleted");
                               // numberOfCountriesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that id");
                                theLog.displayThis("\tSORRY, no country with that id");
                                //numberOfCountriesVisited = 0;
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data");

                            break;
                }
            }
        }
    }

    //public void TransDataProcessing4(TheLog theLog, CountryDataTable countries)
    //{
    //    theLog.displayThis("CODE STATUS > UserApp started");
    //    numberOfCountriesVisited = 0;
    //    transData.Data = transData.GetTransData4(theLog);

    //    foreach (string transCode in transData.Data)
    //    {
    //        if (transCode != null)
    //        {
    //            Console.WriteLine(transCode);
    //            theLog.displayThis(transCode);
    //            ++count;

    //            if (transCode.Length < 3 && transCode != "SA")
    //            {
    //                countryFound = false;
    //            }

    //            else
    //            {
    //                if (transCode.Split(' ')[0] == "IN")
    //                {

    //                    countryFound = countries.Contains((System.Text.RegularExpressions.Regex.Replace(transCode.Split(',')[1], "'", "")), ref numberOfCountriesVisited, countries);
    //                }

    //                else if (transCode != "SA")
    //                {
    //                    countryFound = countries.Contains(transCode.SuCountriesring(3), ref numberOfCountriesVisited, countries);
    //                }
    //            }

    //            switch (transCode.Split(' ')[0])
    //            {
    //                case "SN":


    //                    if (countryFound)
    //                    {
    //                        Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tOK, country found\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    else
    //                    {
    //                        Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    break;

    //                case "SA":

    //                    //theLog.StatusUpdate("Snapshot started", count);
    //                    Console.WriteLine("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
    //                    theLog.displayThis("   CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
    //                    countries.Inorder(countries.CountryDataTable[countries.RootPointer], theLog);
    //                    Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
    //                    theLog.displayThis("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

    //                    break;

    //                case "IN":


    //                    if (!countryFound)
    //                    {
    //                        countries.Add(transCode.Split('(')[1], countries);
    //                        Console.WriteLine("\tOK, country inserted\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tOK, country inserted\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    else
    //                    {
    //                        Console.WriteLine("\tSORRY, country data already exists\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tSORRY, country data already exists\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    break;

    //                case "DN"://static delete
    //                    if (countryFound)
    //                    {
    //                        countries.Remove(transCode.SuCountriesring(3), countries);
    //                        Console.WriteLine("\tOK, country deleted\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tOK, country deleted\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    else
    //                    {
    //                        Console.WriteLine("\tSORRY, invalid country name\n\t\t>> {0} nodes visited", numberOfCountriesVisited);
    //                        theLog.displayThis("\tSORRY, invalid country name\n\t\t>> " + numberOfCountriesVisited + " nodes visited");
    //                        numberOfCountriesVisited = 0;
    //                    }

    //                    break;

    //                default:

    //                    Console.WriteLine("\tSORRY, invalid transaction data\n\t\t>> {0} nodes visited", numberOfCountriesVisited);

    //                    break;
    //            }
    //        }
    //    }
    //}
}