/* This part of the program gets the circle's radius from the user and displays its diameter, circumference and area
 
                                                                                                   */

using System;

public class MainMethod
{

    public static void Main()
    {

        Circle pool = new Circle();     // This creates a new object ( circular pool )
        char choice = 'c';

        while (choice == 'c')
        {
            // Prompt the user to enter the radius of the circle
            Console.Write("Enter the radius of the circle: ");
            double radius = Convert.ToDouble(Console.ReadLine()); // This is the pool's radius        
            pool.SetRadius(radius);       // This sets the value of the radius to be the entered value
            pool.Radius = radius;

            if (pool.Radius == 0)
            {
                Console.WriteLine("Error! Radius cannot be 0 or less");
            }

            else
            {
                double diameter = pool.Radius * 2; // This declares and computes the diameter of the pool


                // Display the pool's area, diameter and circumference
                Console.WriteLine("The circle's area is {0:N} square meters, with diameter {1:N} meters and circumference {2:N} meters", pool.GetArea(), diameter, pool.GetCircumference());
                Console.Write("Do you want to [C]ontinue or [Quit]? (Enter C or Q): ");
                choice = Convert.ToChar(Console.ReadLine().ToLower());
                Console.Clear();              // This clears the screen  
            }
        }
    }


}