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
    private string transData;//to store transcodes from the transdata files
    private StreamReader files;//a file reader

    //**********************************************************************************************************************************

    public TransData(TheLog theLog, int transDataFileNumber)
    {
        files = new StreamReader(@"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS4\CS3310ASS4\bin\Debug\A4TransData" + transDataFileNumber + ".txt"); //opens transdata file
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

    public string GetTransData(TheLog theLog, int transDataFileNumber)//gets data from the transdata file and returns it 1 line at a time
    {
        if (!files.EndOfStream)
        {
            transData = files.ReadLine();// reads a line in the file
            return transData;
        }

        else
        {
            FinishUp(transDataFileNumber, theLog);// closes transdata file
            return "x";//returns "x" to indicate the end of the file has been reached
        }

    }

    //********************************************************************************************************************************************************

    public void FinishUp(int transDataFileNumber, TheLog theLog)//closes the transData file
    {
        files.Close();
    }
}