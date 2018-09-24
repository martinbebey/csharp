/*This is the userAPP Procedural class used to process the country datatable according to the transData obtained from the transdatafiles
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//Using namespaces
using System.IO;
using System.Linq;
using System.Text;
using Countries;
using Country;

public class UserApp
{
    private int count, countryRRN, dataRecordsRead, numberOfDataRecordsRead, numberOfIndexNodesVisited, i = 0;//count used to count the number of transactions processed and i is just used in loops
    private bool countryFound;//determines whether or not a country exists in the country table
    private TransData transData;//transdata object instance used to access transdata files
    private TheLog theLog;
    private CountryDataTable countries;
    private CountryIndex country = new CountryIndex();
    private StringBuilder stringBuilder = new StringBuilder();
    private short id, result;
    private Node node;
    private StreamReader indexFileReader;
    private string[] indexFileRecords;//used to restore the index table
    private string code, countryInfo, indexFilePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\CountryIndex.txt";

    //*******************************************************************************************************************************************************************************************

    public UserApp(Setup setup)//constructor
    {
        countries = setup.Countries;
        theLog = setup.TheLog;
        count = numberOfDataRecordsRead = numberOfIndexNodesVisited = 0;
    }

    //***********************************************************************************************************************************

    public void RestoreIndex(CountryIndex country)//function used to restore the index table from the index table file
    {
        indexFileReader = new StreamReader(indexFilePath);
        theLog.displayThis("FILE STATUS > IndexBackup FILE opened");
        indexFileRecords = File.ReadAllLines(indexFilePath);        
        theLog.displayThis("FILE STATUS > IndexBackup FILE closed");
        indexFileReader.Close();

        foreach(string indexTableRecord in indexFileRecords)
        {
            node = new Node();
            node.CountryCode = indexTableRecord.Split(',')[0];

            if (indexTableRecord.Split(',')[1] != "")
            {
                node.DRP = Convert.ToInt32(indexTableRecord.Split(',')[1]);
            }

            else
            {
                node.DRP = 0;
            }

            node.Link = Convert.ToInt32(indexTableRecord.Split(',')[2]);
            country.LinkedList[i] = node;
            ++i;
        }
    }

    //**********************************************************************************************************************************

    //this basically processes transdata files
    public void TransDataProcessing(int transDataFileNumber)
    {
            theLog.displayThis("CODE STATUS > UserApp started");

        RestoreIndex(country);        
        transData = new TransData(theLog, transDataFileNumber);
        transData.Data = " ";
        count = 0;

        while (transData.Data != "x")//loop to process transactions
        {
            transData.Data = transData.GetTransData(theLog, transDataFileNumber);//gets transdata from the transdata file
           
            if (transData.Data != "x")
            {
                if (transData.Data != null && transData.Data != "")
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

                    //this if/else statement ensure the transaction code is in a correct format for processing
                    if (transData.Data.Length < 3 && transData.Data != "SA")//no SA command in this assignment
                    {
                        countryFound = false;
                    }

                    else
                    {
                        if (transData.Data.Split(' ')[0] == "IN")
                        {
                            if (short.TryParse(transData.Data.Substring(3).Trim(), out result))
                            {
                                id = Convert.ToInt16(transData.Data.Substring(33).Split(',')[0].Trim());
                                countryFound = countries.Contains(id, countries, theLog, ref numberOfDataRecordsRead, ref countryInfo, false);
                            }

                            else if (transData.Data.Substring(3).Trim().Length < 4)
                            {
                                code = transData.Data.Split('\'')[1].Trim();
                                countryFound = country.Contains(country.LinkedList, code, ref numberOfDataRecordsRead, ref numberOfIndexNodesVisited, ref countryRRN);
                            }
                        }

                        else if (transData.Data != "SA")//no SA command in this assignment
                        {
                            if (short.TryParse(transData.Data.Substring(3).Trim(), out result))
                            {
                                id = Convert.ToInt16(transData.Data.Substring(3).Trim());
                                countryFound = countries.Contains(id, countries, theLog, ref numberOfDataRecordsRead, ref countryInfo, false);
                            }

                            else if (transData.Data.Substring(3).Trim().Length < 4)
                            {
                                code = transData.Data.Substring(3).Trim();
                                countryFound = country.Contains(country.LinkedList, code, ref numberOfDataRecordsRead, ref numberOfIndexNodesVisited, ref countryRRN);
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
                                Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited\n", numberOfDataRecordsRead);
                                theLog.displayThis("\tOK, country found\t\t\t\t\t" + countryInfo + "\n\t\t>> " + numberOfDataRecordsRead + " data records read\n");
                                numberOfDataRecordsRead = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that id\n\t\t>> {0} nodes visited\n", numberOfDataRecordsRead);
                                theLog.displayThis("\tSORRY, no country with that id\n\t\t>> " + numberOfDataRecordsRead + " data records read\n");
                                numberOfDataRecordsRead = 0;
                            }

                            break;

                        case "SC":

                            if (countryFound)
                            {
                                dataRecordsRead = numberOfDataRecordsRead;
                                numberOfDataRecordsRead = 0;
                                countries.Contains(Convert.ToInt16(countryRRN), countries, theLog, ref numberOfDataRecordsRead, ref countryInfo, true);
                                Console.WriteLine("\tOK, country found\n\t\t>> {0} nodes visited", numberOfIndexNodesVisited);
                                theLog.displayThis("\tOK, country found\t\t\t\t\t" + countryInfo + "\n\t\t>> " + numberOfIndexNodesVisited + " index nodes visited");
                                Console.WriteLine("\t\t   {0} data records read", dataRecordsRead);
                                theLog.displayThis("\t\t   " + dataRecordsRead + " data records read\n");
                                numberOfDataRecordsRead = 0;
                                numberOfIndexNodesVisited = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, no country with that code\n\t\t>> {0} nodes visited", numberOfIndexNodesVisited);
                                theLog.displayThis("\tSORRY, no country with that code\n\t\t>> " + numberOfIndexNodesVisited + " index nodes visited");
                                Console.WriteLine("\t\t   {0} data records read\n", numberOfDataRecordsRead);
                                theLog.displayThis("\t\t   " + numberOfDataRecordsRead + " data records read\n");
                                numberOfDataRecordsRead = 0;
                                numberOfIndexNodesVisited = 0;
                            }

                            break;

                        case "DC":

                            if (countryFound)
                            {
                                country.Remove(id, country, theLog);
                            }

                            else
                            {
                                country.Remove(id, country, theLog);
                            }

                            break;


                        case "IN":


                            if (!countryFound)
                            {
                                countries.Add(id, transData.Data.Substring(3), countries, theLog, ref numberOfDataRecordsRead);
                                Console.WriteLine("\tOK, country inserted\n");
                                theLog.displayThis("\tOK, country inserted\n");
                                numberOfDataRecordsRead = 0;
                            }

                            else
                            {
                                Console.WriteLine("\tSORRY, another country has that id\n");
                                theLog.displayThis("\tSORRY, another country has that id\n");
                            }

                            break;

                        case "IC"://insert by code


                            if (!countryFound)
                            {
                                country.InsertCodeInIndex(theLog);
                            }

                            else
                            {
                                country.InsertCodeInIndex(theLog);
                            }

                            break;

                        case "DI"://delete data by ID
                            if (countryFound)
                            {
                                countries.Remove(id, countries, theLog);

                            }

                            else
                            {
                                countries.Remove(id, countries, theLog);
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data");

                            break;
                    }
                }
            }
        }

        theLog.displayThis("CODE STATUS > UserApp finished - " + count + " transactions processed");
        count = 0;
        countries.FinishUp(countries, theLog, false);
    }
}