using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BST
{
    public class BSTree
    {
        private Node[] countryDataTable = new Node[239];
        private int rootPtr, n, nextEmpty;

        public BSTree()
        {
            rootPtr = -1;
            n = nextEmpty = 0;
        }

        public Node Find(string name, ref int count)
        {
            Node node = countryDataTable[rootPtr];
            while (node != null)
            {
                ++count;
                if (node.Name.Equals(name) && !node.Tombstoned)
                {
                    return node;
                }

                else
                {
                    //Search left if the value is smaller than the current node
                    bool searchLeft = name.CompareTo(node.Name) < 0;

                    if (searchLeft)
                        node = countryDataTable[node.LeftChild]; //search left
                    else
                        node = countryDataTable[node.RightChild]; //search right
                }
            }

            count = 0;
            return null; //not found

        }

        public virtual bool Contains(string name, ref int count)
        {
            return (this.Find(name, ref count) != null);
        }

        public void Add(string item)
        {
            Node node;

            if (rootPtr == -1)
            {               
                node = new Node(item);
                countryDataTable[nextEmpty] = node;
                node.Index = nextEmpty;
                ++rootPtr;
                ++nextEmpty;
                ++n;
            }

            else
            {
                node = Add(item, countryDataTable[rootPtr]);
                //countryDataTable[nextEmpty] = node;
                node.Index = nextEmpty;
                ++nextEmpty;
                ++n;
            }
        }

        private Node Add(string country, Node node)
        {
            if (node == null)
            {
                return new Node(country);
            }

            if (country.Split('\'')[3].CompareTo(node.Name) < 0)
            {
                if (node.LeftChild == -1)
                {
                    node.LeftChild = nextEmpty;
                }

                countryDataTable[node.LeftChild] = Add(country, countryDataTable[node.LeftChild]);
                return node;
            }

            else
            {
                if (country.Split('\'')[3].CompareTo(node.Name) >= 0)
                {
                    if (node.RightChild == -1)
                    {
                        node.RightChild = nextEmpty;
                    }

                    countryDataTable[node.RightChild] = Add(country, countryDataTable[node.RightChild]);
                    return node;
                }

                else
                {
                    //Do nothing
                    return node;
                }
            }
        }

        public void Remove(string country)
        {
            Remove(country, countryDataTable[rootPtr]);
        }

        private Node Remove(string country, Node node)
        {
            if (node != null)
            {
                if (country.Split('\'')[3].CompareTo(node.Name) < 0)
                {
                    //if(node.LeftChild == -1)
                    //{
                    //    node.LeftChild = nextEmpty;
                    //}

                    countryDataTable[node.LeftChild] = Remove(country, countryDataTable[node.LeftChild]);
                }

                else
                {
                    if (country.Split('\'')[3].CompareTo(node.Name) > 0)
                    {
                        countryDataTable[node.RightChild] = Remove(country, countryDataTable[node.RightChild]);
                    }

                    else

                        if (node.Tombstoned)
                        {
                            countryDataTable[node.RightChild] = Remove(country, countryDataTable[node.RightChild]);
                        }

                        else
                        {
                            node.Tombstoned = true;
                            --n;
                        }
                    //if (node.Left == null)
                    //    node = node.Right;
                    //else
                    //    if (node.Right == null)
                    //        node = node.Left;
                    //    else
                    //    {   
                    //        // node has two children

                    //        Node prev = node;
                    //        Node curr = node.Right;

                    //        // Find the leftmost node (curr) in the right subtree of node
                    //        while (curr.Left != null)
                    //        {
                    //            prev = curr;
                    //            curr = curr.Left;
                    //        }

                    //        // Assign the value of curr to node
                    //        node.Item = curr.Item;

                    //        // Remove curr
                    //        if (node.Right == curr)
                    //            node.Right = curr.Right;
                    //        else
                    //            prev.Left = curr.Right;
                    //    }                    
                }
            }

            return node;
        }
    }



    public class Node
    {
        private int leftChildPtr, rightChildPtr;
        private string code, name, continent;
        private int population, area, index;
        private float lifeExpectancy;
        private bool tombstoned = false;

        public Node(string information)
        {            
            index = leftChildPtr = rightChildPtr = -1;            
            name = information.Split('\'')[3];
            code = information.Split('\'')[1];
            continent = information.Split('\'')[5];
            area = int.Parse(information.Split('\'')[4]);
            population = int.Parse(information.Split('\'')[6]);
            lifeExpectancy = float.Parse(information.Split(',')[7]);                
        }

        public bool Tombstoned
        {
            get
            {
                return tombstoned;
            }

            set
            {
                tombstoned = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }
        // This accessor method returns the value of the radius
        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        // This accessor method returns the value of the radius
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }


        // This accessor method returns the value of the radius
        public string Continent
        {
            get
            {
                return continent;
            }

            set
            {
                continent = value;
            }
        }

        // This accessor method returns the value of the radius
        public int Population
        {
            get
            {
                return population;
            }

            set
            {
                population = value;
            }
        }



        // This accessor method returns the value of the radius
        public int Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }

        public float LifeExpectancy
        {
            get
            {
                return lifeExpectancy;
            }

            set
            {
                lifeExpectancy = value;
            }
        }

        public int LeftChild
        {
            get { return leftChildPtr; }
            set { leftChildPtr = value; }
        }
        public int RightChild
        {
            get { return rightChildPtr; }
            set { rightChildPtr = value; }
        }
    }

}