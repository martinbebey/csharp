// Martin Bebey and Clark Groepper
// 0420751          0547643
//
//
//
//
//

using System;

namespace CSKicksCollection.Trees
{
    class Program
    {
        static void Main()
        {
            BinaryTree<string> BST1 = new BinaryTree<string>();  //creates first BST to insert spells alphabetically into [from spellsfile1]
            BinaryTree<string> BST2 = new BinaryTree<string>();  //creates second BST to insert spells randomly into [from spellsfile2]
            AVLTree<string> AVL = new AVLTree<string>();  //creates an AVL that takes spells from file 1
            BinaryTree<string> tempNode = new BinaryTree<string>();  //single node tree for searching, to transfer 

            string[] spells = new string[150];  //array to hold spell names to randomly generate spells for testing efficiency

            string line;  //represents a line of text [ie a spell] from one of the text files
            int counter = 0;  //coupled with the spells array to assign each new spell to a different part of the array

            string spellSearch;  //accepts the user's search input

            int nodeSearchCountBST1 = 0,  //counts the number of nodes visited per search
            totalNodeSearchCountBST1 = 0,  //totals all the nodes visited after 100 searches
            nodeSearchCountBST2 = 0,
            totalNodeSearchCountBST2 = 0,
            nodeSearchCountAVL = 0,
            totalNodeSearchCountAVL = 0;

            //  taken from www.stackoverflow.com/questions/13539974/random-number-generator-c-sharp , along with r.Next()
            Random r = new Random();  //random number generator for the efficiency test

            char mainActivityChoice = '~',  //allows for user input with a single character
                treeTraversalChoice = '~',
                treeSearchChoice = '~';

            //taken from www.msdn.microsoft.com/en-us/library/aa287535%28v=vs.71%29.aspx
            System.IO.StreamReader fileA = new System.IO.StreamReader(@"...\...\spellsFile1.txt");  //reads the spells, line by line, from file
            System.IO.StreamReader fileB = new System.IO.StreamReader(@"...\...\spellsFile2.txt");

            while ((line = fileA.ReadLine()) != null)  //adds the spells from spellsfile1 to BST1, the AVL, and the spells-array
            {
                BST1.Add(line);
                AVL.Add(line);
                spells[counter] = line;
                counter++;
            }
            fileA.Close();

            while ((line = fileB.ReadLine()) != null)  //adds spells from spellsfile2 to BST2
            {
                BST2.Add(line);
            }
            fileB.Close();

            Console.WriteLine("Magical Binary Search Tree Program - Spell Index");

            while (mainActivityChoice != 'Q')  //allows user to quit
            {
                Console.WriteLine("\nWould you like to...");
                Console.WriteLine("\tTraverse a tree (A)?");
                Console.WriteLine("\tSearch for a spell (B)?");
                Console.WriteLine("\tDo an efficiency test (C)?");
                Console.WriteLine("\tQuit (Q)?");

                while (!Char.TryParse(Console.ReadLine().ToUpper(), out mainActivityChoice))  //prevents exceptions
                {
                    Console.WriteLine("\nWould you like to...");
                    Console.WriteLine("\tTraverse a tree (A)?");
                    Console.WriteLine("\tSearch for a spell (B)?");
                    Console.WriteLine("\tDo an efficiency test (C)?");
                    Console.WriteLine("\tQuit (Q)?");
                }

                switch (mainActivityChoice)
                {
                    case 'A':  //allow the user to choose which tree to do a preorder or inorder traversal in
                        {
                            Console.WriteLine("Which tree would you like to traverse and how:");
                            Console.WriteLine("BST-1 - Preorder (D) or Inorder (F) \n"
                                            + "BST-2 - Preorder (G) or Inorder (H) \n"
                                            + "AVL  -  Preorder (J) or Inorder (K)?");

                            while (!Char.TryParse(Console.ReadLine().ToUpper(), out treeTraversalChoice))
                            {
                                Console.WriteLine("Which tree would you like to traverse and how:");
                                Console.WriteLine("BST-1 - Preorder (D) or Inorder (F) \n"
                                                + "BST-2 - Preorder (G) or Inorder (H) \n"
                                                + "AVL  -  Preorder (J) or Inorder (K)?");

                            }

                            if (treeTraversalChoice == 'D')
                                BST1.PrintTreePreOrder(BST1.Root);
                            else if (treeTraversalChoice == 'G')
                                BST2.PrintTreePreOrder(BST2.Root);
                            else if (treeTraversalChoice == 'J')
                                AVL.PrintTreePreOrder(AVL.Root);
                            else if (treeTraversalChoice == 'F')
                                BST1.PrintTreeInOrder(BST1.Root);
                            else if (treeTraversalChoice == 'H')
                                BST2.PrintTreeInOrder(BST2.Root);
                            else if (treeTraversalChoice == 'K')
                                AVL.PrintTreeInOrder(AVL.Root);

                        }
                        break;

                    case 'B':  //allows the user to choose which tree to search
                        {
                            Console.WriteLine("Which tree do you want to search:");
                            Console.WriteLine("BST-1 (X), BST-2 (Y), or AVL (Z)?");

                            while (!Char.TryParse(Console.ReadLine().ToUpper(), out treeSearchChoice))
                            {
                                Console.WriteLine("Which tree do you want to search:");
                                Console.WriteLine("BST-1 (X), BST-2 (Y), or AVL (Z)?");
                            }

                            switch (treeSearchChoice)
                            {
                                case 'X':  //allows the user to search BST1
                                    {
                                        Console.Write("\nSearch: ");
                                        spellSearch = Convert.ToString(Console.ReadLine());
                                        BST1.Find(spellSearch);

                                        if (BST1.Find(spellSearch) == null)
                                            Console.WriteLine("Sorry, that spell does not exist.");
                                        else
                                        {
                                            tempNode.Add(BST1.Find(spellSearch));
                                            tempNode.PrintTreeRootOnly(tempNode.Root);
                                            tempNode.Clear();
                                        }

                                    }
                                    break;

                                case 'Y':  //allows the user to search BST2
                                    {
                                        Console.Write("\nSearch: ");
                                        spellSearch = Convert.ToString(Console.ReadLine());
                                        BST2.Find(spellSearch);

                                        if (BST2.Find(spellSearch) == null)
                                            Console.WriteLine("Sorry, that spell does not exist.");
                                        else
                                        {
                                            tempNode.Add(BST2.Find(spellSearch));
                                            tempNode.PrintTreeRootOnly(tempNode.Root);
                                            tempNode.Clear();
                                        }
                                    }
                                    break;

                                case 'Z':  //allows the user to search the AVL
                                    {
                                        Console.Write("\nSearch: ");
                                        spellSearch = Convert.ToString(Console.ReadLine());
                                        AVL.Find(spellSearch);

                                        if (AVL.Find(spellSearch) == null)
                                            Console.WriteLine("Sorry, that spell does not exist.");
                                        else
                                        {
                                            tempNode.Add(AVL.Find(spellSearch));
                                            tempNode.PrintTreeRootOnly(tempNode.Root);
                                            tempNode.Clear();


                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case 'C':  //Efficiency Test [to see how many nodes are visited per search on average per tree]
                        {
                            Console.WriteLine("\nBST-1 \t\tBST-2 \t\tAVL\n");
                            for (int j = 0; j < 5; j++)  //totals the number of nodes visited per 100 random searches per tree [5 times over]
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    BST1.Find(spells[r.Next(138)], ref nodeSearchCountBST1);
                                    BST2.Find(spells[r.Next(138)], ref nodeSearchCountBST2);
                                    AVL.Find(spells[r.Next(138)], ref nodeSearchCountAVL);

                                    totalNodeSearchCountBST1 += nodeSearchCountBST1;
                                    totalNodeSearchCountBST2 += nodeSearchCountBST2;
                                    totalNodeSearchCountAVL += nodeSearchCountAVL;

                                    nodeSearchCountBST1 = 0;
                                    nodeSearchCountBST2 = 0;
                                    nodeSearchCountAVL = 0;
                                }
                                Console.Write(totalNodeSearchCountBST1);
                                Console.Write("\t\t{0}", totalNodeSearchCountBST2);
                                Console.WriteLine("\t\t{0}", totalNodeSearchCountAVL);

                                totalNodeSearchCountBST1 = 0;
                                totalNodeSearchCountBST2 = 0;
                                totalNodeSearchCountAVL = 0;
                            }
                        }
                        break;

                    case 'Q':
                        {
                            mainActivityChoice = 'Q';
                        }
                        break;
                }
            }
        }
    }
}