//This program simulates the TMs on sections 8.2.1 and 8.2.2 using text files as input respectively

using System;
using System.IO;
using System.Text;

public class Assignment6
{
    static void Main()
    {
        ConsoleKeyInfo userChoice = new ConsoleKeyInfo();//if user wants to exit application or not

        while(userChoice.Key != ConsoleKey.Escape)
        {
            int tmChoice;//choice of TM
            Console.Write("Enter 1 or 2 for TMs 8.2.1 or 8.2.2 respectively: ");

            while(!int.TryParse(Console.ReadLine(), out tmChoice))
            {
                Console.Write("Invalid input. Enter 1 or 2 for TMs 8.2.1 or 8.2.2 respectively: ");
            }

            while(tmChoice != 1 && tmChoice != 2)
            {
                Console.Write("Invalid input. Enter 1 or 2 for TMs 8.2.1 or 8.2.2 respectively: ");

                while (!int.TryParse(Console.ReadLine(), out tmChoice))
                {
                    Console.Write("Invalid input. Enter 1 or 2 for TMs 8.2.1 or 8.2.2 respectively: ");
                }
            }

            StreamReader transitionTableReader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2013\Projects\CS5800ASS6\CS5800ASS6\bin\Debug\TransitionTable" + tmChoice + ".txt");//path to the file on my PC);
            StringBuilder stringBuilder;
            string data = transitionTableReader.ReadLine(), testString = "B";
            int numberOfStates = int.Parse(data.Split(' ')[0]), numberOfInputs = int.Parse(data.Split(' ')[1]), x, y = -1, acceptingState = int.Parse(data.Split(' ')[2]), currentState = 0, i;
            string[,] transitionTable = new string[numberOfStates, numberOfInputs];
            bool transition = false, crashed = false, accepted = false;
           
            //fillinf in the transition table
            while(!transitionTableReader.EndOfStream)
            {
                data = transitionTableReader.ReadLine();
                ++y;

                for(x = 0; x < numberOfInputs; ++x)
                {
                    transitionTable[y, x] = data.Split('\t')[x];
                }
            }

            transitionTableReader.Close();//closing the file
            Console.Write("Enter a test string: ");
            testString += Console.ReadLine().ToLower() + "B";  //padding blank symbol to the string          
            stringBuilder = new StringBuilder(testString);
            Console.Write("\n");
            
            //this loop checks each character, either moves left or right and changes states according to the transition table
            for(i = 0; i < testString.Length; ++i)
            {
                //printing flow of execution
                if (i == 0)
                {
                    Console.Write("-> q{0}{1} ", currentState, testString.Substring(i));

                }

                else
                {
                    Console.Write("-> {0}q{1}{2} ", testString.Substring(0, i), currentState, testString.Substring(i));
                }

                if (!crashed)
                {
                    for (x = 0; x < numberOfInputs; ++x)
                    {
                        if (transitionTable[currentState, x] != "" && !transition)
                        {
                            if (transitionTable[currentState, x][1] == stringBuilder[i])
                            {
                                transition = true;
                                stringBuilder[i] = transitionTable[currentState, x][2];

                                if (transitionTable[currentState, x][3] == 'l')
                                {
                                    i -= 2;
                                }

                                testString = stringBuilder.ToString();

                                currentState = int.Parse(transitionTable[currentState, x][0].ToString());
                            }

                                //this else part is used in the case of an NTM
                            else if (transitionTable[currentState, x].Length > 4)
                            {
                                if (transitionTable[currentState, x][6] == stringBuilder[i])
                                {
                                    transition = true;
                                    stringBuilder[i] = transitionTable[currentState, x][2];                  
                                    
                                    if (transitionTable[currentState, x][8] == 'l')
                                    {
                                        i -= 2;
                                    }

                                    testString = stringBuilder.ToString();
                                    currentState = int.Parse(transitionTable[currentState, x][5].ToString());
                                }
                            }
                        }
                    }

                    if (!transition)//if no transition is found for given symbol
                    {
                        crashed = true;
                    }

                    transition = false;//reset transition flag
                }

                else
                {
                    break;
                }
            }

            if (i == 0)
            {
                Console.Write("-> q{0}{1} ", currentState, testString.Substring(i));

            }

            else
            {
                Console.Write("-> {0}q{1}{2} ", testString.Substring(0, i), currentState, testString.Substring(i));
            }

            if(currentState == acceptingState && !crashed)
            {
                accepted = true;
            }

            if(accepted)
            {
                Console.WriteLine("\n\nAccepted!\n");
            }

            else
            {
                Console.WriteLine("\n\nRejected!\n");
            }

            Console.Write("Press Esc. key to quit or any other key to test a TM: ");
            userChoice = Console.ReadKey();
            Console.Write("\n");
        }
    }
}

