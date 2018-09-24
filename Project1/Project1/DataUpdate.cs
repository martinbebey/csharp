//// CLASS OF STATIC METHODS:  DataUpdate      used by PROGRAM:  UseMySqlDb
//// AUTHOR:  D. Kaminski
//// *************************************************************************************

//using System;
//using System.IO;
//using System.Text;

//using System.Data;                          // NOTE THIS
//using MySql.Data;                           // NOTE THIS
//using MySql.Data.MySqlClient;               // NOTE THIS

//namespace UseMySqlDb
//{
//    class DataUpdate
//    {
//        private StringBuilder stringBuilder = new StringBuilder();
//        // -----------------------------------------------------------------------------
//        public void DoInsert(MySqlConnection conn, TheLog theLog, int transNum, string sql)
//        {
//            //string sql = "INSERT INTO Country (Name, HeadOfState, Continent) " +
//            //    "VALUES ('Disneyland','Mickey Mouse', 'North America')";

//            stringBuilder.AppendFormat("\r\nSQL ({0}): {1}", transNum, sql);
//            theLog.displayThis(stringBuilder.ToString());
//            stringBuilder.Clear();

//            MySqlCommand cmd = new MySqlCommand(sql, conn);

//            try
//            {
//                cmd.ExecuteNonQuery();
//                theLog.displayThis("\r\nOK, INSERT done");
//            }
//            catch (Exception ex)
//            {
//                stringBuilder.AppendFormat("\r\nERROR on {0}, QUERY not done", transNum);
//                theLog.displayThis(stringBuilder.ToString());
//                stringBuilder.Clear();
//                theLog.displayThis(ex.ToString());
//                Console.WriteLine("ERROR on {0}, INSERT not done", transNum);
//            }
//        }
//        // -----------------------------------------------------------------------------
//        public void DoUpdate(MySqlConnection conn, TheLog theLog, int transNum, string sql)
//        {
//            //string sql = "UPDATE Country SET HeadOfState = 'Barack Obama'" +
//            //    "WHERE Name = 'United States'";

//            stringBuilder.AppendFormat("\r\nSQL ({0}): {1}", transNum, sql);
//            theLog.displayThis(stringBuilder.ToString());
//            stringBuilder.Clear();

//            MySqlCommand cmd = new MySqlCommand(sql, conn);

//            try
//            {
//                cmd.ExecuteNonQuery();
//                theLog.displayThis("\r\nOK, UPDATE done");
//            }
//            catch (Exception ex)
//            {
//                stringBuilder.AppendFormat("\r\nERROR on {0}, QUERY not done", transNum);
//                theLog.displayThis(stringBuilder.ToString());
//                stringBuilder.Clear();
//                theLog.displayThis(ex.ToString());
//                Console.WriteLine("ERROR on {0}, UPDATE not done", transNum);
//            }
//        }
//        // -----------------------------------------------------------------------------
//        public void DoDelete(MySqlConnection conn, TheLog theLog, int transNum, string sql)
//        {
//            //string sql = "DELETE FROM Country WHERE Name = 'Disneyland'";

//            stringBuilder.AppendFormat("\r\nSQL ({0}): {1}", transNum, sql);
//            theLog.displayThis(stringBuilder.ToString());
//            stringBuilder.Clear();

//            MySqlCommand cmd = new MySqlCommand(sql, conn);

//            try
//            {
//                cmd.ExecuteNonQuery();
//                theLog.displayThis("\r\nOK, DELETE done");
//            }
//            catch (Exception ex)
//            {
//                stringBuilder.AppendFormat("\r\nERROR on {0}, QUERY not done", transNum);
//                theLog.displayThis(stringBuilder.ToString());
//                stringBuilder.Clear();
//                theLog.displayThis(ex.ToString());
//                Console.WriteLine("ERROR on {0}, DELETE not done", transNum);
//            }
//        }
//        // -----------------------------------------------------------------------------
//    }
//}





