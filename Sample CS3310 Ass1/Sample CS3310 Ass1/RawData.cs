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

    public string[] GetDataA2(TheLog theLog)//method to get data from the rawdata samplefile
    {

        reader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\RawDataA2.txt");//opens the file
        theLog.displayThis("FILE STATUS > RawDataA2 FILE opened"); //updates the file status inthe log file      
        data = File.ReadAllLines("RawDataA2.txt");//reads thefile
        FinishUp("a2", theLog);//closes the file
    
        return data;  //the raw data is returned                                
    }

    //public string[] GetDataAll(TheLog theLog)//same as previous method but uses rawdataAll file
    //{

    //    reader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310Ass1\CS3310Ass1\bin\Debug\RawDataAll.txt");

    //    theLog.displayThis("FILE STATUS > RawDataAll FILE opened");

    //    data = File.ReadAllLines("RawDataAll.txt");
    //    FinishUp("all", theLog);
    //    return data;
    //}

    public void FinishUp(string rawDataFile, TheLog theLog)//this just updates the log file status and closes the rawdata files
    {
        if(rawDataFile == "a2")
        {
            
            theLog.displayThis("FILE STATUS > RawDataA2 FILE closed");
            reader.Close();            
        }

        //if (rawDataFile == "all")
        //{
           
        //    theLog.displayThis("FILE STATUS > RawDataAll FILE closed");
        //    reader.Close();
        //}
    }
}