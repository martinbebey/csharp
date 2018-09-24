using System;

public class FarmerSolution
{
    public static void Main()
    {
        double costPerChick, costPerDuck, costPerPig = 0, totalExtendedPrice = 0;
        int[] quantityOfAnimals = new int[3];
        double[] extendedPrice = new double[3];
        int totalQuantityOfAnimals = 0, i;

        Console.WriteLine("Enter the cost per Chick: ");

        while(!double.TryParse(Console.ReadLine(), out costPerChick))
        {
            Console.WriteLine("Please enter the cost per Chick: ");
        }

        Console.WriteLine("\nEnter the cost per Duck: ");

        while (!double.TryParse(Console.ReadLine(), out costPerDuck))
        {
            Console.WriteLine("Please enter the cost per Duck: ");
        }

        Console.WriteLine("\nEnter the cost per Pig: ");

        while (!double.TryParse(Console.ReadLine(), out costPerPig))
        {
            Console.WriteLine("Please enter the cost per Pig: ");
        }


        SolveQuantityProblem(costPerChick, costPerDuck, costPerPig, ref quantityOfAnimals, ref  extendedPrice, ref totalExtendedPrice, ref totalQuantityOfAnimals);

        Console.WriteLine("{0}, {1}", totalQuantityOfAnimals, totalExtendedPrice);

        for(i = 0; i < 3; ++i)
        {
            Console.WriteLine("{0}, {1}", quantityOfAnimals[i], extendedPrice[i]);
        }

        Console.ReadLine();
    }

    public static void SolveQuantityProblem(double costPerChick, double costPerDuck, double costPerPig, ref int[] quantityOfAnimals, ref double[] extendedPrice, ref double totalExtendedPrice, ref int totalQuantityOfAnimals)
    {
        int i, mostExpensive = 2, leastExpensive = 0, fairlyExpensive = 1, chick = 0, duck = 1, pig = 2;
       
        double[] costs = new double[3];

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

            if (costPerChick <= costPerDuck && costPerChick <= costPerPig)
            {
                leastExpensive = 0;

                if (costPerDuck <= costPerPig)
                {
                    fairlyExpensive = 1;
                    mostExpensive = 2;
                }

                else
                {
                    mostExpensive = 1;
                    fairlyExpensive = 2;
                }
            }

            else if (costPerDuck <= costPerChick && costPerDuck <= costPerPig)
            {
                leastExpensive = 1;

                if (costPerChick <= costPerPig)
                {
                    fairlyExpensive = 0;
                    mostExpensive = 2;
                }

                else
                {
                    mostExpensive = 0;
                    fairlyExpensive = 2;
                }
            }

            else if (costPerPig <= costPerChick && costPerPig <= costPerDuck)
            {
                leastExpensive = 2;

                if (costPerChick <= costPerDuck)
                {
                    fairlyExpensive = 0;
                    mostExpensive = 1;
                }

                else
                {
                    mostExpensive = 0;
                    fairlyExpensive = 1;
                }
            }

        //for larger number of animals mergesort or insertionsort would be used for sorting...


        while((totalExtendedPrice != 100 && totalQuantityOfAnimals != 100) || totalExtendedPrice != totalQuantityOfAnimals)
        {
            if(totalExtendedPrice >= 100)
            {
                --totalQuantityOfAnimals;
                --quantityOfAnimals[mostExpensive];
                extendedPrice[mostExpensive] = costs[mostExpensive] * quantityOfAnimals[mostExpensive];
                totalExtendedPrice -= costs[mostExpensive];
            }

            else if(totalQuantityOfAnimals == 100)
            {
                totalQuantityOfAnimals -= 3;
                //--totalQuantityOfAnimals;
                quantityOfAnimals[leastExpensive] -= 3;
                //--quantityOfAnimals[leastExpensive];
                extendedPrice[leastExpensive] = costs[leastExpensive] * quantityOfAnimals[leastExpensive];
                totalExtendedPrice -= costs[leastExpensive] * 3;
            }

            if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
            {
                ++quantityOfAnimals[leastExpensive];
                ++totalQuantityOfAnimals;
                totalExtendedPrice = 0;
                extendedPrice[leastExpensive] = costs[leastExpensive] * quantityOfAnimals[leastExpensive];

                for (i = 0; i < 3; ++i)
                {
                    totalExtendedPrice += extendedPrice[i];
                }
            }

            if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
            {
                ++quantityOfAnimals[fairlyExpensive];
                ++totalQuantityOfAnimals;
                totalExtendedPrice = 0;
                extendedPrice[fairlyExpensive] = costs[fairlyExpensive] * quantityOfAnimals[fairlyExpensive];

                for (i = 0; i < 3; ++i)
                {
                    totalExtendedPrice += extendedPrice[i];
                }
                
            }

            if (totalExtendedPrice < 100 && totalQuantityOfAnimals < 100)
            {
                ++quantityOfAnimals[mostExpensive];
                ++totalQuantityOfAnimals;
                totalExtendedPrice = 0;
                extendedPrice[mostExpensive] = costs[mostExpensive] * quantityOfAnimals[mostExpensive];

                for (i = 0; i < 3; ++i)
                {
                    totalExtendedPrice += extendedPrice[i];

                }

            }

            
        }
    }
}