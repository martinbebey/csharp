using System;

public class PumBody
{
    public static void Main()
    {
        char choice = 'z';

        while (choice != 'm')
        {
            int[] items = new int[12];
            int[] scrapLocation = new int[12];
            int[] pumpBody = new int[12];

            int percentage = 0, count = 0;
            //bool good == false;
            Random randomNumber = new Random();

            for (int i = 0; i < items.Length; ++i)
            {
                items[i] = randomNumber.Next(1, 100001);
            }

            for (int i = 0; i < items.Length; ++i)
            {
                percentage = randomNumber.Next(0, 101);

                if (percentage >= 95)
                {
                    pumpBody[i] = items[i];
                }

                else
                {
                    scrapLocation[i] = items[i];
                }
            }

            Console.Write("Pump Body: ");

            for (int i = 0; i < pumpBody.Length; ++i)
            {
                if (pumpBody[i] != null)
                {
                    Console.Write("{0} ", pumpBody[i]);
                }
            }

            Console.Write("\nScrap Location: ");

            for (int i = 0; i < scrapLocation.Length; ++i)
            {
                if (scrapLocation[i] != null)
                {
                    Console.Write("{0} ", scrapLocation[i]);
                }
            }

            Console.Write("\nPress 'm' to quit or any other key to process another 12 parts: ");
            choice = char.Parse(Console.ReadLine().ToLower());
            Console.WriteLine("\n");
        }
          
    }
}