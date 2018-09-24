/*This is the raw data object used to get data from the raw data files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */


using System;//using namespaces
using System.IO;
using System.Linq;

public class RawData
{
    private string data;//string of data and a reader
    private StreamReader reader;

    //**********************************************************************************************************************************

    public string Data//property for the data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    //**********************************************************************************************************************************

    public RawData(TheLog theLog)//constructor opens the file
    {
        reader = new StreamReader(@"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\RawDataA2.txt");//opens the file
        theLog.displayThis("FILE STATUS > RawDataA2 FILE opened"); //updates the file status inthe log file
    }

    //**********************************************************************************************************************************

    public string GetDataA2(TheLog theLog)//method to get data from the RawDataA2 file
    {
        if (!reader.EndOfStream)
        {
            data = reader.ReadLine();//reads the next line in the file
            return data;  //the raw data is returned one line at a time 
        }

        else
        {
            FinishUp("A2", theLog);//closes the file
            return "x"; // x is returned to indicate the end of the file
        }
    }

    //**********************************************************************************************************************************

    public void FinishUp(string rawDataFileNumber, TheLog theLog)//this just updates the log file status and closes the rawdata files
    {
        theLog.displayThis("FILE STATUS > RawData" + rawDataFileNumber + " FILE closed");
        reader.Close();
    }
}