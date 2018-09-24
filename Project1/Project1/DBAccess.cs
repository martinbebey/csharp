/*This is the DBHandler program that handles queries and results from/to the DB
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.Data;                         // for database purposes
using MySql.Data;                           
using MySql.Data.MySqlClient;
using System.Text; 

public class DBAccess
{
    private StringBuilder stringBuilder = new StringBuilder();//to build strings

    //**********************************************************************************************************************************

    //to handle select statements
    public void RetrieveData(string selectQuery, int transNum, TheLog theLog, MySqlConnection connection)
    {
        stringBuilder.AppendFormat("\r\nSQL ({0}): {1}", transNum, selectQuery);
        theLog.displayThis(stringBuilder.ToString());
        stringBuilder.Clear();

        MySqlCommand cmd = new MySqlCommand(selectQuery, connection);

        try
        {
            if (selectQuery.Split(' ')[1] != "COUNT(*)")//dealing with multiple results
            {
                MySqlDataReader rdr = cmd.ExecuteReader();//the db reader

                while (rdr.Read())
                {
                    if (rdr.FieldCount > 1)//more than  1 column in result
                    {
                        stringBuilder.AppendFormat("{0,-14}:  {1,10:N0}", rdr[0], rdr[1]);
                    }

                    else// only 1 column in result (to prevent out of range exception)
                    {
                        stringBuilder.AppendFormat("{0,-14}", rdr[0]);
                    }

                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                rdr.Close();//closing the reader
            }

            else//dealing with single value result
            {
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    int number = Convert.ToInt32(result);

                    if (selectQuery.Split(' ')[selectQuery.Split(' ').Length - 1] == "Country")
                    {
                        theLog.displayThis("\r\n" + number + " countries in the World database");
                    }

                    else
                    {
                        theLog.displayThis("\r\n" + number + " languages in the World database");
                    }
                }
            }

         

        }

        catch (Exception ex) // catching any exceptions
        {
            stringBuilder.AppendFormat("\r\nERROR on {0}, QUERY not done", transNum);
            theLog.displayThis(stringBuilder.ToString());
            stringBuilder.Clear();
            theLog.displayThis(ex.ToString());
            Console.WriteLine("ERROR on {0}, QUERY not done", transNum);
        }

    }

    //**********************************************************************************************************************************

    //handling updates, deletes and inserts
    public void ChangeData(string iduQuery, string queryType, int transNum, TheLog theLog, MySqlConnection connection)
    {
        stringBuilder.AppendFormat("\r\nSQL ({0}): {1}", transNum, iduQuery);
        theLog.displayThis(stringBuilder.ToString());
        stringBuilder.Clear();

        MySqlCommand cmd = new MySqlCommand(iduQuery, connection);

        try
        {
            cmd.ExecuteNonQuery();

            //writing out corresponding messages 
            if (queryType == "u")
            {
                theLog.displayThis("\r\nOK, UPDATE done");
            }

            else if(queryType == "i")
            {
                theLog.displayThis("\r\nOK, INSERT done");
            }

            else
            {
                theLog.displayThis("\r\nOK, DELETE done");
            }

        
        }

        catch (Exception ex)//catching any exceptions
        {
            stringBuilder.AppendFormat("\r\nERROR on {0}, QUERY not done", transNum);
            theLog.displayThis(stringBuilder.ToString());
            stringBuilder.Clear();
            theLog.displayThis(ex.ToString());
            Console.WriteLine("ERROR on {0}, UPDATE not done", transNum);
        }
    }
}