/*This is the the Log object used to open, read from/write to and close the Log file
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
    private string filePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\TheLog.txt";//path to the file on my PC

     public void Open()//opens/creates/overwrites the log file or 
     {
         file = new StreamWriter(filePath, false);
         displayThis("FILE STATUS > TheLog FILE opened");
     }    

    public void FinishUp()//closes the log file
    {
        displayThis("FILE STATUS > TheLog FILE closed");
        file.Close();
    }

    public void displayThis(string strings)//used to write to the log file
    {        
        file.WriteLine(strings);
    }
}