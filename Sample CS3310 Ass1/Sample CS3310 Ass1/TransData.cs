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
    private string[] transData;//array of transData obtained from transdata files
    private StreamReader files;//a file reader

    public string[] Data//public accessor for the private field data
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

    public string[] GetTransData5(TheLog theLog)//works with the transData1 file
    {
        files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\TransData5.txt"); //opens transdata1 file
        theLog.displayThis("FILE STATUS > TransData5 FILE opened"); //updates the file status in the log file      
        transData = File.ReadAllLines("TransData5.txt");// reads the file
        FinishUp(5, theLog);// closes transdata1 file
        return transData;//returns the transData for processing
                                   
    }

    //procedures below work similarly, using transData2-4 files respectively
    public string[] GetTransData6(TheLog theLog)
    {
        files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\TransData6.txt");
        theLog.displayThis("FILE STATUS > TransData6 FILE opened");
        transData = File.ReadAllLines("TransData6.txt");
        FinishUp(6, theLog);
        return transData;

    }

    public string[] GetTransData7(TheLog theLog)
    {
        files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\TransData7.txt");
        theLog.displayThis("FILE STATUS > TransData3 FILE opened");
        transData = File.ReadAllLines("TransData7.txt");
        FinishUp(7, theLog);
        return transData;

    }

    //public string[] GetTransData4(TheLog theLog)
    //{
    //    files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310Ass1\CS3310Ass1\bin\Debug\TransData4.txt");
    //    theLog.displayThis("FILE STATUS >TransData4 FILE opened");
    //    transData = File.ReadAllLines("TransData4.txt");
    //    FinishUp(4, theLog);
    //    return transData;

    //}

    public void FinishUp(int transDataFileNumber, TheLog theLog)//closes the transData files
    {
        if(transDataFileNumber == 5) 
        {
            theLog.displayThis("FILE STATUS > TransData5 FILE closed");
            files.Close();
        }

        if (transDataFileNumber == 6)
        {
            theLog.displayThis("FILE STATUS > TransData6 FILE closed");
            files.Close();
        }

        if (transDataFileNumber == 7)
        {
            theLog.displayThis("FILE STATUS > TransData7 FILE closed");
            files.Close();
        }

        //if (transDataFileNumber == 4)
        //{
        //    theLog.displayThis("FILE STATUS > TransData4 FILE closed");
        //    files.Close();
        //}
    }
}