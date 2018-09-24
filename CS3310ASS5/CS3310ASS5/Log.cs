/*This is the Log object used by UI for all updates
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//using namespaces
using System.IO;
using System.Linq;

public class TheLog
{
    private StreamWriter file;//a writer used to write to the file
    private string filePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\TheLog.txt";//path to the file on my PC

    public void Open()//opens/creates/overwrites the log file 
    {
        file = new StreamWriter(filePath, false);
        displayThis("FILE STATUS > TheLog FILE opened");
    }

    //**********************************************************************************************************************************

    public void FinishUp()//closes the log file
    {
        //displayThis("FILE STATUS > TheLog FILE closed");
        file.Close();
    }

    //**********************************************************************************************************************************

    public void displayThis(string strings)//used to write to the log file
    {
        file.WriteLine(strings);
    }
}