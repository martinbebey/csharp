using System;

public class FarmerSolution
{
    public static void Main()
    {
        ConsoleKeyInfo choice;
        
        do
        {
            double costPerChick, costPerDuck, costPerPig = 0, totalExtendedPrice = 0;
            double[] quantityOfAnimals = new double[3];
            double[] extendedPrice = new double[3];
            double totalQuantityOfAnimals = 0;
            

            Console.Clear();
            Console.Write("Enter the cost per Chick: ");

            while (!double.TryParse(Console.ReadLine(), out costPerChick))
            {
                Console.Write("Invalid entry! Please enter the cost per Chick: ");
            }

            if (costPerChick < 0)
            {
                Console.Write("Cost cannot be less than $0. Please enter the cost per Chick: ");

                while (!double.TryParse(Console.ReadLine(), out costPerChick))
                {
                    Console.Write("Invalid entry! Please enter the cost per Chick: ");
                }
            }

            Console.Write("\nEnter the cost per Duck: ");

            while (!double.TryParse(Console.ReadLine(), out costPerDuck))
            {
                Console.WriteLine("Invalid entry! Please enter the cost per Duck: ");
            }

            if (costPerDuck < 0)
            {
                Console.Write("Cost cannot be less than $0. Please enter the cost per Duck: ");

                while (!double.TryParse(Console.ReadLine(), out costPerChick))
                {
                    Console.Write("Invalid entry! Please enter the cost per Duck: ");
                }
            }

            Console.Write("\nEnter the cost per Pig: ");

            while (!double.TryParse(Console.ReadLine(), out costPerPig))
            {
                Console.WriteLine("Invalid entry! Please enter the cost per Pig: ");
            }

            if (costPerPig < 0)
            {
                Console.Write("Cost cannot be less than $0. Please enter the cost per Pig: ");

                while (!double.TryParse(Console.ReadLine(), out costPerChick))
                {
                    Console.Write("Invalid entry! Please enter the cost per Pig: ");
                }
            }

            Console.WriteLine("\nList of solutions: ");

            SolveQuantityProblem(costPerChick, costPerDuck, costPerPig, ref quantityOfAnimals, ref  extendedPrice, ref totalExtendedPrice, ref totalQuantityOfAnimals);

            Console.Write("\nPress the Escape (Esc) key to quit, or any other key to enter new prices. ");

           choice = Console.ReadKey();
        }
        while (choice.Key != ConsoleKey.Escape);
    }

    public static void SolveQuantityProblem(double costPerChick, double costPerDuck, double costPerPig, ref double[] quantityOfAnimals, ref double[] extendedPrice, ref double totalExtendedPrice, ref double totalQuantityOfAnimals)
    {
        int i, chick = 0, duck = 1, pig = 2;

        double[] costs = new double[3];
        int result;
        double a, b = 0;

        for (i = 0; i < 3; ++i)
        {
            if (i == 0)
            {
                costs[i] = costPerChick;
            }

            if (i == 1)
            {
                costs[i] = costPerDuck;
            }

            if (i == 2)
            {
                costs[i] = costPerPig;
            }
        }

        ++quantityOfAnimals[duck];
        quantityOfAnimals[chick] = (100 - (100 * costPerPig) - (quantityOfAnimals[duck] * (costPerDuck - costPerPig))) / (costPerChick - costPerPig);

        while (quantityOfAnimals[chick] >= 1 && quantityOfAnimals[duck] < 99) 
        {
            quantityOfAnimals[pig] = 100 - quantityOfAnimals[duck] - quantityOfAnimals[chick];

            if (quantityOfAnimals[pig] >= 1)
            {

                for (i = 0; i < 3; ++i)
                {
                    totalExtendedPrice += costs[i] * quantityOfAnimals[i];
                    extendedPrice[i] = costs[i] * quantityOfAnimals[i];
                    totalQuantityOfAnimals += quantityOfAnimals[i];
                }

                if (int.TryParse(quantityOfAnimals[chick].ToString(), out  result))
                {
                    Console.WriteLine("\nAnimal Type Cost per Animal Qty of Animals Extended Price\n");
                    Console.WriteLine("Chicks      ${0, 14:0.00} {1, 14} ${2, 7:00.00}", costs[chick], quantityOfAnimals[chick], extendedPrice[chick]);
                    Console.WriteLine("Ducks       ${0, 14:0.00} {1, 14} ${2, 7:00.00}", costs[duck], quantityOfAnimals[duck], extendedPrice[duck]);
                    Console.WriteLine("Pigs        ${0, 14:0.00} {1, 14} ${2, 7:00.00}", costs[pig], quantityOfAnimals[pig], extendedPrice[pig]);

                    Console.WriteLine("\n\nBoth must equal 100. ->                {0, 3} ${1, 7:00.00}", totalQuantityOfAnimals, totalExtendedPrice);

                }

            }

            totalExtendedPrice = totalQuantityOfAnimals = quantityOfAnimals[chick] = quantityOfAnimals[pig] = 0;
            
            ++quantityOfAnimals[duck];
            a = costPerDuck - costPerPig;

            //if(a == 0)
            //{
            //    a = 1;
            //}

            b = costPerChick - costPerPig;

            //if (b == 0)
            //{
            //    b = 1;
            //}

            //if (a == 0)
            //{
            //    quantityOfAnimals[chick] = -1 * ((100 - (100 * costPerPig) - (quantityOfAnimals[duck] * a)) / b);
            //}

            //else
            //{

                quantityOfAnimals[chick] = (100 - (100 * costPerPig) - (quantityOfAnimals[duck] * a)) / b;
            //}
        }

        if(quantityOfAnimals[chick] < 1 && quantityOfAnimals[pig] == 0)
        {
            Console.WriteLine("\nSorry! no (more) available solutions for the given prices.");
        }

    }


    //    if (costPerChick <= costPerDuck && costPerChick <= costPerPig)
    //    {
    //        leastExpensive = 0;

    //        if (costPerDuck <= costPerPig)
    //        {
    //            fairlyExpensive = 1;
    //            mostExpensive = 2;
    //        }

    //        else
    //        {
    //            mostExpensive = 1;
    //            fairlyExpensive = 2;
    //        }
    //    }

    //    else if (costPerDuck <= costPerChick && costPerDuck <= costPerPig)
    //    {
    //        leastExpensive = 1;

    //        if (costPerChick <= costPerPig)
    //        {
    //            fairlyExpensive = 0;
    //            mostExpensive = 2;
    //        }

    //        else
    //        {
    //            mostExpensive = 0;
    //            fairlyExpensive = 2;
    //        }
    //    }

    //    else if (costPerPig <= costPerChick && costPerPig <= costPerDuck)
    //    {
    //        leastExpensive = 2;

    //        if (costPerChick <= costPerDuck)
    //        {
    //            fairlyExpensive = 0;
    //            mostExpensive = 1;
    //        }

    //        else
    //        {
    //            mostExpensive = 0;
    //            fairlyExpensive = 1;
    //        }
    //    }

    //    //for larger number of animals mergesort or insertionsort would be used for sorting...


    //    while ((totalExtendedPrice != 100 && totalQuantityOfAnimals != 100) || totalExtendedPrice != totalQuantityOfAnimals)
    //    {
    //        if (totalExtendedPrice >= 100)
    //        {
    //            --totalQuantityOfAnimals;

    //            if (quantityOfAnimals[mostExpensive] > 1)
    //            {
    //                --quantityOfAnimals[mostExpensive];
    //                extendedPrice[mostExpensive] = costs[mostExpensive] * quantityOfAnimals[mostExpensive];
    //                totalExtendedPrice -= costs[mostExpensive];
    //            }

    //            else if(quantityOfAnimals[fairlyExpensive] > 1)
    //            {
    //                --quantityOfAnimals[fairlyExpensive];
    //                --quantityOfAnimals[fairlyExpensive];
    //                --totalQuantityOfAnimals;
    //                extendedPrice[fairlyExpensive] = costs[fairlyExpensive] * quantityOfAnimals[fairlyExpensive];
    //                totalExtendedPrice -= costs[fairlyExpensive] * 2;
    //            }

    //            else
    //            {
    //                quantityOfAnimals[leastExpensive] -= 3;
    //                totalQuantityOfAnimals -= 2;
    //                extendedPrice[leastExpensive] = costs[leastExpensive] * quantityOfAnimals[leastExpensive];
    //                totalExtendedPrice -= costs[leastExpensive];
    //            }
    //        }

    //        else if (totalQuantityOfAnimals == 100)
    //        {
    //            totalQuantityOfAnimals -= 3;
    //            //--totalQuantityOfAnimals;
    //            quantityOfAnimals[leastExpensive] -= 3;
    //            //--quantityOfAnimals[leastExpensive];
    //            extendedPrice[leastExpensive] = costs[leastExpensive] * quantityOfAnimals[leastExpensive];
    //            totalExtendedPrice -= costs[leastExpensive] * 3;
    //        }

    //        if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
    //        {
    //            ++quantityOfAnimals[leastExpensive];
    //            ++totalQuantityOfAnimals;
    //            totalExtendedPrice = 0;
    //            extendedPrice[leastExpensive] = costs[leastExpensive] * quantityOfAnimals[leastExpensive];

    //            for (i = 0; i < 3; ++i)
    //            {
    //                totalExtendedPrice += extendedPrice[i];
    //            }
    //        }

    //        if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
    //        {
    //            ++quantityOfAnimals[fairlyExpensive];
    //            ++totalQuantityOfAnimals;
    //            totalExtendedPrice = 0;
    //            extendedPrice[fairlyExpensive] = costs[fairlyExpensive] * quantityOfAnimals[fairlyExpensive];

    //            for (i = 0; i < 3; ++i)
    //            {
    //                totalExtendedPrice += extendedPrice[i];
    //            }

    //        }

    //        if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
    //        {
    //            ++quantityOfAnimals[mostExpensive];
    //            ++totalQuantityOfAnimals;
    //            totalExtendedPrice = 0;
    //            extendedPrice[mostExpensive] = costs[mostExpensive] * quantityOfAnimals[mostExpensive];

    //            for (i = 0; i < 3; ++i)
    //            {
    //                totalExtendedPrice += extendedPrice[i];

    //            }

    //        }


      //  }
   // }
}