using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class Algorithms
{

    public void pseudo()
    {
        Dictionary<int, int> dictionary = new Dictionary<int, int>();
        int[] array = new int[41];
        int[] array2 = new int[41];
        int key, value, collision = 0, index = 0, hits = 0;
        for (int number = 101; number < 111; ++number)
        {
            key = number;
            value = (17 * number + 7) % 41;
            dictionary.Add(key, value);
        }
        for (int number = 301; number < 311; ++number)
        {
            key = number;
            value = (17 * number + 7) % 41;
            dictionary.Add(key, value);
        }
        for (int number = 501; number < 511; ++number)
        {
            key = number;
            value = (17 * number + 7) % 41;
            dictionary.Add(key, value);
        }

        foreach (KeyValuePair<int, int> i in dictionary)
        {
            if (array2[i.Value] == 0)
            {
                array[i.Value] = i.Key;
                array2[i.Value] = 1;
            }

            else if (array2[i.Value] == 1)
            {
                if (array[i.Value] == i.Key)
                {
                    array[i.Value] = i.Key;
                    array2[i.Value] = 1;
                }

                else
                {
                    int entry = i.Value + index;

                    if (entry > 40)
                    {
                        entry = 0;
                    }

                    while (array2[entry] == 1)
                    {
                        ++hits;
                        ++collision;
                        index = collision * collision;

                        if (index + i.Value > 40)
                        {
                            entry = 0;
                        }

                        else
                        {
                            entry = index + i.Value;
                        }
                    }

                    array[entry] = i.Key;
                    array2[entry] = 1;
                    collision = 0;
                    index = 0;
                }

            }

        }

        Console.WriteLine("\n the number of collision using the pseudorandom is " + hits);
        

    }

    public static void Main()
    {
        Dictionary<int, int> dictionary = new Dictionary<int, int>();
        int[] array = new int[41];
        int[] array2 = new int[41];
        int key, value, collision = 0, index;

        for (int number = 101; number < 111; ++number)
        {
            key = number;
            value = number % 41;
            dictionary.Add(key, value);
        }

        for (int number = 301; number < 311; ++number)
        {
            key = number;
            value = number % 41;
            dictionary.Add(key, value);
        }

        for (int number = 501; number < 511; ++number)
        {
            key = number;
            value = number % 41;
            dictionary.Add(key, value);
        }

        foreach (KeyValuePair<int, int> i in dictionary)
        {
            if (array2[i.Value] == 0)
            {
                array[i.Value] = i.Key;
                array2[i.Value] = 1;
            }

            else if (array[i.Value] == i.Key)
            {
                array[i.Value] = i.Key;
                array2[i.Value] = 1;
            }

            else
            {

                index = i.Value;
                while (array2[index] == 1)
                {
                    ++collision;
                    if (index > 40)
                    {
                        index = 0;
                    }

                    else
                    {
                        ++index;
                    }
                }

                array[index] = i.Key;
                array2[index] = 1;
            }
        }

        Console.Write("The number of collision using the modulo-division is " + collision);
        Algorithms n = new Algorithms();
        n.pseudo();
        n.Rotation();
        Console.ReadLine();

    }

    public void Rotation()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            int[] array = new int[41];
            int[] array2 = new int[41];
            int key, value, collision = 0, index;
            string str;
            char a, b, c;
            int[] rotate = new int[3];
             

            for (int number = 101; number < 111; ++number)
            {
                key = number;
                str = Convert.ToString(number);
                a = str[2];
                b = str[1];
                c = str[0];
                rotate[0] = Convert.ToInt32(a);
                rotate[1] = Convert.ToInt32(c);
                rotate[2] = Convert.ToInt32(b);
                str = Convert.ToString(a) + Convert.ToString(c) + Convert.ToString(b);
                value = (Convert.ToInt32(str)) % 41;
                dictionary.Add(key, value);
            }

            for (int number = 301; number < 311; ++number)
            {
                key = number;
                str = Convert.ToString(number);
                a = str[2];
                b = str[1];
                c = str[0];
                rotate[0] = Convert.ToInt32(a);
                rotate[1] = Convert.ToInt32(c);
                rotate[2] = Convert.ToInt32(b);
                str = Convert.ToString(a) + Convert.ToString(c) + Convert.ToString(b);
                value = (Convert.ToInt32(str)) % 41;
                dictionary.Add(key, value);
            }

            for (int number = 501; number < 511; ++number)
            {
                key = number;
                str = Convert.ToString(number);
                a = str[2];
                b = str[1];
                c = str[0];
                rotate[0] = Convert.ToInt32(a);
                rotate[1] = Convert.ToInt32(c);
                rotate[2] = Convert.ToInt32(b);
                str = Convert.ToString(a) + Convert.ToString(c) + Convert.ToString(b);                
                value = (Convert.ToInt32(str)) % 41;
                dictionary.Add(key, value);
            }

            foreach (KeyValuePair<int, int> i in dictionary)
            {
                if (array2[i.Value] == 0)
                {
                    array[i.Value] = i.Key;
                    array2[i.Value] = 1;
                }

                else if (array[i.Value] == i.Key)
                {
                    array[i.Value] = i.Key;
                    array2[i.Value] = 1;
                }

                else
                {

                    index = i.Value;
                    while (array2[index] == 1)
                    {
                        ++collision;
                        if (index > 40)
                        {
                            index = 0;
                        }

                        else
                        {
                            ++index;
                        }
                    }

                    array[index] = i.Key;
                    array2[index] = 1;
                }
            }
            Console.WriteLine("the number of collision using rotation is " + collision);
            
            
        }
        
    }

