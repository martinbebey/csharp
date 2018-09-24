using System;

public class Fractional
{
    public static void Main()
    {
        int numerator, denominator;
        char choice = 'c';
        Fractions fraction1, fraction2 = new Fractions(), fraction3 = new Fractions();

        while (choice == 'c')
        {
            fraction1 = new Fractions(3, 4);

            Console.Write("Enter the numerator for fraction 2: ");
            numerator = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the denominator fro fraction 2: ");
            denominator = Convert.ToInt32(Console.ReadLine());
            

            fraction2 = new Fractions(numerator, denominator);            
            fraction2.Denomnator = denominator;

            fraction3 = Fractions.OperationMultiply(fraction1, fraction2);            

            Console.WriteLine("Fraction 1 = " + fraction1.Print() + " fraction 2 = " + fraction2.Print() + " fraction 3 = " + fraction3.Print());

            Console.Write("Press [C]ontinue or [Q]uit: ");
            choice = Convert.ToChar(Console.ReadLine().ToLower());
            while(choice != 'c' && choice != 'q')
            {
                Console.Write("Press [C]ontinue or [Q]uit: ");
                choice = Convert.ToChar(Console.ReadLine().ToLower());
            }
        }
    }
}