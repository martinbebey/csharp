using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPaper
{
    class AmazonShortestDelivery
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int[][] ClosestXdestinations(int numDestinations, int[][] allLocations, int numDeliveries)
        {
            // WRITE YOUR CODE HERE
            int[][] deliveryDestinations = new int[numDestinations][];
            double[] distances = new double[allLocations.Length];
            int i = 0, entry;
            double smallestDistance = 0;
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
                    if (distances[i] <= smallestDistance)
                    {
                        smallestDistance = distances[i];
                        point = allLocations[i];
                    }
                }

                deliveryDestinations[entry] = point;
            }

            return deliveryDestinations;
        }
        // METHOD SIGNATURE ENDS
    }
}
