using System;
using System.IO;
using System.Linq;

public class TransData
{
    private string[] transData;
    private TheLog theLog = new TheLog();
    private StreamWriter files = new StreamWriter(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310 Ass1\CS3310 Ass1\bin\Debug\TransData.txt");

    public string[] Data
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

    public string[] GetTransData()
    {
        File.Open("TransData.txt", FileMode.Open);
        theLog.StatusUpdate("TransData FILE opened", 0);
        string[] file = File.ReadAllLines("TransData.txt");
        theLog.StatusUpdate("RawData FILE closed", 0);
        files.Close();
        return file;
        //data = File.ReadAllLines(file);                           
    }
}