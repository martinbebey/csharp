using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPaper
{
    class AmazonDemo1
    {
        // IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
        // SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
        // DEFINE ANY CLASS AND METHOD NEEDED
        // CLASS BEGINS, THIS CLASS IS REQUIRED
         //METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int[] cellCompete(int[] cells, int k)
        {
            // INSERT YOUR CODE HERE
            // temp [] array
            int[] temp = new int[8];
            for (int i = 0; i < 8; i++)
                temp[i] = cells[i];

            // Iterate for k days
            while (k-- > 0)
            {

                // Finding next values 
                // for corner cells
                temp[0] = 0 ^ cells[1];
                temp[8 - 1] = 0 ^ cells[8 - 2];

                // Compute values of intermediate cells
                // If both cells active or inactive, then 
                // temp[i]=0 else temp[i] = 1.
                for (int i = 1; i <= 8 - 2; i++)
                    temp[i] = cells[i - 1] ^ cells[i + 1];

                // Copy temp[] to cells[] 
                // for next iteration
                for (int i = 0; i < 8; i++)
                    cells[i] = temp[i];
            }

            return cells;

            // METHOD SIGNATURE ENDS
        }
    }
}
