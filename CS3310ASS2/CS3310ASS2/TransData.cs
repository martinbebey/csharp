/*This is the TransData object used to access the transdata files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Linq;

public class TransData
{
    private string transData;//array of transData obtained from transdata files
    private StreamReader files;//a file reader

    //**********************************************************************************************************************************

    public TransData(TheLog theLog, int transDataFileNumber)//constructor opens the file
    {
        files = new StreamReader(@"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\TransData" + transDataFileNumber + ".txt"); //opens transdata file
        theLog.displayThis("FILE STATUS > TransData" + transDataFileNumber + " FILE opened\n"); //updates the file status inthe log file
    }

    //**********************************************************************************************************************************

    public string Data//public accessor for the private field data
    {
        get
        {
            return transData;
        }

        set
        {
            transData = value;
        }
    }

    //**********************************************************************************************************************************

    public string GetTransData(TheLog theLog, int transDataFileNumber)//works with the transData1 file
    {
        if (!files.EndOfStream)
        {
            transData = files.ReadLine();// reads the next line in the file
            return transData;//returns the transData for processing one line at a time
        }

        else
        {
            FinishUp(transDataFileNumber, theLog);// closes transdata file
            return "x";//returns x to indicate the end of the file
        }
        

    }

    //**********************************************************************************************************************************

    public void FinishUp(int transDataFileNumber, TheLog theLog)//closes the transData files
    {
        theLog.displayThis("FILE STATUS > TransData" + transDataFileNumber + " FILE closed");
        files.Close();
    }
}