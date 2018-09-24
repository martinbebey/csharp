/*This is the TransData object used to access the transdata files
 * 
 * 
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

    public TransData()
    {
        files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2013\Projects\CS5430A5\CS5430A5\bin\Debug\A5UserRequests.txt");//opens transaction file
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

    public string GetTransData()//gets data from the transdata file and returns it 1 line at a time
    {
        if (!files.EndOfStream)
        {
            transData = files.ReadLine();// reads a line in the file
            return transData;
        }

        else
        {
            FinishUp();// closes transdata file
            return "x";//returns "x" to indicate the end of the file has been reached
        }

    }

    //********************************************************************************************************************************************************

    public void FinishUp()//closes the transData file
    {
        files.Close();
    }
}