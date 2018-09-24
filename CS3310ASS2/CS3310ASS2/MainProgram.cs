/*This is the Main program used to test the functionality of the other 2 procedural classes and 4 objects
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
        string filePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310Ass1\CS3310Ass1\bin\Debug\TheLog.txt";//path to the file on my PC
        File.Delete(filePath);//deletes the bin file if it exists
        Setup setup = new Setup();
        UserApp userApp = new UserApp(setup);
        setup.CountryDataTableBuilder();// fills in the bin file with raw data

        for (int i = 5; i <= 7; ++i)//for loop from 5 to 7 to process various transData files
        {
            Console.WriteLine("Processing transactions from the file TransData" + i + ".txt");
            userApp.TransDataProcessing(i);
        }

        Console.ReadKey();//holds output screen opened for vewing purposes
    }
}

