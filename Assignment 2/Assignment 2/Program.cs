                         /*                             Assignment 2                             *
                          *                                                                      *
                          *                     Encoding and Decoding text                       *
                          *                                                                      *
                          *                              Edited by                               *
                          *                                                                      *
                          * * * * * * * * *    Sarah Rayfuse and Martin Bebey    * * * * * * * * */

//These are the used namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_2 //Creates a new namespace
{
    class Program
    {
        static void Main(string[] args) // This is the console driver program
        {
            string command = "w"; // This is the user's command to write a new text message initialised to "w"

            // This while loop makes the program to start back if the user chooses to enter a new text message (by entering the command
            // "w" when prompted)
            while (command == "w")
            {
                // Print instructions to user
                Console.Write("Enter some text in lower case: ");
                String input = Console.ReadLine(); // This is the user's inputed text

                //This displays the inputed text to the user
                Console.WriteLine("\nYour text is: " + input);

                //This is to ensure the inputed text is in lower case and also not just an empty space
                while (input != input.ToLower() || input == "")
                {
                    //Print instructions to the user
                    Console.Write("\nInvalid input !!!\n\nYou are not allowed to write numbers or symbols !  Please try again with LOWERCASE letters separated or not"
                           + " by a space. You must enter at least a letter or a space.");
                    //Prompt the user to enter some text
                    Console.Write("\n\nEnter some text: ");
                    input = Console.ReadLine(); //This is the user's inputed text

                    //This displays the inputed text to the user
                    Console.WriteLine("\nYour text is: " + input);
                }

                Dictionary<char, int> chars = new Dictionary<char, int>(); // a hash to store frequencies (easier than a plain array)

                char[] alpha = "abcdefghijklmnopqrstuvwxyz ".ToCharArray(); //an array of letters and a space

                foreach (char letter in alpha) // set all frequencies to 0
                {
                    chars.Add(letter, 0);
                }

                //This try/catch block ensures the user cannot enter numbers or symbols
                try
                {
                    foreach (char letter in input) // count the individual letters
                    {

                        chars[letter]++;

                    }
                }

                catch (KeyNotFoundException) //This catches the exception generated when a number or symbol is inputed by the user
                {
                    input = null; //This sets back the user's input to null (to remove the numbers and/or symbols)

                    //This loop ensures the user cannot proceed until the input is made up of only letters and/or spaces
                    while (input == null)
                    {
                        //Print instructions to the user
                        Console.Write("\nYou are not allowed to write numbers or symbols !  Please try again with LOWERCASE letters separated or not"
                               + " by a space, that is, only letters and/or spaces can be entered.");
                        Console.Write("\n\nEnter some text: ");

                        input = Console.ReadLine(); //This is the user's input

                        //This checks if the inputed text is in lower case...
                        while (input != input.ToLower())
                        {
                            //...if it is not, print instructions to the user
                            Console.Write("\nYou are not allowed to write numbers or symbols !  Please try again with LOWERCASE letters separated or not"
                                   + " by a space, that is, only letters and/or spaces can be entered.");
                            Console.Write("\n\nEnter some text: ");
                            input = Console.ReadLine(); // This is the user's input
                        }

                        //This try/catch block makes the loop to start back if the user still enters everything but letters and/or spaces 
                        //(by catching the exception generated and setting  back the "input" variable to "null")
                        try
                        {
                            foreach (char letter in input) // count the indidvidual letters
                            {

                                chars[letter]++;

                            }
                        }

                        catch (KeyNotFoundException) //Catch the exception
                        {
                            input = null; //Set back input to null so the loop starts back
                        }
                    }

                    //Display the inputed text to the user
                    Console.WriteLine("\nYour text is: " + input);
                }

                PriorityQueue<TreeNode<Character>> PQ = new PriorityQueue<TreeNode<Character>>(); // the priority queue

                foreach (KeyValuePair<char, int> pair in chars) // initialize the priority queue
                {
                    if (pair.Value > 0) // add only letters that occur
                    {
                        PQ.Add(new TreeNode<Character>(new Character(pair.Key, pair.Value))); // creae a node for each letter
                    }
                }

                while (PQ.Size() > 1) // do the following loop when PQ's size is greater than one (until we have one tree)
                {
                    TreeNode<Character> right = PQ.Front(); // get the right node
                    PQ.Remove();
                    TreeNode<Character> left = PQ.Front(); //get the left node
                    PQ.Remove();

                    TreeNode<Character> newTreeNode = new TreeNode<Character>(new Character('*', left.Item.Freq + right.Item.Freq)); // use * for non-leaf nodes
                    newTreeNode.Left = left;
                    newTreeNode.Left.HuffCode = "0"; // left node of a tree has a value of 0
                    newTreeNode.Right = right;
                    newTreeNode.Right.HuffCode = "1"; // right node of a tree has a value of 1
                    PQ.Add(newTreeNode);
                }
                


                    Dictionary<char, String> codes = new Dictionary<char, String>(); // store the code for each letter in a hash

                    Prefix(codes, PQ.Front(), ""); // fill codes hash with codes

                    // task 4                
                    String code = ""; // initializing code to an empty string so that later we can put the encoded text there


                    foreach (char letter in input) //encoding each letter 
                    {

                        code += codes[letter];
                    }

                    //This loop implies that if the user entered only one character (or a text made up of only one character aLthrough
                    //or only made up of space(s) ), That single character will be encoded to "0" by default
                    if (code.Length == 0)
                    {
                        code = "0";
                    }

                    //Print out the encoded text to the user
                    Console.WriteLine("\nYour encoded text is: " + code); //output the encoded text
                
                


                    //task 5///////////////////////////////////////////////////

                    TreeNode<Character> current = PQ.Front(); //starts at the root node

                    //Output the decoded text
                    Console.Write("\nYour decoded text is: ");

                    foreach (char letter in code) //going through the 0s and 1s
                    {
                        if (letter == '0') // if the value of letter is 0 current moves to the left node
                        {
                            current = current.Left; // the current node is set to the left child
                        }

                        else
                        {
                            current = current.Right; // the current node is set to the right child
                        }


                        try
                        {
                            if (current.Left == null && current.Right == null)  // if the node has no children it is a leaf node (which we are trying to find)
                            {
                                Console.Write(current.Item.Char); // Display its code
                                current = PQ.Front(); // go back to the root of the tree

                            }
                        }


                        catch (NullReferenceException)
                        {
                            Console.Write(input);
                        }
                    }                

                    // Ask the user if he wants to quit or enter some other text message
                    Console.Write("\n\nPress [Q]uit to quit or [W]rite to input another text message: ");
                    command = Console.ReadLine().ToLower(); // This is the user's command to quit or write a text (in lower case)

                    //This ensures the user inputs the right command
                    while (command != "q" && command != "w")
                    {
                        //Print instructions to the user
                        Console.Write("\n\nInvalid input !! Please enter either [Q]uit to quit the program or [W]rite to write another text"
                                      + " message: ");
                        command = Console.ReadLine().ToLower(); //This is the user's command (in lower case)
                    }

                    //This takes care of the user's command
                    switch (command)
                    {
                        case "q": //If the user enters "q"
                            Console.Clear(); // Clear the screen (and the window closes)
                            break;

                        case "w": //If he enters "w" instead
                            Console.Clear(); //Clear the screen (and the program begins again)
                            break;

                        //This does nothing
                        default:
                            break;
                    }

                
            }

        }
        

        static void Prefix(Dictionary<char, String> dict, TreeNode<Character> treeNode, String str)
        {
            str += treeNode.HuffCode; //append either 0 or 1

            if (treeNode.Left == null && treeNode.Right == null) //if a leaf node is reached
            {
                dict.Add(treeNode.Item.Char, str);
            }

            else
            {
                Prefix(dict, treeNode.Left, str); //traverse the tree to left
                Prefix(dict, treeNode.Right, str); // traverse tree to right
            }
        }

        static void createHuffman() // create huffman tree
        {

        }
    }
}
