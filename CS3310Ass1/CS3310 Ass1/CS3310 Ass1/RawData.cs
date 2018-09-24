using System;
using System.IO;
using System.Linq;

public class RawData
{
    private string[] data;
    private TheLog theLog = new TheLog();
    private StreamWriter files = new StreamWriter(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310 Ass1\CS3310 Ass1\bin\Debug\RawDataAll.txt");
    //// This mutator method sets a value for the radius
    //public void SetData(string[] Data)
    //{

    //    data = Data;

    //}


    // This accessor method returns the value of the radius
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

    //public string[] RawData
    //{
    //    get
    //    {
    //        return data;
    //    }

    //    set
    //    {
    //        data = value;
    //    }
    //}

    public string[] GetData()
    {
        File.Open("RawDataAll.txt", FileMode.Open);
        theLog.StatusUpdate("RawData FILE opened", 0);
        string[] file = File.ReadAllLines("RawDataAll.txt");
        theLog.StatusUpdate("RawData FILE closed", 0);
        files.Close();
        return file;
        //data = File.ReadAllLines(file);                           
    }
}