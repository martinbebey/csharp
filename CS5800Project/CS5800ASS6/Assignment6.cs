//this program implements a turing machine designed to simulate the work of an elevator
//by
//Martin Bebey

using System;
using System.IO;
using System.Text;

class Assignment6
{
    public static void Main()
    {
        int state = 0;//floor number or W6/A

        while (state != 6)//until no one uses the elevator
        {
            state = 0;
            StreamReader transitionTableReader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2013\Projects\CS5800Project\CS5800ASS6\bin\Debug\TransitionTable1.txt");//path to the file on my PC);
            string data, inputString = "";//user data
            StringBuilder stringBuilder;
            int x = -1, flag = 0, served = 0, nextState = -1, stateA, y = 0, columnCount = 0, rowCount = 0, digit = 0, inputFlag = 0, index = -1, column = -1;
            string[,] transitionTable;//transition table
            char charact, c;//each character in the input string

            //reading data from a file containing transition table data for the turing machine
            data = transitionTableReader.ReadLine();
            rowCount = int.Parse(data.Split(' ')[0]);
            columnCount = int.Parse(data.Split(' ')[1]);
            transitionTable = new string[rowCount, columnCount];

            while (!transitionTableReader.EndOfStream)
            {
                data = transitionTableReader.ReadLine();
                ++x;
                y = 0;
                foreach (string str in data.Split('\t'))
                {
                    transitionTable[x, y] = str;
                    ++y;
                }
            }

            transitionTableReader.Close();//closing the file

            Console.Write("Enter the starting floor (1 through 5) of the car or 6 to exit: ");

            while (!int.TryParse(Console.ReadLine(), out state))
            {
                Console.Write("Invalid input! Enter the starting floor (1 through 5) of the car or 6 to exit: ");
            }

            while (state < 1 || state > 6)
            {
                Console.Write("Invalid input! Enter the starting floor (1 through 5) of the car or 6 to exit: ");

                while (!int.TryParse(Console.ReadLine(), out state))
                {
                    Console.Write("Invalid input! Enter the starting floor (1 through 5) of the car or 6 to exit: ");
                }
            }

            if (state != 6)//if user did not choose to exit
            {

                Console.Write("Enter a string of floors (1 through 5) to be served by the car: ");

                while (inputFlag != 2)
                {
                    foreach (char character in Console.ReadLine())
                    {
                        if (inputFlag == 0)
                        {
                            if (!int.TryParse(character.ToString(), out digit) || digit > 5)
                            {
                                Console.Write("Invalid input! Enter a string of floors (1 through 5) to be served by the car: ");
                                inputFlag = 1;
                            }

                            else
                            {
                                inputString += character;
                            }
                        }
                    }

                    if (inputFlag != 1)
                    {
                        inputFlag = 2;
                    }

                    else
                    {
                        inputFlag = 0;
                        inputString = "";
                    }
                }

                inputFlag = 0;
                inputString = "B" + inputString + "B";//padding Blank symbols to the string
                stringBuilder = new StringBuilder(inputString);
                index = 1;
                --state;
                stateA = rowCount - 1;
                Console.Write("\n -> W{0}{1} ", state + 1, inputString);//output TM starting output


                while (served != inputString.Length - 2)
                {
                    if (flag == 0) // if going right
                    {
                        charact = inputString[index];

                        if (charact == '*')
                        {
                            column = columnCount - 4;
                        }

                        else if (charact == 'B')
                        {
                            column = columnCount - 2;
                        }

                        else
                        {
                            column = int.Parse(charact.ToString()) - 1; //not B or not *
                        }

                        c = stringBuilder[index];

                        //changing the string according to the transition table
                        stringBuilder[index] = char.Parse(transitionTable[state, column].Split(',')[1]);
                        inputString = stringBuilder.ToString();

                        //printing results
                        if(index == 0)
                        {
                            Console.Write("-> W{0}{1} ", state + 1, inputString.Substring(index));

                        }

                        else
                        {
                            Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), state + 1, inputString.Substring(index));
                        }

                        //counting number of floors served and the getting the next state from the transition table
                        if (inputString[index] == '*' && c != '*')
                        {
                            ++served;
                            nextState = int.Parse(transitionTable[state, column].Split(',')[0].Substring(1, 1)) - 1;
                        }

                        else if (c != 'B')
                        {
                            nextState = int.Parse(transitionTable[state, column].Split(',')[0].Substring(1, 1)) - 1;
                            ++index;//going right

                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", state + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), state + 1, inputString.Substring(index));
                            }

                        }

                        if (c == 'B')
                        {
                            --index;
                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", state + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), state + 1, inputString.Substring(index));
                            }
                        }

                        if (nextState != state)
                        {
                            state = nextState;
                            nextState = stateA;
                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                            }
                            flag = 1;
                        }
                    }

                    else if (flag == 1) //this implements state A, which is W6 in this program which just moves right to the end of the string
                    {
                        while (inputString[index] != 'B')
                        {
                            charact = inputString[index];

                            if (charact == '*')
                            {
                                column = columnCount - 4;
                            }

                            else if (charact == 'B')
                            {
                                column = columnCount - 2;
                            }

                            else
                            {
                                column = int.Parse(charact.ToString()) - 1; //not B or not *
                            }

                            stringBuilder[index] = char.Parse(transitionTable[nextState, column].Split(',')[1]);
                            inputString = stringBuilder.ToString();

                            
                            
                            nextState = int.Parse(transitionTable[nextState, column].Split(',')[0].Substring(1, 1)) - 1;
                            ++index;
                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                            }
                        }

                        if (index == 0)
                        {
                            Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                        }

                        else
                        {
                            Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                        }

                        nextState = state;
                        --index;

                        if (index == 0)
                        {
                            Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                        }

                        else
                        {
                            Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                        }

                        flag = 2;
                    }

                    else if (flag == 2)// this moves back to the begining of the string from the end cancelling out calls to the current floor
                    {
                        while (inputString[index] != 'B')
                        {
                            charact = inputString[index];

                            if (charact == '*')
                            {
                                column = columnCount - 4;
                            }

                            else if (charact == 'B')
                            {
                                column = columnCount - 2;
                            }

                            else
                            {
                                column = int.Parse(charact.ToString()) - 1; //not B or not *
                            }

                            c = stringBuilder[index];

                            stringBuilder[index] = char.Parse(transitionTable[state, column].Split(',')[1]);
                            inputString = stringBuilder.ToString();

                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                            }

                            if (inputString[index] == '*' && c != '*')
                            {
                                ++served;
                                nextState = int.Parse(transitionTable[nextState, column].Split(',')[0].Substring(1, 1)) - 1;
                            }

                            else
                            {
                                nextState = int.Parse(transitionTable[state, column].Split('/')[1].Split(',')[0].Substring(1, 1)) - 1;
                            }

                            --index;//moving left

                            if (index == 0)
                            {
                                Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                            }

                            else
                            {
                                Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                            } 

                            if (nextState != state)
                            {
                                state = nextState;
                                flag = 1;
                            }
                        }

                        ++index;

                        if (index == 0)
                        {
                            Console.Write("-> W{0}{1} ", nextState + 1, inputString.Substring(index));

                        }

                        else
                        {
                            Console.Write("-> {0}W{1}{2} ", inputString.Substring(0, index), nextState + 1, inputString.Substring(index));
                        }

                        flag = 0;
                    }
                }

                Console.Write("\n\n");
            }
        }
    }
}