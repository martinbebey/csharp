using System;
using System.IO;
using System.Linq;

public class TheLog
{
    private StreamWriter file;

    public StreamWriter Files
    {
        get
        {
            return file;
        }

        set
        {
            file = value;
        }
    }
    
     public TheLog()
    {
        File.Create(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310 Ass1\CS3310 Ass1\bin\Debug\TheLog.txt");
        file = new StreamWriter(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310 Ass1\CS3310 Ass1\bin\Debug\TheLog.txt");
        File.Open("TheLog.txt", FileMode.Open);
        file.WriteLine("FILE STATUS > TheLog FILE opened");            
    }

    public void StatusUpdate(string status, int numberProcessed)
    {        
        switch(status)
        {
            case "Setup started":
                
                file.WriteLine("CODE STATUS > " + status);     
             
                break;

            case "Setup finished":

                file.WriteLine("CODE STATUS > " + status + " - " + numberProcessed + " countries processed");

                break;

            case "UserApp started":

                file.WriteLine("CODE STATUS > " + status);

                break;

            case "UserApp finished":

                file.WriteLine("CODE STATUS > " + status + " - " + numberProcessed + " transactions processed");

                break;

            case "Snapshot started":

                file.WriteLine("CODE STATUS > " + status);

                break;

            case "Snapshot finished":

                file.WriteLine("CODE STATUS > " + status + " - " + numberProcessed + " nodes displayed");

                break;

            case "RawData FILE opened":

                file.WriteLine("CODE STATUS > " + status);

                break;

            case "RawData FILE closed":

                file.WriteLine("CODE STATUS > " + status);

                break;

            case "TransData FILE opened":

                file.WriteLine("CODE STATUS > " + status);

                break;

            case "TransData FILE closed":

                file.WriteLine("CODE STATUS > " + status);

                break;

            default:
                break;
        }
    }

    public void FinishUp()
    {
        file.WriteLine("FILE STATUS > TheLog FILE closed");
        file.Close();
    }
}