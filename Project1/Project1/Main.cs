/*This is the Main Program used to test the functionality of UserApp and DBAccess/Handler
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.Data;                          // for database purposes
using MySql.Data;                           
using MySql.Data.MySqlClient;

public class MainProgram
{
    public static void Main()
    {
        TheLog theLog = new TheLog();           //objects
        TransData transData = new TransData();
        UserApp userApp = new UserApp();
        string transactionData = " ";//used as a recipient for transactions from the wolrdtrans file
        int transNum = 0;//to keep track of the query number
        string password = "gospel7";          // my connection password

        //connection string
        string connStr = "source=localhost;user=martin;port = 3306; database = martindb; Password= " + password + ";";

        MySqlConnection conn = new MySqlConnection(connStr);//new connection

        theLog.Open();
        theLog.displayThis("Connecting to MySQL...");

        try
        {
            conn.Open();//open the connection
            theLog.displayThis("OK, the DB Connection is OPENED\n");

            Console.WriteLine("See WorldLogFile.txt in top-level project folder");
        }

        catch (Exception ex) // catching any exceptions
        {
            theLog.displayThis("\r\nERROR, DB Connection didn't work - no trans done");
            theLog.displayThis(ex.ToString());
            Console.WriteLine("ERROR, DB Connection didn't work - no trans done");
        }

        while(transactionData != "x")//loops through until the end of the transaction file
        {
            transactionData = transData.GetTransData();//obtaining the query

            if(transactionData != "")
            {
                ++transNum;

                switch(transactionData.Split(' ')[0])//choosing the handler in UserApp
                {
                    case "SELECT":
                        userApp.SelectHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "I":
                        userApp.InsertHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "D":
                        userApp.DeleteHandler(transactionData, transNum, theLog, conn);
                        break;

                    case "UPDATE":
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