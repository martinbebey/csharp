/*This is the raw data object used to get data from the raw data file
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

    public string Data
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

    public RawData(TheLog theLog)
    {
        reader = new StreamReader(@"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\RawDataA3.txt");//opens the file
        theLog.displayThis("FILE STATUS > RawDataA3 FILE opened"); //updates the file status in the log file
    }

    //**********************************************************************************************************************************

    public string GetDataA3(TheLog theLog)//method to get data from the RawDataA2 file
    {
        if (!reader.EndOfStream)
        {
            data = reader.ReadLine();//reads thefile a line at a time
            return data;
        }

        else
        {
            FinishUp("A3", theLog);//closes the file
            return "x";  //returns "x" to indicate the end of the file has been reached  
        }                  
    }

    //**********************************************************************************************************************************

    public void FinishUp(string rawDataFileNumber, TheLog theLog)//this just updates the log file status and closes the rawdata file
    {
        theLog.displayThis("FILE STATUS > RawData" + rawDataFileNumber + " FILE closed");
        reader.Close();
    }
}