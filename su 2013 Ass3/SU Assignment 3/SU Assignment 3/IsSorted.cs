/* This program gets a list of numbers from the user and determines whether the numbers are sorted or not.*
 */



using System;                

public class Sorted
{
    public static void Main()
    {


        char endOrStartBack = 'A'; 

        
        while (endOrStartBack != 'q')
        {
            int numElements; // This is the number of elements the user will enter
            int[] numbers;   // This is the list of numbers created by the user
            bool sort;       // This holds 'true' if the numbers are sorted and 'false' otherwise


            // Print instructions to the user
            Console.WriteLine("Enter a list of numbers\n");


            // Prompt the user to enter the number of elements he will like to enter
            Console.Write("How many numbers will you like to enter ? ");

            numElements = Convert.ToInt32(Console.ReadLine()); // This is the number of elements the user will enter


            // This loop ensures at least one (positive) number is entered by the user
            while (numElements < 1)
            {
                Console.Write("\nInput is invalid !! You must enter at least one number\n\n");
                Console.Write("How many numbers will you like to enter ? ");
                numElements = Convert.ToInt32(Console.ReadLine());

            }


            numbers = new int[numElements]; // This creates an array with the number of addresses chosen by the user
            Console.Write("\n");            // This takes the cursor to the margin


            InputArray(numbers, ref numElements);  // This invokes or hands control to the 'InputArray' method
            sort = IsSorted(numbers, numElements); // This will invoke the 'IsSorted' method and assign 'true' or 'false' to the variable 'sort'


            // Print out whether the numbers entered by the user are sorted or not
            Console.WriteLine("\nThe statement 'The numbers you have entered are sorted' is {0}", sort);


            // Ask the user if (s)he will like to quit or enter another list of numbers
            Console.WriteLine("\nPress [Q] to quit or [L] to clear the screen and enter another list of numbers.");
            endOrStartBack = Convert.ToChar(Console.ReadLine().ToLower()); // This is the character the user has entered in lower case


            // This loop ensures only the entries 'q' or 'l' from the user are considered
            while (endOrStartBack != 'q' && endOrStartBack != 'l')
            {
                Console.WriteLine("\nInvalid input !!");
                Console.WriteLine("Press [Q] to quit or [L] to clear the screen and enter another list of numbers");
                endOrStartBack = Convert.ToChar(Console.ReadLine().ToLower());

            }


            // This statement processes the user's command
            switch (endOrStartBack)
            {
                // This will close the window and end the program if 'Q' or 'q' is entered
                case 'q':
                    break;


                // This clears the screen and starts back the program if 'L' or 'l' is entered
                case 'l':
                    Console.Clear();
                    break;


                // This does nothing
                default:
                    break;

            }

        }

    }


    // This method builds up the list of numbers as they are entered by the user
    public static void InputArray(int[] array, ref int n)
    {


        for (int i = 0; i < array.Length; ++i)
        {
            // Prompt the user to enter the next number
            Console.Write("Enter number {0}: ", (i + 1));
            array[i] = Convert.ToInt32(Console.ReadLine()); // This assigns an address to the number entered (in an array)

        }

    }


    
    public static bool IsSorted(int[] array, int n)
    {
        bool sort = true; // This implies the list is always sorted at first (and also if not more than 2 numbers are entered)       

        for (n = 1; n < array.Length; ++n)
        {
            // This statement only applies if the SECOND number of the list is LESS THAN OR EQUAL TO the FIRST
            if ((array[1] < array[0] || array[1] == array[0]) && sort == true)
            {

                // This statement applies if ANY number in the list is LESS THAN OR EQUAL TO the PREVIOUS number
                if (array[n] <= array[(n - 1)])
                {

                   
                    if (((array[n] < array[0]) && (array[n] < array[(n - 1)])) || array[n] == array[(n - 1)])
                        sort = true;

                    else
                        sort = false;  
                }

                   
                else if (array[n] > array[(n - 1)] && array[(n - 1)] >= array[(n - 2)])
                    sort = true;
                else
                    sort = false;   

            }


            
            if ((array[1] > array[0] || array[n] == array[0]) && sort == true)
            {

                
                if ((array[n] > array[(n - 1)] && array[n] > array[0]) || array[n] == array[(n - 1)])
                    sort = true;  

                else
                    sort = false; //....otherwise 'false' is returned !!

            }

        }

        return sort; // This will finally return 'true' if the list of numbers is sorted or 'false' otherwise, to be displayed from the 'Main' method.
    }





}