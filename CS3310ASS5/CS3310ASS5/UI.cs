/*This is the UI class controlling access to the city pair files and updating the log file
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;

public class UI
{
    private string cityPairs, cityPairsFilePath;
    private StreamReader cityPairsFileReader;
    private TheLog theLog;
    private int z;// used in loops

    //**********************************************************************************************************************************

    public UI(string fileNameSuffix, SetupUtility setup)// constructor
    {
        cityPairsFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "CityPairs.txt";//path to the file on my PC
        cityPairsFileReader = new StreamReader(cityPairsFilePath);
        theLog = setup.TheLog;
    }

    //**********************************************************************************************************************************

    public string GetCityPairs(string fileNameSuffix)
    {
        if (!cityPairsFileReader.EndOfStream)
        {
            cityPairs = cityPairsFileReader.ReadLine();// reads a line in the file
            return cityPairs;
        }

        else
        {
            FinishUp(fileNameSuffix);// closes  file
            return "x";//returns "x" to indicate the end of the file has been reached
        }
    }

    //**********************************************************************************************************************************

    public void WriteThis(string strings)//Writes to the log
    {
        theLog.displayThis(strings);
    }

    //**********************************************************************************************************************************

    public void FinishUp(string fileNameSuffix)//closes citypair and log files
    {
        cityPairsFileReader.Close();

        if (fileNameSuffix == "Other")
        {
            ++z;

            if (z == 2)
            {
                theLog.displayThis("FILE STATUS > TheLog FILE closed");
                theLog.FinishUp();
            }
        }
    }
}