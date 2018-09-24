//This program is used to test the performance of 2 algorithms by timing them
//CS5310
//By Martin Bebey

using System;
using System.Diagnostics; //for the clock

public class PrefixAverages
{
    public static void Main()//driver program
    {
        int z = 1, n; //z is used to make the program run 20 times to generate enough results to plot
        int arraySize = 50000;// array size or n is initialized to 50 000
        n = arraySize;

        while (z != 21)//20 iterations
        {
            Random random = new Random(); //random number generator
           
            int i;// for looping
            int[] X = new int[arraySize];//input array
            int[] A = new int[arraySize];//output array

            for (i = 0; i < arraySize; ++i)//filling in the input array with random values between -1000 and 1000
            {
                X[i] = random.Next(-1000, 1001);
            }

            Console.WriteLine("\nFor n = {0}", arraySize);

            A = PrefixAverages1(X);//time algorithm 1
            A = PrefixAverages2(X);//time algorithm 2

            arraySize += n;//n is incremented by 50 000 each time
            ++z;//loop index
        }

        Console.ReadKey();// to hold the screen after excution completes
    }

    public static int[] PrefixAverages1(int[] X)// the first algorithm with input array as parameter
    {
        Stopwatch stopwatch = new Stopwatch();// a stopwatch used to time the running time of the algorithm

        stopwatch.Start();//starting the watch

        int[] A = new int[X.Length];//ouput array initialized with same length as input array
        int i, j, a;//loop indexes and a variable a to keep track of averages

        for (i = 0; i < X.Length; ++i)//outter loop reinitializing a to 0 (redundant)
        {
            a = 0;

            for (j = 0; j <= i; ++j)//inner loop filling in the averages
            {
                a += X[j];
                A[i] = a / (i + 1);
            }
        }

        stopwatch.Stop();//watch stopped

        Console.WriteLine("PrefixAverages1 running time: {0}", stopwatch.Elapsed);//elapsed time displayed

        return A;//output array is returned 

    }

    public static int[] PrefixAverages2(int[] X)// the 2nd algorithm with input array as parameter
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        int[] A = new int[X.Length];
        int i, s = 0;//s used to keep track of averages

        for (i = 0; i < X.Length; ++i)//like the inner loop of algorithm 1
        {
            s += X[i];
            A[i] = s / (i + 1);
        }

        stopwatch.Stop();//timer stopped

        Console.WriteLine("PrefixAverages2 running time: {0}", stopwatch.Elapsed);//time elapsed for this algorithm displayed

        return A;//output array is returned
    }
}