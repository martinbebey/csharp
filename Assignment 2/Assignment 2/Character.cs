                        /*                             Assignment 2                             *
                         *                                                                      *
                         *                     Encoding and Decoding text                       *
                         *                                                                      *
                         *                              Edited by                               *
                         *                                                                      *
                         * * * * * * * * *    Sarah Rayfuse and Martin Bebey    * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_2
{
    class Character : IComparable
    {
        char character; // the letter
        int freq; // frequency of the letter

        public Character(char character, int freq) // initialize character and frequency
        {
            this.character = character;
            this.freq = freq;
        }

        public char Char
        {
            get { return character; } // return value of character
            set { character = value; } // sets the value of character with the contents of value
        }

        public int Freq
        {
            get { return freq; }  // return value of frequency
            set { freq = value; } // sets the value of freq with the contents of value
        }

        public int CompareTo(object obj)
        {
            return -1 * freq.CompareTo(((Character)obj).Freq);// comparing the frequency of characters
        }
    }
}
