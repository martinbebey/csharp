using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPaper
{
    class AmazonDemo2 //program to return highest common divisor of array
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int generalizedGCD(int num, int[] arr)
        {
            // WRITE YOUR CODE HERE
            int result = arr[0];
            for (int i = 1; i < num; i++)
                result = gcd(arr[i], result);

            return result;
        }
        // METHOD SIGNATURE ENDS

        static int gcd(int a, int b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }
    }
}
