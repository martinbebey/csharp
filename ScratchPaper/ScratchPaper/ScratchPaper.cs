using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPaper
{
    class ScratchPaper
    {
        public static void Main()
        {
            int[][] deliverDestinations = new int[3][];
            int[][] allLocations = { new int[] { 3, 6 }, new int[] { 2, 4 }, new int[] { 5, 3 }, 
                                       new int[] { 2, 7 }, new int[] { 1, 8 }, new int[] { 7, 9 } };
            deliverDestinations = ClosestXdestinations(6, allLocations, 3);
            Console.ReadKey();
        }

        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        static int[][] ClosestXdestinations(int numDestinations, int[][] allLocations, int numDeliveries)
        {
            // WRITE YOUR CODE HERE
            int[][] deliveryDestinations = new int[numDeliveries][];
            double[] distances = new double[allLocations.Length];
            int i = 0, entry, val = 0;
            double smallestDistance = Int16.MaxValue;
            int[] point = new int[2];

            //first we compute all the distances
            foreach (int[] coordinates in allLocations)
            {
                distances[i] = Math.Sqrt(Math.Pow(coordinates[0], 2) + Math.Pow(coordinates[1], 2));
                ++i;
            }

            //next find the shortest distances and points
            for (entry = 0; entry < numDeliveries; ++entry)
            {
                for (i = 0; i < distances.Length; ++i)
                {
                    if (distances[i] < smallestDistance)
                    {
                        smallestDistance = distances[i];
                        point = allLocations[i];
                        val = i;
                    }
                }

                distances[val] = int.MaxValue;
                smallestDistance = int.MaxValue;
                deliveryDestinations[entry] = point;
            }

            return deliveryDestinations;
        }
        // METHOD SIGNATURE ENDS

        // Complete the firstRepeatedWord function below. Nexient hackerrank question - passed
        static string firstRepeatedWord(string s)
        {
            foreach(string word in s.Split(' '))
            {
                int count = 0;

                for(int i = 0; i < s.Split(' ').Length; ++i)
                {
                    if(word == s.Split(' ')[i])
                    {
                        ++count;

                        if(count == 2)
                        {
                            return word;
                        }
                    }
                }
            }

            return "no duplicate words";
        }

        // Complete the maxDifference function below - nexient hackerrank question - partial pass
        static int maxDifference(int[] a)
        {
            int maxDiff = -1; //a[1] - a[0];
            int i, j;

            for (i = 0; i < a.Length; i++)
            {
                for (j = i + 1 ; j < a.Length; ++j)
                {
                    if(a[j] - a[i] > maxDiff)
                    {
                        maxDiff = a[j] - a[i];
                    }
                }
            }

            return maxDiff;
        }

        // Complete the binaryOutput function below - nexient hackerrank question - need to print all string combinations
        static string binaryOutput(string binaryInput)
        {
            if (binaryInput == "" || binaryInput == null)
            {
                return "";
            }

            else
            {
                foreach (char bit in binaryInput)
                {
                    if (bit != '1' && bit != '0')
                    {
                        throw new System.ArgumentException("Input is invalid!");
                    }
                }

                
            }

            return binaryInput; //what the function should return besides an empt string is not specified in the question :|
        }

        // Complete the findNumber function below - hackerrank from - nexient test
        static string findNumber(List<int> arr, int k)
        {
            if(arr.Contains(k))
            {
                return "YES";
            }

            else
            {
                return "NO";
            }
        }

        //print all odd numbers between 2 numbers inclusive - hackerrank from nexient test
        static List<int> oddNumbers(int l, int r)
        {
            List<int> oddNumbers = new List<int>();
            int i;
   
            if(l % 2 == 0)
            {
               for (i = l + 1; i <= r; i+= 2)
                {
                    Console.WriteLine(i);
                    oddNumbers.Add(i);
                }
            }

            else
            {
                for (i = l; i <= r; i+= 2)
                {
                    Console.WriteLine(i);
                    oddNumbers.Add(i);
                }
            }

            return oddNumbers;
        }

        public static string Solution(string S)
        {
            
            bool swaped = true;
            char letter = 'z';

            while (swaped)
            {
                swaped = false;

                for (int i = 0; i < S.Length - 1 && i > -1; ++i)
                {
                    letter = S[i+1];
                    if (letter == S[i])
                    {
                        S = S.Remove(i, 2);
                        swaped = true;
                        --i;
                    }
                }
            }

            return S;
        }
    }
}
