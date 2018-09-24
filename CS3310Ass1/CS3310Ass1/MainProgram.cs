/*This is the Main program used to test the functionality of the other 2 procedural classes and 4 objects
 * it is responsible for opening the log file in the beggining and closing it in the end
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using BST;

public class CountriesOfTheWorld
{
    public static void Main()
    {
        BSTree countryDataTable = new BSTree();
        Setup setup = new Setup();
        UserApp userApp = new UserApp();
        TheLog theLog = new TheLog();
        theLog.Open();//opens the log file

        Console.WriteLine("Building from the file RawDataSample.txt");//prints this to the user
        setup.CountryDataTableBuilder(false, true, theLog,countryDataTable);// builds the country data table using the raw data sample file

        for (int i = 1; i < 4; ++i)//loop used to process transactions from transdata files 1 to 3
        {
            if(i == 1)
            {
                Console.WriteLine("Processing transactions from the file TransData1.txt");
                userApp.TransDataProcessing(true, false, false, false, theLog, countryDataTable);
            }

            if (i == 2)
            {
                Console.WriteLine("Processing transactions from the file TransData2.txt");
                userApp.TransDataProcessing(false, true, false, false, theLog, countryDataTable);
            }

            if (i == 3)
            {
                Console.WriteLine("Processing transactions from the file TransData3.txt");
                userApp.TransDataProcessing(false, false, true, false, theLog, countryDataTable);
            }
           
        }

        countryDataTable.FinishUp(true, theLog, countryDataTable);//calls finishUp in the country data table with a true parameter to launchthe snapshot

        Console.WriteLine("Building from the file RawDataAll.txt");
        setup.CountryDataTableBuilder(true, false, theLog, countryDataTable);//builds/adds to country table using the RawDataAll file

        for (int i = 4; i < 5; ++i)//loop used to process transactions from transdata file 4
        { 
            if (i == 4)
            {
                Console.WriteLine("Processing transactions from the file TransData4.txt");
                userApp.TransDataProcessing(false, false, false, true, theLog, countryDataTable);
            }
        }

        countryDataTable.FinishUp(false, theLog,countryDataTable);//calls finishUp in countryDataTable with false parameter to prevent the snapshot from happening        
        theLog.FinishUp();//finally closes the log file

        Console.ReadKey();//holds output screen opened for vewing purposes
    }
}

