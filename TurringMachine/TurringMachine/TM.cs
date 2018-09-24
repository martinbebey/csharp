using System;

public class TurrinMachine
{
    public static void Main()
    {
        char[] word;
        int Acount = 0, Bcount = 0, i = 0, lenghth;
        char letter = 'x';
        while (letter != 'q')
        {
            Console.Write("Enter the length of the word: ");       
            
                lenghth = Convert.ToInt32(Console.ReadLine());
                word = new char[lenghth];            

            while (i < lenghth && letter != 'q')
            {
                Console.Write("Enter either 'a' or 'b' or 'q' to exit: ");
                letter = Convert.ToChar(Console.ReadLine());
                switch (letter)
                {
                    case 'a':
                        ++Acount;
                        word[i] = 'a';
                        ++i;
                        break;
                    case 'b':
                        ++Bcount;
                        word[i] = 'b';
                        ++i;
                        break;
                    case 'q':
                        break;
                    case 'd':

                        break;
                    default:
                        break;
                }
            }

            if (letter != 'q')
            {
                if (Acount > Bcount)
                {                   
                        Console.WriteLine("Accepted");                    
                }

                else
                {
                    Console.WriteLine("TM crashed");
                }

                i = 0; Bcount = 0; Acount = 0;
            }
        }
    }
}