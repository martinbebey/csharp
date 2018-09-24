//This program uses user input and transition files to test strings to corresponding DFAs and shows the output

//By Martin Bebey

using System;
using System.IO;

public class Ass3Program
{
    public void TestDfa(string[] newStateArray, string[,] newTransitionTable, int[] finishStates)
    {
        //int dfaChoice = 0;   //user choice of DFA to work with or to quit the program     

        //while (dfaChoice != 4)//while user doesn't want to quit
        //{
        //    int numberOfStates = 0, i, currentState;// i is used in loops
        //    string[,] transitionTable;
        //    int[] startStates, finishStates;
        //    StreamReader transitionTableReader;
        //    string data, userString;// data is data read from the files, user string is the string to be tested
        //    bool transition = true, finish = false;//transition shows whether or not there was a change in state (if input character was valid)
        //    //finish tells whether the current state is a finish state

        //    Console.Write("Enter 1, 2 or 3 for DFAs on p. 152, 154 or 155 respectively or 4 to quit: ");

        //    while (!int.TryParse(Console.ReadLine(), out dfaChoice))
        //    {
        //        Console.Write("Invalid input! ");
        //        Console.Write("Enter 1, 2 or 3 for DFAs on p. 152, 154 or 155 respectively or 4 to quit: ");
        //    }

        //    if (dfaChoice != 4)
        //    {

        //        while (dfaChoice < 1 || dfaChoice > 3)
        //        {
        //            Console.Write("Invalid input! ");
        //            Console.Write("Enter 1, 2 or 3 for DFAs on p. 152, 154 or 155 respectively: ");

        //            while (!int.TryParse(Console.ReadLine(), out dfaChoice))
        //            {
        //                Console.Write("Invalid input! ");
        //                Console.Write("Enter 1, 2 or 3 for DFAs on p. 152, 154 or 155 respectively: ");
        //            }
        //        }

        //transitionTableReader = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2013\Projects\CS5800Ass3\CS5800Ass3\bin\Debug\TransitionTable" + dfaChoice + ".txt");//path to the file on my PC);

        //data = transitionTableReader.ReadLine();//reading 1st line in file
        //numberOfStates = int.Parse(data.Split(' ')[0]);
        //startStates = new int[int.Parse(data.Split(' ')[1])];
        //finishStates = new int[int.Parse(data.Split(' ')[2])];

        //data = transitionTableReader.ReadLine();//2nd line

        //for (i = 0; i < startStates.Length; ++i)
        //{
        //    startStates[i] = int.Parse(data.Split(' ')[i]);
        //}

        //data = transitionTableReader.ReadLine();//3rd line

        //for (i = 0; i < finishStates.Length; ++i)
        //{
        //    finishStates[i] = int.Parse(data.Split(' ')[i]);
        //}

        //transitionTable = new string[numberOfStates, numberOfStates];

        //while (!transitionTableReader.EndOfStream)//populating the transition table (reading the rest of the file)
        //{
        //    data = transitionTableReader.ReadLine();
        //    transitionTable[int.Parse(data.Split(' ')[0]), int.Parse(data.Split(' ')[1])] = data.Split(' ')[2];
        //}

        //transitionTableReader.Close();// closing the file

        string userString, currentState;
        bool transition = true, finish = false;
        int i, current = 0;


        Console.Write("Enter a test string: ");
        userString = Console.ReadLine();//getting the string to be tested
        currentState = newStateArray[0];//starting at the start state

        Console.Write("-> " + currentState + " ");

        foreach (char x in userString)
        {
            if (transition)//if character is valid
            {
                transition = false;

                for (i = 0; i < newStateArray.Length; ++i)//check if character leads to any other state from the current state
                {
                    if (newTransitionTable[current, i] != null && !transition)
                    {

                        if (newTransitionTable[current, i] == x.ToString())//if character leads to a new state
                        {
                            transition = true;
                            current = i;//update the current state
                            Console.Write("-> " + newStateArray[current] + " ");//output the transition to user
                        }

                        else//if transition to a state can happen with more than 1 character (such as 'a,b')
                        {
                            if (newTransitionTable[current, i].Length > 1)
                            {
                                if (newTransitionTable[current, i].Substring(0, 1) == x.ToString() || newTransitionTable[current, i].Substring(1, 1) == x.ToString())
                                {
                                    transition = true;
                                    current = i;
                                    Console.Write("-> " + newStateArray[current] + " ");
                                }
                            }
                        }
                    }
                }
            }

        }

        foreach (int state in finishStates)//check if current state is one of the finish states
        {
            if (current == state)
            {
                finish = true;
            }
        }

        if (transition && finish)//if the last input character caused a transition to a finish state
        {
            Console.Write("\nAccepted!\n");
        }

        else
        {
            Console.Write("\nRejected!\n");
        }

        finish = false;
    }
}
