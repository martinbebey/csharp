using System; //just so i don't have errors

public class Ass1//don't mind the name
{
    public static int LinearProbing(int index, int[] list, int partNumber, ref int collisions) //takes in the address, array list and part number(key) respectively and the number of collisons
    {
        int original = index;//original is used to mark the starting point so that if we come back to it we  know the array is full
        bool resolution = false;//flag to know when to stop the method

        Console.WriteLine("Linear Probing");//to show the algorithm in use

        while (resolution == false)//until the collision has been resolved or cannot be done
        {
            if (list[index] == null)//if the address is free put the key in (obviously if this method is called that means there is a collision so this condition is also used the first time to double check if there is a collision)
            {
                list[index] = partNumber;//insert the key in the list
                resolution = true;//the collision is resolved
            }

            else
            {
                ++index;//add 1 to the address
                ++collisions;//used to count the number of collisions. this parameter should be passed to this method with an initial 0 value. 
                if (index == list.Length)//if we have reached the end of the array
                {
                    index = 0;//we start from the beginning
                }

                if (index == original)//if we come back to the starting index we know there is no place to insert the key
                {
                    resolution = true;//we assume resolution is true to stop executing the loop
                    Console.WriteLine("List is full! Cannot insert value.");//and print an error message
                }
            }
        }

        return index; //return the index where the key was inserted to keep track of it for deletion purposes. 
        //if this returned index is equal to the the index originally sent to this method, then you know the collision failed.
    }

    public static int KeyOffset(int key, int index, int[] list, ref int collisions)//key(part number), address and array list passed respectively and the number of collisions
    {
        bool resolution = false;//same purpose as in linear probing        
        int original = index;//original is used to mark the starting point so that if we come back to it we  know the array is full
                             //this algorithm produces the same collison path for the same key so i assume at some point it's gonna come back to he original index and when that happens the resolution has failed
       
        Console.WriteLine("Key Offset");//to show the algorithm in use

        while (resolution == false)//same as in linear probing
        {
            if (list[index] == null)
            {
                list[index] = key;
                resolution = true;
            }

            else//calculate new address
            {
                int oldAddress = index;
                int offset = Convert.ToInt32(Math.Round((double)key / list.Length));
                index = (offset + oldAddress) % list.Length;

                ++collisions; //should be sent by reference initially with a value of 0

                if (index == original)//if it starts from the beginning address again
                {
                    resolution = true;//resolution has failed so we want to get off the loop
                    Console.WriteLine("Cannot insert Key using this resolution algorithm");//and print an error message
                }
            }
        }

        return index;//return the index where the key was inserted to keep track of it for deletion purposes. 
        //if this returned index is equal to the the index originally sent to this method, then you know the collision failed.
    }
}//best algorithm should be the one with the least collisons/probes