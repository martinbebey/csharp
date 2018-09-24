using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySearchTree
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool IsEmpty();
        int Size();
    }

    public interface ISearchable<T> : IContainer<T>
    {
        void Add(T item);
        void Remove(T item);
        bool Contains(T item);
    }

   public class BinarySearchTree<T> : ISearchable<T> where T : IComparable
    {
        private Node root;
        //public TypeOfTraversal traversalType;
        public BinarySearchTree()
        {
            root = null;
        }

        public void Add(T item)
        {
            root = Add(item, root);
        }

        private Node Add(T item, Node node)
        {
            if (node == null)
                return new Node(item);
            else
                if (item.CompareTo(node.Item) < 0)
                {
                    node.Left = Add(item, node.Left);
                    return node;
                }
                else
                    if (item.CompareTo(node.Item) > 0)
                    {
                        node.Right = Add(item, node.Right);
                        return node;
                    }
                    else
                        // Do nothing
                        return node;
        }

        public void Remove(T item)
        {
            root = Remove(item, root);
        }

        private Node Remove(T item, Node node)
        {
            if (node != null)
            {
                if (item.CompareTo(node.Item) < 0)
                    node.Left = Remove(item, node.Left);
                else
                    if (item.CompareTo(node.Item) > 0)
                        node.Right = Remove(item, node.Right);
                    else
                        // node has one or no children

                        if (node.Left == null)
                            node = node.Right;
                        else
                            if (node.Right == null)
                                node = node.Left;
                            else
                            {   
                                // node has two children

                                Node prev = node;
                                Node curr = node.Right;

                                // Find the leftmost node (curr) in the right subtree of node
                                while (curr.Left != null)
                                {
                                    prev = curr;
                                    curr = curr.Left;
                                }

                                // Assign the value of curr to node
                                node.Item = curr.Item;

                                // Remove curr
                                if (node.Right == curr)
                                    node.Right = curr.Right;
                                else
                                    prev.Left = curr.Right;
                            }                    
            }
            return node;
        }

        public bool Contains(T item)
        {
            return Contains(item, root);
        }

        private bool Contains(T item, Node node)
        {
            if (node == null)
                return false;
            else
                if (item.CompareTo(node.Item) == 0)
                    return true;
                else
                    if (item.CompareTo(node.Item) < 0)
                        return Contains(item, node.Left);
                    else
                        return Contains(item, node.Right);
        }

        public void MakeEmpty()
        {
            root = null;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public int Size()
        {
            return Size(root);
        }

        private int Size(Node node)
        {
            if (node == null)
                return 0;
            else
                return 1 + Size(node.Left) + Size(node.Right);
        }

        public void Print()
        {
            Print(root);
        }

        private void Print(Node node)
        {
            if (node != null)
            {           
                Print(node.Left);
                Console.WriteLine(node.Item.ToString());
                Print(node.Right);
            }
        }

        //public void Preorder(Node node)
        //{
        //    if (node != null)
        //    {
        //        Console.Write(node);
        //        Preorder(node.Left);
        //        Preorder(node.Right);
        //    }
        //}

        //public void Inorder(Node node)
        //{
        //    if (node != null)
        //    {
        //        Inorder(node.Left);
        //        Console.Write(node);
        //        Inorder(node.Right);
        //    }
        //}

        //public void Postorder(Node node)
        //{
        //    if (node != null)
        //    {
        //        Postorder(node.Left);
        //        Postorder(node.Right);
        //        Console.Write(node);
        //    }
        //}

        //public enum TypeOfTraversal { Preorder, Inorder, Postorder }
        //public TypeOfTraversal TraversalType
        //{
        //    set
        //    {
        //        traversalType = value;
        //    }
        //}

        
        public class Node
        {
            private T item;
            private Node left, right;

            public Node(T item)
            {
                this.item = item;
                left = right = null;
            }

            public Node Left
            {
                get { return left; }
                set { left = value; }
            }
            public Node Right
            {
                get { return right; }
                set { right = value; }
            }

            public T Item
            {
                get { return item; }
                set { item = value; }
            }
        }

        
    }
}
