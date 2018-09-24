/*This is the Main program used to test the functionality of the other procedural classe and 4 objects
 * 
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;

public class BTreesDemo
{
    public static void Main()
    {
        string filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS4\CS3310ASS4\bin\Debug\TheLog.txt";//path to the log file on my PC
        File.Delete(filePath);//deletes the log file if it exists
        UserApp userApp = new UserApp();

        for (int i = 1; i <= 3; ++i)//for loop from 1 to 3 to process transaction data
        {            
            userApp.TransDataProcessing(i);//calls userApp sending it file name suffix
        }

        Console.ReadLine();//holds output screen opened for viewing purposes
    }
}

