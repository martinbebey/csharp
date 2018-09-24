/*This is the userAPP Procedural class used to process the country data table and index according to the transData obtained from the transdatafiles
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//Using namespaces
using System.IO;
using System.Linq;
using System.Text;

public class UserApp
{
    private int numberOfTransactions, numberOfNodesRead = 0;
    private TransData transData;
    private TheLog theLog = new TheLog();
    private CountryDataTable countryDataTable;
    private StringBuilder stringBuilder = new StringBuilder();
    private CodeIndex codeIndex;
    private short DRP = 0;
    private string KV, dataRecord;//dataRecord is record obtained from the country data files

    //******************************************************************************************************************************

    //this basically processes transcodes from the transData files
    public void TransDataProcessing(int transDataFileNumber)
    {
        if (transDataFileNumber == 1)
        {
            theLog.Open();
        }

        codeIndex = new CodeIndex(transDataFileNumber);
        countryDataTable = new CountryDataTable(theLog, transDataFileNumber);
        transData = new TransData(theLog, transDataFileNumber);
        Console.WriteLine("\nPROCESSING A4TransData" + transDataFileNumber + "\n");
        theLog.displayThis("\nPROCESSING A4TransData" + transDataFileNumber + "\n");
        transData.Data = " ";
        numberOfTransactions = 0;

        while (transData.Data != "x")//loop to process transactions
        {
            transData.Data = transData.GetTransData(theLog, transDataFileNumber);//gets transdata from the transdata file

            if (transData.Data != "x")//x means end of file is reeached
            {
                if (transData.Data != null && transData.Data != "")
                {
                    if (transData.Data != " ")
                    {
                        Console.WriteLine(transData.Data);
                        theLog.displayThis(transData.Data);
                    }

                    ++numberOfTransactions;//counts the number of transactions
                    KV = transData.Data.Substring(3).Trim();

                    switch (transData.Data.Split(' ')[0])//does various things according to the transaction code
                    {
                        case "SC":

                            codeIndex.SelectByCode(KV, ref numberOfNodesRead, ref DRP);

                            if (DRP > 0)
                            {
                                dataRecord = countryDataTable.GetRecord(DRP).Substring(0, 23);
                                Console.WriteLine(">>> " + dataRecord);
                                Console.WriteLine("   [# nodes read:  " + numberOfNodesRead + "]");
                                theLog.displayThis(">>> " + dataRecord);
                                theLog.displayThis("   [# nodes read:  " + numberOfNodesRead + "]");
                                numberOfNodesRead = 0;
                            }

                            else//if DRP is -1 or 0 =>  the code was not found in the tree
                            {
                                Console.WriteLine(">>> ERROR - code not in index");
                                Console.WriteLine("   [# nodes read:  " + numberOfNodesRead + "]");
                                theLog.displayThis(">>> ERROR - code not in index");
                                theLog.displayThis("   [# nodes read:  " + numberOfNodesRead + "]");
                                numberOfNodesRead = 0;
                            }

                            break;

                        default:

                            Console.WriteLine("\tSORRY, invalid transaction data");

                            break;
                    }
                }
            }
        }

        numberOfTransactions = 0;
        countryDataTable.FinishUp();

        if (transDataFileNumber == 3)
        {
            theLog.FinishUp();
        }
    }
}
