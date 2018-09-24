/*This is the UserApp class used to format queries and send them over to the DBHandler/Access class
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.Data;                          // for database purposes
using MySql.Data;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types;

public class UserApp
{
    private DBAccess dbAccess = new DBAccess();//dbHandler object

    //**********************************************************************************************************************************

    //handling select statements
    public void SelectHandler(string transactionData, int transNum, TheLog theLog, OracleConnection connection)
    {
        dbAccess.RetrieveData(transactionData, transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling delete statements
    public void DeleteHandler(string transactionData, int transNum, TheLog theLog, OracleConnection connection)
    {
        transactionData = "DELETE * FROM Product WHERE ProductID =  " + transactionData.Split(' ')[1] + ";";

        dbAccess.ChangeData(transactionData, "d", transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling insert statements
    public void InsertHandler(string transactionData, int transNum, TheLog theLog, OracleConnection connection)
    {
        
        transactionData = "INSERT INTO Product (ProductID, ProductName, Price, SupplierID) VALUES ('" + transactionData.Split(' ')[1] + "', '" + transactionData.Split(' ')[2].ToUpper() + "', '" + transactionData.Split(' ')[3] + "', '" + transactionData.Split(' ')[4] + "');";

        dbAccess.ChangeData(transactionData, "i", transNum, theLog, connection);
    }

    //**********************************************************************************************************************************

    //handling insert statements
    public void UpdateHandler(string transactionData, int transNum, TheLog theLog, OracleConnection connection)
    {
        transactionData = "UPDATE PRODUCT SET QuanInStock = QuanInStock + " + transactionData.Split(' ')[2] + " WHERE ProductID = " + transactionData.Split(' ')[1] + ";";

        dbAccess.ChangeData(transactionData, "u", transNum, theLog, connection);
    }
}