/* This application lets the user compare various hashing/ collison resolution algorithms, delete and search for a value */
// SU2 2013 COIS 2020H - Assignment 1
//                                                          By M@rtin Bebey (0420751) & Markus Piil

using System;
using System.IO;// to read from text file

public class Assignment1
{
    public static void Main()
    {

        bool selectedAlgorithm = false;//variables to know if user selected an algorithm and table size
        bool validTable = false;
        bool tableSizeEstablished = false;
        bool quitApp = false;//to know when to stop the application

        string[] tempHashTable = new string[0];// a temporary array to store values from the text file

        const string FILENAME = "partslist.txt";//the text file
       

        int algSelectInt = 0;//to know the algorithm selected
        int collisions = 0;//to keep track of the number of collisons
        int[] listSize = new int[0];//the size of the list
        int[] realHashTable = new int[0];//the final hash table containing the part numbers after hashing & collision resolution
        int tableInt = 0;//size of hash table
        int hasher;//the part number

        while (quitApp == false)// loop keeps running application until user wants to quit
        {
            //Display the main menu
            Console.WriteLine("Main Menu: Please choose one of the following numerical options:");
            Console.WriteLine("(1) Choose table size [41 or 53]");
            Console.WriteLine("(2) Choose the algorithm to be used");
            Console.WriteLine("(3) Load values from partslist.txt");
            Console.WriteLine("(4) Print out hash table contents");
            Console.WriteLine("(5) Search for a value");
            Console.WriteLine("(6) Delete a value");
            Console.WriteLine("(7) Quit application");

            //Reads in user's selection, converts to Int32 to use with switch table
            int optSelectInt;
            while (!int.TryParse(Console.ReadLine(), out optSelectInt))
            {
                Console.Write("Invalid value! Please enter an integer between 1-7: ");
            }

            Console.WriteLine("");


            //Runs through a switch block, entering the case connected to the user's input for Main Menu options
            switch (optSelectInt)
            {
                case 1:
                    //Sets bool flags to false, allowing user to redeclare array if so desired
                    validTable = false;
                    tableSizeEstablished = false;

                    //While loop prompts user to enter a valid table length of 41 or 53 only
                    while (validTable == false)
                    {
                        //Prompts user for table size (array length)
                        Console.WriteLine("What table size would you like? [41 or 53]");
                        Console.WriteLine("NOTE: If array length has already been declared, this will redeclare and purge the contents.");
                        
                        while (!int.TryParse(Console.ReadLine(), out tableInt))
                        {
                            Console.Write("Invalid value! Please enter '41' or '53': ");
                        }

                        //Filters out undesired input
                        if (tableInt == 41 || tableInt == 53)
                        {
                            //Declares and initializes the table size to the desired prime number 41 or 53
                            listSize = new int[tableInt];

                            //Provides confirmation to the user that array has been set
                            Console.WriteLine("Array set to length of: {0}.\n", listSize.Length);
                            validTable = true;
                            tableSizeEstablished = true;

                        }
                        else
                        {
                            //Provides feedback for undesired digits discovered in input
                            //User must make another pass through the loop.
                            Console.WriteLine("\nPlease enter 41 or 53 only. Try again.");
                        }
                    }
                    break;

                case 2:
                    //Presents list of algorithms that can be used
                    Console.WriteLine("Which algorithm would you like to use?");
                    Console.WriteLine("(1) Modulo-Division with linear probing");
                    Console.WriteLine("(2) Modulo-Division with key offset");
                    Console.WriteLine("(3) Pseudorandom with linear probing");
                    Console.WriteLine("(4) Psudorandom with key offset");
                    Console.WriteLine("(5) Rotation with linear probing");
                    Console.WriteLine("(6) Rotation with key offset");

                    
                    while (!int.TryParse(Console.ReadLine(), out algSelectInt))
                    {
                        Console.Write("Invalid value! Please enter an integer between 1-6: ");
                    }

                    //switch statement gives user feedback confirming their selection and returning to Main menu
                    //algSelectInt stores the selected algorithm for later use.
                    switch (algSelectInt)
                    {
                        case 1:
                            Console.WriteLine("You've selected Modulo-Division algorithm with Linear Probing resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        case 2:
                            Console.WriteLine("You've selected Modulo-Division algorithm with Key Offset resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        case 3:
                            Console.WriteLine("You've selected Pseudorandom algorithm with Linear Probing resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        case 4:
                            Console.WriteLine("You've selected Pseudorandom algorithm with Key Offset resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        case 5:
                            Console.WriteLine("You've selected Rotation algorithm with Linear Probing resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        case 6:
                            Console.WriteLine("You've selected Rotation algorithm with Key Offset resolution. Returning to Main Menu.");
                            selectedAlgorithm = true;
                            break;

                        default:
                            Console.WriteLine("Invalid selection made. Returning to the Main Menu.");
                            selectedAlgorithm = false;
                            break;

                    }

                    break;

                case 3:
                    //Loads values from partslist.txt if the table size and algorithm has been set
                    if (tableSizeEstablished == false || selectedAlgorithm == false)
                    {
                        Console.WriteLine("The data cannot be loaded. Table size or algorithm not yet established.");
                        Console.WriteLine("Returning to main menu.");

                    }
                    else if (tableSizeEstablished == true && selectedAlgorithm == true)
                    {
                        Console.WriteLine("Loading item list from text file");

                        //Declares tempHashTable string array to load items from file sequentially
                        //Creates realHashTable int array to store the integers after they've been loaded into the temp array
                        tempHashTable = new string[listSize.Length];
                        realHashTable = new int[listSize.Length];

                        //Creates Boolean flag array to declare a 'used' position on the table. This will be used for
                        //linear probing resolution
                        bool[] indexTaken = new bool[listSize.Length];

                        FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);

                        StreamReader reader = new StreamReader(inFile);
                        string loadIn;
                        loadIn = reader.ReadLine();

                        //Loads the data (as strings) into tempHashTable
                        int i = 0;
                        while (i <= 29)
                        {
                            tempHashTable[i] = Convert.ToString(loadIn);
                            i++;
                            loadIn = reader.ReadLine();
                        }


                        int j;
                        //SWITCH STATEMENT LOADS THE CONTENT INTO realHashTable Array based on algorithm selected
                        switch (algSelectInt)
                        {
                            //Modulo-Division with Linear Probing resolution                            
                            case 1:
                                for (j = 0; j <= (29); j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);
                                    //Checks for collision. If none found; inserts value
                                    if (indexTaken[hasher % tableInt] == false)
                                    {
                                        realHashTable[hasher % tableInt] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[hasher % tableInt] = true;
                                    }
                                    else if (indexTaken[hasher % tableInt] == true)
                                    {
                                        LinearProbing((hasher % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }
                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            //Modulo-Division with Key Offset resolution
                            case 2:
                                for (j = 0; j <= (29); j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);
                                    //Checks for collision. If none found; inserts value
                                    if (indexTaken[hasher % tableInt] == false)
                                    {
                                        realHashTable[hasher % tableInt] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[hasher % tableInt] = true;
                                    }
                                    else if (indexTaken[hasher % tableInt] == true)
                                    {
                                        KeyOffset((hasher % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }
                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            //Pseudorandom with Linear Probing Resolution (11x + 23; x = part number)
                            case 3:
                                for (j = 0; j <= (29); j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);

                                    //Checks for collision. If none found; inserts value
                                    if (indexTaken[(((hasher * 11) + 23) % tableInt)] == false)
                                    {
                                        realHashTable[(((hasher * 11) + 23) % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[(((hasher * 11) + 23) % tableInt)] = true;
                                    }
                                    else if (indexTaken[(((hasher * 11) + 23) % tableInt)] == true)
                                    {
                                        LinearProbing((((hasher * 11) + 23) % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }

                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            //Pseudorandom with Key Offset resolution (11x + 23; x = part number)
                            case 4:
                                for (j = 0; j <= (29); j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);

                                    //Checks for collision. If none found; inserts value
                                    if (indexTaken[(((hasher * 11) + 23) % tableInt)] == false)
                                    {
                                        realHashTable[(((hasher * 11) + 23) % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[(((hasher * 11) + 23) % tableInt)] = true;
                                    }
                                    else if (indexTaken[(((hasher * 11) + 23) % tableInt)] == true)
                                    {
                                        KeyOffset((((hasher * 11) + 23) % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }

                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            //Rotation with Linear Probing resolution
                            case 5:
                                for (j = 0; j <= 29; j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);
                                    int rotationX = (hasher % 1000) * 10000;
                                    int rotationY = ((rotationX + hasher) / 1000);

                                    //realHashTable[(rotationY % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                    if (indexTaken[(rotationY % tableInt)] == false)
                                    {
                                        realHashTable[(rotationY % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[(rotationY % tableInt)] = true;
                                    }
                                    else if (indexTaken[(rotationY % tableInt)] == true)
                                    {
                                        LinearProbing((rotationY % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }
                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            //Rotation with Key Offset resolution
                            case 6:
                                for (j = 0; j <= 29; j++)
                                {
                                    hasher = Convert.ToInt32(tempHashTable[j]);
                                    int rotationX = (hasher % 1000) * 10000;
                                    int rotationY = ((rotationX + hasher) / 1000);

                                    //realHashTable[(rotationY % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                    if (indexTaken[(rotationY % tableInt)] == false)
                                    {
                                        realHashTable[(rotationY % tableInt)] = Convert.ToInt32(tempHashTable[j]);
                                        indexTaken[(rotationY % tableInt)] = true;
                                    }
                                    else if (indexTaken[(rotationY % tableInt)] == true)
                                    {
                                        KeyOffset((rotationY % tableInt), realHashTable, hasher, ref collisions); //takes in the address, array list and part number(key) respectively and the number of collisons
                                    }
                                }
                                Console.WriteLine("Collisions: {0}.", collisions);
                                collisions = 0;
                                break;

                            default:
                                break;
                            //End of Hashing algorithm switch statement
                        }
                    }
                    break;
                //End of data loading procedure

                case 4:
                    //Prints out hash table contents

                    //uses 'p' as the counter, printing out every item in the hash table until array length is reached
                    int x = 0;
                    for (x = 0; x <= (listSize.Length - 1); x++)
                    {
                        Console.WriteLine("Hash Table item {0} is {1}.", x, realHashTable[x]);
                    }


                    break;

                case 5:
                    //Allows user to search for a value, using linear search

                    
                    int hashSearch;
                    Console.WriteLine("How would you like to Search for a value?");
                    Console.WriteLine("(1) By 4-digit part number");
                    Console.WriteLine("(2) By table index value");                

                   //user selects either 1 or 2
                    while (!int.TryParse(Console.ReadLine(), out hashSearch))
                    {
                        Console.Write("Invalid value! Please either '1' or '2': ");
                    }

                    while (hashSearch != 1 && hashSearch != 2)
                    {
                        Console.Write("Please enter either '1' or '2': ");
                        while (!int.TryParse(Console.ReadLine(), out hashSearch))
                        {
                            Console.Write("Invalid value! Please either '1' or '2': ");
                        }
                    }

                    //search by part number
                    if (hashSearch == 1)
                    {
                        Console.Write("Enter the 4-digit part number to search for: ");
                        while (!int.TryParse(Console.ReadLine(), out hashSearch))
                        {
                            Console.Write("Invalid value! Please enter a 4 digit integer: ");
                        }

                        

                        bool foundSearch = false;

                        //brute force search approach
                        int z = 0;
                        for (z = 0; z <= (tableInt - 1); z++)
                        {
                            if (hashSearch == realHashTable[z])
                            {
                                Console.WriteLine("The desired part number was located in table position {0}.", z);
                                foundSearch = true;
                            }

                        }

                        //if not found
                        if (foundSearch == false)
                        {
                            Console.WriteLine("Search returned 0 results.");
                        }
                    }

                        // search by index
                    else if (hashSearch == 2)
                    {
                        //cannot search an empty array
                        if (realHashTable.Length != 41 && realHashTable.Length != 53)
                        {
                            Console.WriteLine("Cannot search an empty list!");
                        }

                        else
                        {
                            //user enters a valid index
                            Console.Write("Enter the index you would like to search: ");
                            while (!int.TryParse(Console.ReadLine(), out hashSearch))
                            {
                                Console.Write("Please enter an index value, between 0 and {0} inclusive: ", (tableInt - 1));
                            }

                            while (hashSearch < 0 || hashSearch > (tableInt - 1))
                            {
                                Console.Write("Please enter an index value, between 0 and {0} inclusive: ", (tableInt - 1));
                                while (!int.TryParse(Console.ReadLine(), out hashSearch))
                                {
                                    Console.Write("Please enter an index value, between 0 and {0} inclusive", (tableInt - 1));
                                }
                            }
                            //result is displayed
                            Console.WriteLine("The part number at index {0} is {1}", hashSearch, realHashTable[hashSearch]);
                        }
                    }
            
                    break;
            

                case 6:
                    //Allows user to delete a value
                    //Prompts user to decide if deletion should be done by part number or table index
                    Console.WriteLine("How would you like to delete a value?");
                    Console.WriteLine("(1) By 4-digit part number");
                    Console.WriteLine("(2) By table index value");
                    int deleteDecide;
                    //user enters a valid choice
                    while (!int.TryParse(Console.ReadLine(), out deleteDecide))
                    {
                        Console.Write("Invalid value! Please enter either '1' or '2': ");
                    }

                    switch (deleteDecide)
                    {
                        case 1:
                            Console.WriteLine("Please enter the 4-digit part number to be deleted");
                            int deleteInt;
                            while (!int.TryParse(Console.ReadLine(), out deleteInt))
                            {
                                Console.Write("Invalid value! Please enter a 4 digit integer: ");
                            }
                            //brute force approach to deletion
                            bool didDelete = false;
                            int z = 0;
                            for (z = 0; z <= (tableInt - 1); z++)
                            {
                                if (deleteInt == realHashTable[z])
                                {
                                    Console.WriteLine("The desired part number was located in table position {0}.", z);
                                    realHashTable[z] = 0;
                                    Console.WriteLine("Part number deleted. Returning to main menu.\n");
                                    didDelete = true;
                                }
                            }

                            //if not found
                            if (didDelete == false)
                            {
                                Console.WriteLine("Could not find the value requested.");
                            }
                            break;

                        case 2:

                            //cannot delete from an empty list
                            if (realHashTable.Length != 41 && realHashTable.Length != 53)
                            {
                                Console.WriteLine("Cannot delete from an empty list!");
                            }

                            else
                            {
                                //user enters a valid index
                                Console.WriteLine("Please enter the index value, between 0 and {0} inclusive", (tableInt - 1));
                                int deleteIndex;
                                while (!int.TryParse(Console.ReadLine(), out deleteIndex))
                                {
                                    Console.Write("Invalid value! Please enter an integer between 0 and {0} inclusive: ", (tableInt - 1));
                                }

                                realHashTable[deleteIndex] = 0;

                                //results are displayed
                                Console.WriteLine("Part number at index {0} deleted. Returning to main menu.", deleteIndex);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case 7:
                    quitApp = true;
                    break;

                default:
                    Console.WriteLine("Please enter a number between 1 and 6.");
                    break;
            }
            //End of Switch Statement
        }

    }

    public static int LinearProbing(int index, int[] list, int partNumber, ref int collisions) //takes in the address, array list and part number(key) respectively and the number of collisons
    {
        int original = index;//original is used to mark the starting point so that if we come back to it we  know the array is full
        bool resolution = false;//flag to know when to stop the method

        

        while (resolution == false)//until the collision has been resolved or cannot be done
        {
            if (list[index] == 0)//if the address is free put the key in (obviously if this method is called that means there is a collision so this condition is also used the first time to double check if there is a collision)
            {
                list[index] = partNumber;//insert the key in the list
                resolution = true;//the collision is resolved
            }

            else
            {
                ++index;//add 1 to the address
                ++collisions;//used to count the number of collisions. this parameter should be passed to this method with an initial 0 value. 
                if (index == list.Length)//if we have reached the end of the array
                {
                    index = 0;//we start from the beginning
                }

                if (index == original)//if we come back to the starting index we know there is no place to insert the key
                {
                    resolution = true;//we assume resolution is true to stop executing the loop
                    Console.WriteLine("List is full! Cannot insert value.");//and print an error message
                }
            }
        }

        return index; //return the index where the key was inserted to keep track of it for deletion purposes. 
        //if this returned index is equal to the the index originally sent to this method, then you know the collision failed.
    }

    public static int KeyOffset(int index, int[] list, int key, ref int collisions)//key(part number), address and array list passed respectively and the number of collisions
    {
        bool resolution = false;//same purpose as in linear probing        
        int original = index;//original is used to mark the starting point so that if we come back to it we  know the array is full
        //this algorithm produces the same collison path for the same key so i assume at some point it's gonna come back to he original index and when that happens the resolution has failed

       

        while (resolution == false)//same as in linear probing
        {
            if (list[index] == 0)
            {
                list[index] = key;
                resolution = true;
            }

            else//calculate new address
            {
                int oldAddress = index;
                int offset = Convert.ToInt32(Math.Round((double)key / list.Length));
                index = (offset + oldAddress) % list.Length;

                ++collisions; //should be sent by reference initially with a value of 0

                if (index == original)//if it starts from the beginning address again
                {
                    resolution = true;//resolution has failed so we want to get off the loop
                    Console.WriteLine("Cannot insert Key using this resolution algorithm");//and print an error message
                }
            }
        }

        return index;//return the index where the key was inserted to keep track of it for deletion purposes. 
        //if this returned index is equal to the the index originally sent to this method, then you know the collision failed.
    }
}//best algorithm should be the one with the least collisons/probes