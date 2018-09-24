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
    private string[] data;//string of data and a reader
    private StreamReader reader;
    
    public string[] Data
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

    public string[] GetDataSample(TheLog theLog)//method to get data from the rawdata samplefile
    {

        reader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310Ass1\CS3310Ass1\bin\Debug\RawDataSample.txt");//opens the file
        theLog.displayThis("FILE STATUS > RawDataSample FILE opened"); //updates the file status inthe log file      
        data = File.ReadAllLines("RawDataSample.txt");//reads thefile
        FinishUp("sample", theLog);//closes the file
    
        return data;  //the raw data is returned                                
    }

    public string[] GetDataAll(TheLog theLog)//same as previous method but uses rawdataAll file
    {

        reader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310Ass1\CS3310Ass1\bin\Debug\RawDataAll.txt");

        theLog.displayThis("FILE STATUS > RawDataAll FILE opened");

        data = File.ReadAllLines("RawDataAll.txt");
        FinishUp("all", theLog);
        return data;
    }

    public void FinishUp(string rawDataFile, TheLog theLog)//this just updates the log file status and closes the rawdata files
    {
        if(rawDataFile == "sample")
        {
            
            theLog.displayThis("FILE STATUS > RawDataSample FILE closed");
            reader.Close();            
        }

        if (rawDataFile == "all")
        {
           
            theLog.displayThis("FILE STATUS > RawDataAll FILE closed");
            reader.Close();
        }
    }
}