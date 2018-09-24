using System;
using BST;

public class WorldCountries
{
    public static void Main()
    {
        char userCommand = ' ';
        int transDataFileChoice;
        Setup setup = new Setup();
        UserApp userApp = new UserApp();
        TheLog theLog = new TheLog();
        BSTree countries = new BSTree();
        theLog.Open();

        while (userCommand != 'e')
        {
            bool buildFromSample = false, buildFromAll = false, processTransData1 = false, processTransData2 = false, processTransData3 = false, processTransData4 = false;
            Console.Write("Press [B] to Build a country table from RawData.txt, [T] for Transdata processing using TransData.txt or [E] to Exit: ");

            while (!char.TryParse(Console.ReadLine().ToLower(), out userCommand))
            {
                Console.Write("Invalid command! Please press [B] to Build a country table from RawDataAll.txt, [T] for Transdata processing using TransData.txt or [E] to Exit: ");
            }

            switch (userCommand)
            {
                case 'b':
                    Console.Write("Press [S],[A] or [B] to build from RawDataSample.txt, RawDataAll.txt or both respectivrely, or {E] to Exit: ");

                    while (!char.TryParse(Console.ReadLine().ToLower(), out userCommand))
                    {
                        Console.Write("Invalid command! Press [S],[A] or [B] to build from RawDataSample.txt, RawDataAll.txt or both respectivrely, or {E] to Exit: ");
                    }

                    while (userCommand != 'a' && userCommand != 'b' && userCommand != 's' && userCommand != 'e')
                    {
                        Console.Write("Invalid command! Press [S],[A] or [B] to build from RawDataSample.txt, RawDataAll.txt or both respectivrely, or {E] to Exit: ");
                        while (!char.TryParse(Console.ReadLine().ToLower(), out userCommand))
                        {
                            Console.Write("Invalid command! Press [S],[A] or [B] to build from RawDataSample.txt, RawDataAll.txt or both respectivrely, or {E] to Exit: ");
                        }
                    }

                    switch (userCommand)
                    {
                        case 'a':
                            buildFromAll = true;
                            Console.WriteLine("Bulding from RawDataAll.txt");
                            setup.CountryDataTableBuilder(buildFromAll, buildFromSample, theLog, countries);
                            break;

                            case 's':

                            buildFromSample = true;
                            Console.WriteLine("Bulding from RawDataSample.txt");
                            setup.CountryDataTableBuilder(buildFromAll, buildFromSample, theLog, countries);
                            break;

                        case 'b':

                            buildFromSample = buildFromAll = true;
                            Console.WriteLine("Bulding from both RawDataAll.txt and RawDataSample.txt");
                            setup.CountryDataTableBuilder(buildFromAll, buildFromSample, theLog, countries);
                            break;

                        case 'e':

                            Console.Write("Now closing...");

                            break;

                        default:
                            break;
                    }
                  
                    break;

                case 't':
                    Console.Write("Press [1], [2], [3], [4] or [5] to process data from the corresponding TransData.txt files or from all of them respectively, or [0] to exit: ");
                    while(!int.TryParse(Console.ReadLine(), out transDataFileChoice))
                    {
                        Console.Write("Invalid Command! Press [1], [2], [3], [4] or [5] to process data from the corresponding TransData.txt files or from all of them respectively, or [0] to exit: ");
                    }

                    while (transDataFileChoice != 1 && transDataFileChoice != 2 && transDataFileChoice != 3 && transDataFileChoice != 4 && transDataFileChoice != 0)
                    {
                        Console.Write("Invalid command! Press [1], [2], [3], [4] or [5] to process data from the corresponding TransData.txt files or from all of them respectively, or [0] to exit: ");
                        while (!int.TryParse(Console.ReadLine(), out transDataFileChoice))
                        {
                            Console.Write("Invalid command! Press [1], [2], [3], [4] or [5] to process data from the corresponding TransData.txt files or from all of them respectively, or [0] to exit: ");
                        }
                    }
                    switch (transDataFileChoice)
                    {
                        case 1:
                            processTransData1 = true;
                            Console.WriteLine("Processing TransData1.txt");
                            userApp.TransDataProcessing(processTransData1, processTransData2, processTransData3, processTransData4, theLog, countries);
                            break;

                        case 2:

                            processTransData2 = true;
                            Console.WriteLine("Processing TransData2.txt");
                            userApp.TransDataProcessing(processTransData1, processTransData2, processTransData3, processTransData4, theLog, countries);
                            break;

                        case 3:

                            processTransData3 = true;
                            Console.WriteLine("Processing TransData3.txt");
                            userApp.TransDataProcessing(processTransData1, processTransData2, processTransData3, processTransData4, theLog, countries);
                            break;

                        case 4:

                            processTransData4 = true;
                            Console.WriteLine("Processing TransData4.txt");
                            userApp.TransDataProcessing(processTransData1, processTransData2, processTransData3, processTransData4, theLog, countries);
                            break;

                        case 5:

                            processTransData1 = processTransData2 = processTransData3 = processTransData4 = true;
                            Console.WriteLine("Processing TransData files 1 through 4");
                            userApp.TransDataProcessing(processTransData1, processTransData2, processTransData3, processTransData4, theLog, countries);
                            break;

                        case 0:

                            userCommand = 'e';
                            Console.Write("Now closing...");

                            break;

                        default:
                            break;
                    }                    

                    break;

                case 'e':

                    Console.Write("Now closing...");

                    break;

                default:

                    Console.Write("Invalid command! ");

                    break;
            }
        }

        theLog.FinishUp();
    }
}