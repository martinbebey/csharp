/*This is the Main Program used to test the functionality of UserApp and DBAccess/Handler
 * 
 * 
 * 
 */

using System;
using System.Data;                          // for database purposes
using MySql.Data;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types;

public class MainProgram
{
    public static void Main()
    {
        TheLog theLog = new TheLog();           //objects
        TransData transData = new TransData();
        UserApp userApp = new UserApp();
        string transactionData = " ";//used as a recipient for transactions from the wolrdtrans file
        int transNum = 0;//to keep track of the query number
        //string password = "password";          // my connection password

        //connection string
        //string connStr = "Data Source=ORCL;User Id=system;Password=password";

        OracleConnection conn = new OracleConnection();//new connection
        conn.ConnectionString = "Data Source= localhost;User Id=system;Password=password;";

        theLog.Open();
        theLog.displayThis("Connecting to ORACLE...");

        try
        {
            conn.Open();//open the connection
            theLog.displayThis("OK, the DB Connection is OPENED\n");

            Console.WriteLine("See LogFile.txt in top-level project folder");
        }

        catch (Exception ex) // catching any exceptions
        {
            theLog.displayThis("\r\nERROR, DB Connection didn't work - no trans done");
            theLog.displayThis(ex.ToString());
            Console.WriteLine("ERROR, DB Connection didn't work - no trans done");
        }

        while (transactionData != "x")//loops through until the end of the transaction file
        {
            transactionData = transData.GetTransData();//obtaining the query

            if (transactionData != "")
            {
                ++transNum;

                switch (transactionData.Split(' ')[0])//choosing the handler in UserApp
                {
                    case "SELECT":
                        userApp.SelectHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "Add":
                        userApp.InsertHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "Delete":
                        userApp.DeleteHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "Update":
                        userApp.UpdateHandler(transactionData, transNum, theLog, conn);
                        break;

                    default:
                        break;
                }
            }

        }

        conn.Close();//Closing the connection
        theLog.FinishUp();//closing the log file
        Console.ReadKey();
    }
}