//This program solves the Pillar word puzzle Kata problem

//The basic idea here is to search in each of the 8 directions for any given character in the grid, and each search looks up a word of a specified
//length in the given direction. This is done for every distinct search word length. The process continues until all the words have been found or
//the end of the grid has been reached.


//By Martin Bebey

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


public class PillarWordPuzzleKata
{
    public static void Main()
    {
        //defining variables. Their names are pretty self-explanatory. x,y and i are used for looping
        StreamReader puzzleReader;
        string firstLineOfTheGrid, currentGridLine, searchWordList, lookedUpWord = "", coordinates = "", puzzleFilePath = Application.StartupPath + @"\word puzzle 2.txt"; //these may need to be changed to forward slashes depending on your machine
        string[] searchWords;
        char[,] puzzleGrid;
        int numberOfSearchWords, numberOfCharactersInARow, i, row, column, searchRow, searchColumn, x, y, wordSize, countOfWordsFound = 0, wordLookUpLength;
        int[] searchWordSizes;
        bool sizeIsStored, allWordsFound = false;
        bool[] wordAlreadyFound;

        //initializing variables
        puzzleReader = new StreamReader(puzzleFilePath, false);//reads the text file containing the puzzle
        searchWordList = puzzleReader.ReadLine();//stores all search words in this array
        numberOfSearchWords = searchWordList.Split(' ').Count();
        searchWords = new string[numberOfSearchWords];
        searchWordSizes = new int[numberOfSearchWords];
        wordAlreadyFound = new bool[numberOfSearchWords];//prevents duplicates by setting a flag determining if word was previously found

        //initialize all search word sizes to 0 
        for (i = 0; i < numberOfSearchWords; ++i)
        {
            searchWordSizes[i] = 0;
        }

        //populating the search word array with the search words and all found flags to false
        for (i = 0; i < numberOfSearchWords; ++i)
        {
            searchWords[i] = searchWordList.Split(' ')[i];
            wordAlreadyFound[i] = false;
        }

        //getting the distinct lengths of the search words by getting all their lengths and only storing the distinct values
        //this is used to determine the length of the search in each direction for each character in the grid
        for (i = 0; i < numberOfSearchWords; ++i)
        {
            wordSize = searchWords[i].Length;
            sizeIsStored = false;

            for(x = 0; x < numberOfSearchWords && !sizeIsStored; ++x)
            {
                if(searchWordSizes[x] == wordSize)
                {
                    sizeIsStored = true;
                }

                else if(searchWordSizes[x] == 0)
                {
                    searchWordSizes[x] = wordSize;
                    sizeIsStored = true;
                }
            }
        }
        
        firstLineOfTheGrid = puzzleReader.ReadLine();//reads in the first characters that are in the first line of the grid - 2nd line of the file
        numberOfCharactersInARow = firstLineOfTheGrid.Split(' ').Count() - 1;//to get the size of the square grid
        puzzleGrid = new char[numberOfCharactersInARow, numberOfCharactersInARow];//initialize the grid

        //populate the 1st line of the grid
        for (i = 0; i < numberOfCharactersInARow; ++i)
        {
            puzzleGrid[0, i] = Convert.ToChar(firstLineOfTheGrid.Split(' ')[i]);
        }

        //populate the rest of the grid
        for(row = 1; row < numberOfCharactersInARow; ++row)
        {
            currentGridLine = puzzleReader.ReadLine();

            for (i = 0; i < numberOfCharactersInARow; ++i)
            {
                puzzleGrid[row, i] = Convert.ToChar(currentGridLine.Split(' ')[i]);
            }
        }

        //Now the fun part - the search
        //we want to keep searching until the whole grid is covered OR until all words have been found in which case it'll be unecessary to continue searching
        //also duplicates are prevented
        for(row = 0; row < numberOfCharactersInARow && !allWordsFound; ++row)//for each row in the grid
        {
            for(column = 0; column < numberOfCharactersInARow && !allWordsFound; ++column)//for each column in the grid
            {
                //for each distinct search word  size, look up words in all directions
                for(i = 0; i < searchWordSizes.Length && searchWordSizes[i] != 0 && !allWordsFound; ++i)
                {
                    wordLookUpLength = searchWordSizes[i];
                    coordinates = string.Format("({0}, {1})", row, column);

                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        //horizontal forward search (moving to the right)
                        searchColumn = column + x;

                        if (searchColumn < numberOfCharactersInARow)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", row, searchColumn);//record coordinates as we go for easy printing
                            }

                            lookedUpWord += puzzleGrid[row, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //horizontal reverse search (moving to the left)
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                       
                        searchColumn = column - x;

                        if (searchColumn > -1)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", row, searchColumn);
                            }

                            lookedUpWord += puzzleGrid[row, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //vertical upward search (moving North)
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchRow = row - x;

                        if (searchRow > -1)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, column);
                            }

                            lookedUpWord += puzzleGrid[searchRow, column];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //vertical down search (moving south)
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchRow = row + x;

                        if (searchRow < numberOfCharactersInARow)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, column);
                            }

                            lookedUpWord += puzzleGrid[searchRow, column];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //diagonal NE search 
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchColumn = column + x;
                        searchRow = row - x;

                        if (searchColumn < numberOfCharactersInARow && searchRow > -1)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, searchColumn);
                            }

                            lookedUpWord += puzzleGrid[searchRow, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //diagonal NW search 
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchColumn = column - x;
                        searchRow = row - x;

                        if (searchColumn > -1 && searchRow > -1)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, searchColumn);
                            }

                            lookedUpWord += puzzleGrid[searchRow, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //diagonal SE search 
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchColumn = column + x;
                        searchRow = row + x;

                        if (searchColumn < numberOfCharactersInARow && searchRow < numberOfCharactersInARow)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, searchColumn);
                            }

                            lookedUpWord += puzzleGrid[searchRow, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);


                    //diagonal SW search 
                    for (x = 0; x < wordLookUpLength; ++x)
                    {
                        
                        searchColumn = column - x;
                        searchRow = row + x;

                        if (searchColumn > -1 && searchRow < numberOfCharactersInARow)
                        {
                            if (x >= 1)
                            {
                                coordinates += string.Format(", ({0}, {1})", searchRow, searchColumn);
                            }

                            lookedUpWord += puzzleGrid[searchRow, searchColumn];//creates the search word by appending characters in the given direction up to the specified length
                        }
                    }

                    //calls the method to compare the words
                    compareWords(ref numberOfSearchWords, ref countOfWordsFound, ref lookedUpWord, ref searchWords, ref row, ref column, ref coordinates, ref allWordsFound, ref wordAlreadyFound);
                }
            }
        }

        Console.ReadKey();//to keep the console opened to see the results. Press ANY key to terminate the program
    }

    //this is used to compare the looked up word against the search words and see if there is a match
    public static void compareWords(ref int numberOfSearchWords, ref int countOfWordsFound, ref string lookedUpWord, ref string[] searchWords, ref int row, ref int column, ref string coordinates, ref bool allWordsFound, ref bool[] wordAlreadyFound)
    {        
                    for (int y = 0; y < numberOfSearchWords; ++y)
                    {
                        if (searchWords[y] == lookedUpWord & !wordAlreadyFound[y])//if it's a match and the word was not already found/printed before
                        {
                            countOfWordsFound++;
                            wordAlreadyFound[y] = true;

                            if (countOfWordsFound == numberOfSearchWords)
                            {
                                allWordsFound = true;//once this is true, the search stops and the rest of the grid is ignored
                            }

                            Console.WriteLine(searchWords[y] + ": " + coordinates);//prints the coordinates for every word found
                        }

                    }

                    ///Reinitialize the looked up word and the coordinates
                    lookedUpWord = "";
                    coordinates = string.Format("({0}, {1})", row, column);
    }
}