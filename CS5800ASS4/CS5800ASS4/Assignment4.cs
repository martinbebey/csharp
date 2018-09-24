//this program minimizes given dfa and tests the resulting dfa

//by Martin Bebey

using System;
using System.IO;

public class Assignment4
{
    public static void Main()
    {
        int dfaChoice = 0;   //user choice of DFA to work with or to quit the program 

        while (dfaChoice != 9)
        {
            StreamReader transitionTableReader;
            Ass3Program program = new Ass3Program();
            char[] stateArray, stateArray2;
            char states = '@';
            int numberOfStates = 0, i, j, k, currentState, endState1 = -1, endState2 = -2, newStateCount = 0, index = -1, z, index2 = -2, a;
            int[] startStates, finishStates;
            string[,] transitionTable, tableOfDistinguishabilities, newTransitionTable;
            string[] testInput = new string[] { "0", "00", "01", "000", "001", "010", "011", "0110", "0010" }, testInput2 = new string[] {"1", "10", "11", "100", "101", "110", "111", "1110", "1010"}, newStateArray, newStateArray2; // 14 test input data
            string data, str1 = "", firstState, secondState;
            bool finishState1 = false, finishState2 = false, transition = true, finish = false, match0 = false, match1 = false, compatibility = false;

            Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");

            while (!int.TryParse(Console.ReadLine(), out dfaChoice))
            {
                Console.Write("Invalid input! ");
                Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");
            }

            if (dfaChoice != 9)
            {
                while (dfaChoice < 4 || dfaChoice > 8)
                {
                    Console.Write("Invalid input! ");
                    Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");

                    while (!int.TryParse(Console.ReadLine(), out dfaChoice))
                    {
                        Console.Write("Invalid input! ");
                        Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");
                    }
                }

                transitionTableReader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2013\Projects\CS5800ASS4\CS5800ASS4\bin\Debug\TransitionTable" + dfaChoice + ".txt");//path to the file on my PC);

                data = transitionTableReader.ReadLine();//reading 1st line in file
                numberOfStates = int.Parse(data.Split(' ')[0]);
                startStates = new int[int.Parse(data.Split(' ')[1])];
                finishStates = new int[int.Parse(data.Split(' ')[2])];
                stateArray = new char[numberOfStates];
                stateArray2 = new char[numberOfStates];
                newStateArray = new string[numberOfStates];

                for (i = 0; i < numberOfStates; ++i)
                {
                    stateArray[i] = ++states;
                    stateArray2[i] = states;
                }

                

                data = transitionTableReader.ReadLine();//2nd line

                for (i = 0; i < startStates.Length; ++i)
                {
                    startStates[i] = int.Parse(data.Split(' ')[i]);
                }

                data = transitionTableReader.ReadLine();//3rd line

                for (i = 0; i < finishStates.Length; ++i)
                {
                    finishStates[i] = int.Parse(data.Split(' ')[i]);
                }

                transitionTable = new string[numberOfStates, numberOfStates];
                tableOfDistinguishabilities = new string[numberOfStates, numberOfStates];

                while (!transitionTableReader.EndOfStream)//populating the transition table (reading the rest of the file)
                {
                    data = transitionTableReader.ReadLine();
                    transitionTable[int.Parse(data.Split(' ')[0]), int.Parse(data.Split(' ')[1])] = data.Split(' ')[2];
                }

                transitionTableReader.Close();// closing the file

                for (j = 0; j < numberOfStates - 1; ++j )
                {
                    for (i = j+1; i < numberOfStates; ++i )
                    {
                       foreach(int state in finishStates)
                       {
                           if(state == j)
                           {
                               finishState1 = true;
                           }

                           if (state == i)
                           {
                               finishState2 = true;
                           }
                       }

                       if ((finishState1 && !finishState2) || (finishState2 && !finishState1))
                       {
                           tableOfDistinguishabilities[j, i] = "X";
                       }

                       else
                       {
                           finishState1 = finishState2 = false;
                           currentState = j;


                            foreach (string y in testInput)
                           {                               
                               if (!match0)
                               {
                                   currentState = j;

                                   if (endState1 != endState2)
                                   {
                                       foreach (char x in y)
                                       {
                                           transition = true;

                                           if (transition)//if character is valid
                                           {
                                               transition = false;

                                               for (k = 0; k < numberOfStates; ++k)//check if character leads to any other state from the current state
                                               {


                                                   if (transitionTable[currentState, k] != null && !transition)
                                                   {

                                                       if (transitionTable[currentState, k] == x.ToString())//if character leads to a new state
                                                       {
                                                           transition = true;
                                                           currentState = k;//update the current state
                                                           //Console.Write("-> q{0} ", i);//output the transition to user
                                                       }

                                                       else//if transition to a state can happen with more than 1 character (such as 'a,b')
                                                       {
                                                           if (transitionTable[currentState, k].Length > 1)
                                                           {
                                                               if (transitionTable[currentState, k].Substring(0, 1) == x.ToString() || transitionTable[currentState, k].Substring(1, 1) == x.ToString())
                                                               {
                                                                   transition = true;
                                                                   currentState = k;
                                                                   // Console.Write("-> q{0} ", i);
                                                               }
                                                           }
                                                       }
                                                   }
                                               }
                                           }
                                       }


                                       foreach (int state in finishStates)//check if current state is one of the finish states
                                       {
                                           if (currentState == state)
                                           {
                                               finish = true;
                                           }
                                       }

                                       if (transition && finish)//if the last input character caused a transition to a finish state
                                       {
                                           //Console.Write("\n Accepted!\n");
                                           endState1 = currentState;
                                           str1 = y;
                                           currentState = i;
                                           transition = false;
                                           finish = false;

                                           foreach (char x in str1)
                                           {
                                               transition = true;

                                               if (transition)//if character is valid
                                               {
                                                   transition = false;

                                                   for (k = 0; k < numberOfStates; ++k)//check if character leads to any other state from the current state
                                                   {
                                                       if (transitionTable[currentState, k] != null && !transition)
                                                       {

                                                           if (transitionTable[currentState, k] == x.ToString())//if character leads to a new state
                                                           {
                                                               transition = true;
                                                               currentState = k;//update the current state
                                                               //Console.Write("-> q{0} ", i);//output the transition to user
                                                           }

                                                           else//if transition to a state can happen with more than 1 character (such as 'a,b')
                                                           {
                                                               if (transitionTable[currentState, k].Length > 1)
                                                               {
                                                                   if (transitionTable[currentState, k].Substring(0, 1) == x.ToString() || transitionTable[currentState, k].Substring(1, 1) == x.ToString())
                                                                   {
                                                                       transition = true;
                                                                       currentState = k;
                                                                       // Console.Write("-> q{0} ", i);
                                                                   }
                                                               }
                                                           }
                                                       }
                                                   }
                                               }
                                           }


                                           foreach (int state in finishStates)//check if current state is one of the finish states
                                           {
                                               if (currentState == state)
                                               {
                                                   finish = true;
                                               }
                                           }

                                           if (transition && finish)//if the last input character caused a transition to a finish state
                                           {
                                               //Console.Write("\n Accepted!\n");
                                               endState2 = currentState;
                                               //str1 = y;
                                           }

                                           else
                                           {
                                               //Console.Write("\n Rejected!\n");
                                           }
                                       }

                                       else
                                       {
                                           //Console.Write("\n Rejected!\n");
                                       }
                                   }

                                   if (endState1 == endState2)
                                   {
                                       tableOfDistinguishabilities[j, i] = "Y";
                                       match0 = true;
                                   }

                                   else
                                   {
                                       tableOfDistinguishabilities[j, i] = "X";
                                   }
                               }
                           }

                           if (match0)
                           {
                               endState1 = -1;
                               endState2 = -2;
                               finish = false;

                               foreach (string y in testInput2)
                               {
                                   if (!match1)
                                   {
                                       currentState = j;

                                       if (endState1 != endState2)
                                       {
                                           foreach (char x in y)
                                           {
                                               transition = true;

                                               if (transition)//if character is valid
                                               {
                                                   transition = false;

                                                   for (k = 0; k < numberOfStates; ++k)//check if character leads to any other state from the current state
                                                   {
                                                       if (transitionTable[currentState, k] != null && !transition)
                                                       {

                                                           if (transitionTable[currentState, k] == x.ToString())//if character leads to a new state
                                                           {
                                                               transition = true;
                                                               currentState = k;//update the current state
                                                               //Console.Write("-> q{0} ", i);//output the transition to user
                                                           }

                                                           else//if transition to a state can happen with more than 1 character (such as 'a,b')
                                                           {
                                                               if (transitionTable[currentState, k].Length > 1)
                                                               {
                                                                   if (transitionTable[currentState, k].Substring(0, 1) == x.ToString() || transitionTable[currentState, k].Substring(1, 1) == x.ToString())
                                                                   {
                                                                       transition = true;
                                                                       currentState = k;
                                                                       // Console.Write("-> q{0} ", i);
                                                                   }
                                                               }
                                                           }
                                                       }
                                                   }
                                               }
                                           }


                                           foreach (int state in finishStates)//check if current state is one of the finish states
                                           {
                                               if (currentState == state)
                                               {
                                                   finish = true;
                                               }
                                           }

                                           if (transition && finish)//if the last input character caused a transition to a finish state
                                           {
                                               //Console.Write("\n Accepted!\n");
                                               endState1 = currentState;
                                               str1 = y;
                                               currentState = i;
                                               transition = false;
                                               finish = false;

                                               foreach (char x in str1)
                                               {
                                                   transition = true;

                                                   if (transition)//if character is valid
                                                   {
                                                       transition = false;

                                                       for (k = 0; k < numberOfStates; ++k)//check if character leads to any other state from the current state
                                                       {
                                                           if (transitionTable[currentState, k] != null && !transition)
                                                           {

                                                               if (transitionTable[currentState, k] == x.ToString())//if character leads to a new state
                                                               {
                                                                   transition = true;
                                                                   currentState = k;//update the current state
                                                                   //Console.Write("-> q{0} ", i);//output the transition to user
                                                               }

                                                               else//if transition to a state can happen with more than 1 character (such as 'a,b')
                                                               {
                                                                   if (transitionTable[currentState, k].Length > 1)
                                                                   {
                                                                       if (transitionTable[currentState, k].Substring(0, 1) == x.ToString() || transitionTable[currentState, k].Substring(1, 1) == x.ToString())
                                                                       {
                                                                           transition = true;
                                                                           currentState = k;
                                                                           // Console.Write("-> q{0} ", i);
                                                                       }
                                                                   }
                                                               }
                                                           }
                                                       }
                                                   }
                                               }


                                               foreach (int state in finishStates)//check if current state is one of the finish states
                                               {
                                                   if (currentState == state)
                                                   {
                                                       finish = true;
                                                   }
                                               }

                                               if (transition && finish)//if the last input character caused a transition to a finish state
                                               {
                                                   //Console.Write("\n Accepted!\n");
                                                   endState2 = currentState;
                                                   //str1 = y;
                                               }

                                               else
                                               {
                                                   //Console.Write("\n Rejected!\n");
                                               }
                                           }

                                           else
                                           {
                                               //Console.Write("\n Rejected!\n");
                                           }
                                       }

                                       if (endState1 == endState2)
                                       {
                                           tableOfDistinguishabilities[j, i] = "Y";
                                           match1 = true;
                                       }

                                       else
                                       {
                                           tableOfDistinguishabilities[j, i] = "X";
                                       }
                                   }
                               }
                           }

                           else
                           {
                               tableOfDistinguishabilities[j, i] = "X";
                           }

                           if (match0 && match1)
                           {
                               tableOfDistinguishabilities[j, i] = "Y";
                           }

                           else
                           {
                               tableOfDistinguishabilities[j, i] = "X";
                           }                         
                       }

                       endState1 = -1;
                       endState2 = -2;
                       str1 = "";
                       finish = finishState1 = finishState2 = transition = match0 = match1 = false;
                     }
                }

                for(j = 0; j < numberOfStates - 1; ++j)
                {
                    for (i = j + 1; i < numberOfStates; ++i )
                    {
                        if(tableOfDistinguishabilities[j,i] == "Y")
                        {
                            //if(newStateArray[j] == null)
                            //{
                                newStateArray[j] = newStateArray[j] + stateArray[i].ToString();
                                compatibility = true;
                                stateArray[i] = 'z';

                           // }                           
                        }

                        //else
                        //{
                        //    newStateArray[j] = stateArray[j] + stateArray[i].ToString();
                        //}

                        else if(!compatibility)
                        {
                            newStateArray[j] = stateArray[j].ToString();
                        }
                    }

                    compatibility = false;
                }

                for (i = 0; i < numberOfStates - 1; ++i)
                {
                    if (tableOfDistinguishabilities[j, i] == "Y")
                    {
                        //if(newStateArray[j] == null)
                        //{
                        newStateArray[j] = stateArray[j] + stateArray[i].ToString();
                        compatibility = true;
                        stateArray[i] = 'z';

                        // }                           
                    }

                        //else
                    //{
                    //    newStateArray[j] = stateArray[j] + stateArray[i].ToString();
                    //}

                    else if (!compatibility)
                    {
                        newStateArray[j] = stateArray[j].ToString();
                    }
                }


                    Console.Write("The new states are: ");
                    foreach (string newStates in newStateArray)
                    {
                        if (newStates != null && !newStates.Contains("z"))
                        {
                            Console.Write(newStates + " ");
                            ++newStateCount;
                        }
                    }

                newTransitionTable = new string[newStateCount, newStateCount];
                newStateArray2 = new string[newStateCount];

                for (z = 0, a = 0; z < numberOfStates; ++z)
                {
                    if(!newStateArray[z].Contains("z") && newStateArray[z] != null)
                    {
                        newStateArray2[a] = newStateArray[z];
                        ++a;
                    }
                }

                    for (i = 0; i < newStateCount; ++i)
                    {
                        for (j = 0; j < newStateCount; ++j)
                        {
                            index2 = -2;

                            firstState = newStateArray2[i].Substring(0, 1);

                            for (z = 0; z < numberOfStates; ++z)
                            {
                                if (stateArray2[z].ToString() == firstState)
                                {
                                    index = z;
                                }
                            }

                            if ((transitionTable[index, j] != null) && (stateArray2[j].ToString() == newStateArray2[j].Substring(0, 1)))
                            {
                                newTransitionTable[i, j] = transitionTable[index, j];
                            }

                            else if (newStateArray2[j].ToString().Length > 1)
                            {
                                secondState = newStateArray2[j].Substring(1, 1);

                                for (z = 0; z < numberOfStates; ++z)
                                {

                                    if (stateArray2[z].ToString() == secondState)
                                    {
                                        index2 = z;
                                    }

                                }

                                if (index2 > 0)
                                {
                                    if ((transitionTable[index, index2] != null) && (newStateArray2[j].Contains(stateArray2[index2].ToString())))
                                    {
                                        newTransitionTable[i, j] = transitionTable[index, index2];
                                    }
                                }
                            }

                            else
                            {
                                secondState = newStateArray2[j].Substring(0, 1);

                                for (z = 0; z < numberOfStates; ++z)
                                {

                                    if (stateArray2[z].ToString() == secondState)
                                    {
                                        index2 = z;
                                    }

                                }

                                if (index2 > 0)
                                {
                                    if ((transitionTable[index, index2] != null) && (stateArray2[index2].ToString() == newStateArray2[j].Substring(0, 1)))
                                    {
                                        newTransitionTable[i, j] = transitionTable[index, index2];
                                    }
                                }
                            }
                        }
                    }

                Console.WriteLine("\nNew transition table:\n");

                for (j = 0; j < newStateArray2.Length; ++j )
                {
                    Console.Write(newStateArray2[j].PadLeft(4));

                    for (i = 0; i < newStateArray2.Length; ++i)
                    {
                        if (newTransitionTable[i, j] != null)
                        {
                            Console.Write("{0,4}", newTransitionTable[i, j]);
                        }

                        else
                        {
                            Console.Write("{0,4}", "X");
                        }
                    }

                    Console.Write("\n\n");
                }

                Console.Write("    ");

                foreach(string node in newStateArray2)
                {
                    Console.Write("{0,4}", node);
                }

                Console.Write("\n\n");

                program.TestDfa(newStateArray2, newTransitionTable, finishStates);//calls program from assignment 3 to test the minimized DFA
            }

           //Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");

           // while (!int.TryParse(Console.ReadLine(), out dfaChoice))
           // {
           //     Console.Write("Invalid input! ");
           //     Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");
           // }

           // if (dfaChoice != 9)
           // {
           //     while (dfaChoice < 4 || dfaChoice > 8)
           //     {
           //         Console.Write("Invalid input! ");
           //         Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");

           //         while (!int.TryParse(Console.ReadLine(), out dfaChoice))
           //         {
           //             Console.Write("Invalid input! ");
           //             Console.Write("Enter 4, 5, 6, 7 or 8 for DFAs on p155, 158, 4.4.1, 4.4.2 and fig9 respetively or 9 to quit: ");
           //         }
           //     }
           // }
        }
    }
}