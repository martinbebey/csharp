/*This is the Main program used to test the functionality of the other 2 procedural classes and 5 objects
 * 
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using Countries;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CountriesOfTheWorld
{
    public static void Main()
    {
        string filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\TheLog.txt";//path to the log file on my PC
        File.Delete(filePath);//deletes the log file if it exists
        filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\CountryData.bin";//the binary file
        File.Delete(filePath); //deletes the bin file if it exists
        Setup setup = new Setup();
        UserApp userApp = new UserApp(setup);
        setup.CountryDataTableBuilder();// fills in the bin file with raw data

        for (int i = 8; i <= 8; ++i)//for loop from 8 to 8 to process transaction data
        {
            Console.WriteLine("\nProcessing transactions from the file TransData" + i + ".txt");
            userApp.TransDataProcessing(i);
        }

        setup.FinishUp();
        Console.ReadLine();//holds output screen opened for viewing purposes
    }
}

