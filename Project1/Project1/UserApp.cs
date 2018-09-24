/*This is the UserApp class used to format queries and send them over to the DBHandler/Access class
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.Data;                          // for database purposes
using MySql.Data;                           
using MySql.Data.MySqlClient;

public class UserApp
{
    private DBAccess dbAccess = new DBAccess();//dbHandler object

    //**********************************************************************************************************************************

    //handling select statements
    public void SelectHandler(string transactionData, int transNum, TheLog theLog, MySqlConnection connection)
    {
        dbAccess.RetrieveData(transactionData, transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling delete statements
    public void DeleteHandler(string transactionData, int transNum, TheLog theLog, MySqlConnection connection)
    {
        transactionData = transactionData.Replace("D ", "DELETE FROM ").Replace("|'", " = '");

        if (transactionData.Split('|').Length == 2)
        {
            transactionData = transactionData.Replace("|", " WHERE ");
        }

        dbAccess.ChangeData(transactionData, "d", transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling insert statements
    public void InsertHandler(string transactionData, int transNum, TheLog theLog, MySqlConnection connection)
    {
        transactionData = transactionData.Replace("I ", "INSERT INTO ").Replace("|'", " VALUES ('") + ")";

        if (transactionData.Split('|').Length == 2)
        {
            transactionData = transactionData.Replace("|", " (").Replace(" VALUES", ") VALUES");
        }

        dbAccess.ChangeData(transactionData, "i", transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling insert statements
    public void UpdateHandler(string transactionData, int transNum, TheLog theLog, MySqlConnection connection)
    {
        dbAccess.ChangeData(transactionData, "u", transNum, theLog, connection);
    }
}