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
    class TreeNode<T> : IComparable where T : IComparable // a node class used for the binary tree
    {
        private T item;
        private String huffCode; // stores a 0 or a 1
        private TreeNode<T> left, right; // refferences to the right and left children

        public TreeNode(T item) // initializer
        {
            this.item = item;
            left = right = null;
        }

        public TreeNode<T> Left // left property
        {
            get { return left; }
            set { left = value; }
        }

        public TreeNode<T> Right // right property
        {
            get { return right; }
            set { right = value; }
        }

        public T Item // item property
        {
            get { return item; }
            set { item = value; }
        }

        public String HuffCode // huffman code property
        {
            get { return huffCode; }
            set { huffCode = value; }
        }

        public int CompareTo(object obj) // impliment IComparable
        {
            return item.CompareTo(((TreeNode<T>)obj).item);
        }
    }
}