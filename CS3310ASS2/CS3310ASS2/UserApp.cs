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
    private int count;//count used to count the number of transactionsprocessed 
    private bool countryFound;//determines whether or not a country exists in the country table
    private TransData transData;//transdata object instance used to access transdata files
    private TheLog theLog;
    private CountryDataTable countries;
    string countryInfo;
    StringBuilder stringBuilder = new StringBuilder();
    private short id, result;

    //**********************************************************************************************************************************

    public UserApp(Setup setup)//constructor
    {
        countries = setup.Countries;
        theLog = setup.TheLog;
        count = 0;
        countries = setup.Countries;
    }

    //**********************************************************************************************************************************

    //this basically processes transdata1 file or calls other identical methods to process other transdata files respectively
    public void TransDataProcessing(int transDataFileNumber)
    {
        theLog.displayThis("CODE STATUS > UserApp started");
        transData = new TransData(theLog, transDataFileNumber);
        transData.Data = " ";
        count = 0;

        while (transData.Data != "x")//loop to process transactions
        {
            transData.Data = transData.GetTransData(theLog, transDataFileNumber);//gets transdata from the transdata5 file

            if (transData.Data != "x")
            {
                if (transData.Data != null)
                {
                    if (transData.Data.Substring(0, 2) == "IN")
                    {
                        Console.Write("IN {0} {1} {2} {3} {4} {5} {6}", transData.Data.Substring(33).Split(',')[0].Trim(), transData.Data.Split('\'')[1].Trim(), transData.Data.Split('\'')[3].Trim(), transData.Data.Split('\'')[5].Trim(), transData.Data.Split(',')[5].Trim(), transData.Data.Split(',')[7].Trim(), transData.Data.Split(',')[8].Trim());
                        stringBuilder.AppendFormat("IN {0} {1} {2} {3} {4} {5} {6}", transData.Data.Substring(33).Split(',')[0].Trim(), transData.Data.Split('\'')[1].Trim(), transData.Data.Split('\'')[3].Trim(), transData.Data.Split('\'')[5].Trim(), transData.Data.Split(',')[5].Trim(), transData.Data.Split(',')[7].Trim(), transData.Data.Split(',')[8].Trim());
                        theLog.displayThis(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }

                    else if (transData.Data != " ")
                    {                  
                        Console.WriteLine(transData.Data);
                        theLog.displayThis(transData.Data);
                    }

                    ++count;//counts the number of transactions

                    //this if else statement ensure the transaction code is in a correct format for processing
                    if (transData.Data.Length < 3 && transData.Data != "SA")
                    {
                        countryFound = false;
                    }

                    else
                    {
                        if (transData.Data.Split(' ')[0] == "IN")
                        {
                            id = Convert.ToInt16(transData.Data.Substring(33).Split(',')[0].Trim());
                            countryFound = countries.Contains(id, countries, theLog, ref countryInfo);
                        }

                        else if (transData.Data != "SA")
                        {
                            if (short.TryParse(transData.Data.Substring(3).Trim(), out result))
                            {
                                id = Convert.ToInt16(transData.Data.Substring(3).Trim());
                                countryFound = countries.Contains(id, countries, theLog, ref countryInfo);
                            }

                            else
                            {
                                countryFound = false;
                            }
                        }
                    }

                    switch (transData.Data.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SI":

                            if (countryFound)
                            {
                                Console.WriteLine("\tOK, country found\t\t\t\t\t" + countryInfo + "\n");
                                theLog.displayThis("\tOK, country found\t\t\t\t\t" + countryInfo + "\n");
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with  that id\n");
                                theLog.displayThis("\tSORRY, no country with that id\n");
                            }

                            break;

                        case "SA"://this result is sorted by ID

                            Console.WriteLine("   ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");

                            countries.GetAll(countries, theLog);
                            Console.WriteLine("   +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                            theLog.displayThis("   +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                            break;

                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(id, transData.Data.Substring(3), countries, theLog);
                                Console.WriteLine("\tOK, country inserted\n");
                                theLog.displayThis("\tOK, country inserted\n");
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, another country has that id\n");
                                theLog.displayThis("\tSORRY, another country has that id\n");
                            }

                            break;

                        case "DI"://delete data
                            if (countryFound)
                            {
                                countries.Remove(id, countries, theLog, ref countryInfo);
                                Console.WriteLine("\tOK, country deleted\t\t\t\t\t" + countryInfo + "\n");
                                theLog.displayThis("\tOK, country deleted\t\t\t\t\t" + countryInfo + "\n");
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that id\n");
                                theLog.displayThis("\tSORRY, no country with that id\n");
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data\n");

                            break;
                    }
                }
            }
        }

        theLog.displayThis("CODE STATUS > UserApp finished - " + count + " transactions processed");
        count = 0;

        if (transDataFileNumber == 7)
        {            
            countries.FinishUp(countries, theLog, true);
            theLog.FinishUp();//closes the log file
        }
    }
}