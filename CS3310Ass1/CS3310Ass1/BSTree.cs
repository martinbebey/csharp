/*This is the userAPP Procedural class used to process the country datatable according to the transData obtained from the transdatafiles
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BST
{
    public class BSTree
    {
        private Node[] countryDataTable;
        private int rootPtr, n, nextEmpty, numberOfNodesDisplayed;
        private StringBuilder stringBuilder;

        public BSTree()
        {
            rootPtr = -1;
            countryDataTable = new Node[500];
            stringBuilder = new StringBuilder();
            n = nextEmpty = numberOfNodesDisplayed = 0;
        }

        public int RootPointer
        {
            get
            {
                return rootPtr;
            }

            set
            {
                rootPtr = value;
            }

        }

        public Node[] CountryDataTable
        {
            get
            {
                return countryDataTable;
            }

            set
            {
                countryDataTable = value;
            }
        }

        public int NextEmpty
        {
            get
            {
                return nextEmpty;
            }

            set
            {
                nextEmpty = value;
            }
        }

        //checks to see if a country is present by its name, starting at the root
        public Node Find(string name, ref int count, BSTree countries)
        {
            Node node = countryDataTable[countries.RootPointer];
            while (node != null)
            {
                ++count;//counts the number of nodes visited

                if (node.Name.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase) && !node.Tombstoned)
                {
                    return node;
                }

                else
                {
                    //Search left if the value is smaller than the current node
                    bool searchLeft = String.Compare(name.Trim(), node.Name.Trim(), StringComparison.OrdinalIgnoreCase) < 0;

                    if (searchLeft)
                    {
                        if (node.LeftChild != -1)
                        {
                            node = countryDataTable[node.LeftChild]; //search left
                        }

                        else
                        {
                            return null;//country not found
                        }
                    }

                    else
                    {
                        if (node.RightChild != -1)
                        {
                            node = countryDataTable[node.RightChild]; //search right
                        }

                        else
                        {
                            return null;// not found
                        }
                    }
                }
            }

            return null; //not found

        }

        //calls the Find method to see if it contains the country
        public virtual bool Contains(string name, ref int count, BSTree countries)
        {
            return (this.Find(name, ref count, countries) != null);
        }

        //public method adds/inserts a country to the country table by calling the private method Add, then updates n, nextEmpty and the rootpointer if the tree was empty
        public void Add(string item, BSTree countries)
        {
            if (item != "Ad Info")
            {
                Node node;

                if (rootPtr == -1)
                {
                    node = new Node(item, countries);
                    countryDataTable[nextEmpty] = node;
                    ++rootPtr;
                    ++nextEmpty;
                    ++n;
                }

                else
                {
                    node = Add(item, countryDataTable[rootPtr], countries);                   
                    ++nextEmpty;
                    ++n;
                }
            }
        }

        //adds/inserts a country to the country table
        private Node Add(string country, Node node, BSTree countries)
        {
            if (node == null)
            {
                return new Node(country, countries);
            }

            //comparisons arecase insensitive and trailing spaces are ignored
            if (String.Compare(System.Text.RegularExpressions.Regex.Replace(country.Split(',')[1], "'", "").Trim(), node.Name.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
            {
                if (node.LeftChild == -1)
                {
                    node.LeftChild = nextEmpty;
                }

                countryDataTable[node.LeftChild] = Add(country, countryDataTable[node.LeftChild], countries);
                return node;
            }

            else
            {
                if (String.Compare(System.Text.RegularExpressions.Regex.Replace(country.Split(',')[1], "'", "").Trim(), node.Name.Trim(), StringComparison.OrdinalIgnoreCase) > 0)
                {
                    if (node.RightChild == -1)
                    {
                        node.RightChild = nextEmpty;
                    }

                    countryDataTable[node.RightChild] = Add(country, countryDataTable[node.RightChild], countries);
                    return node;
                }

                else
                {
                    if (node.Tombstoned)
                    {
                        if (node.RightChild == -1)
                        {
                            node.RightChild = nextEmpty;
                        }

                        countryDataTable[node.RightChild] = Add(country, countryDataTable[node.RightChild], countries);
                        return node;
                    }

                    else//if a country already exists and is not tombstoned these are decremented and incremented back in the public add method
                    {
                        --n;
                        --nextEmpty;
                        return node;
                    }
                }                
            }
        }        

        //public remove used to tombstone selected countries in the table using the private method
        public void Remove(string country, BSTree countries)
        {
            Remove(country, countryDataTable[countries.RootPointer]);
        }

        //used to tombstone countries in the table. works similarly to the Add function
        private Node Remove(string country, Node node)
        {
            if (node != null)
            {
                if (String.Compare(country.Trim(), node.Name.Trim(), StringComparison.OrdinalIgnoreCase) < 0)
                {
                    countryDataTable[node.LeftChild] = Remove(country, countryDataTable[node.LeftChild]);
                }

                else
                {
                    if (String.Compare(country.Trim(), node.Name.Trim(), StringComparison.OrdinalIgnoreCase) > 0)
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
                }
            }

            return node;
        }

        //preorder andpost order traversals are not used in this program. their Algorithm is just written as they are part of a BST
        public void Preorder(Node node)
        {
            if (node != null)
            {
                Console.Write(node);
                Preorder(countryDataTable[node.LeftChild]);
                Preorder(countryDataTable[node.RightChild]);
            }
        }

        //used for the SA transaction code (the log utility). It traverses the tree in order and prints out country information in the desired format
        public void Inorder(Node node, TheLog theLog)
        {
            
            if (node != null)
            {
                if (node.LeftChild != -1)
                {
                    Inorder(countryDataTable[node.LeftChild], theLog);
                }

                if (!node.Tombstoned)
                {
                    stringBuilder.AppendFormat("   {0, -3:000} {1, -18} {2, -13} {3, 10:##,###,###} {4, 13:#,###,###,###} {5, 4:00.0}", node.Code, node.Name, node.Continent, node.Area, node.Population, node.LifeExpectancy);
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }


                if (node.RightChild != -1)
                {
                    Inorder(countryDataTable[node.RightChild], theLog);
                }
            }
        }

        public void Postorder(Node node)
        {
            if (node != null)
            {
                Postorder(countryDataTable[node.LeftChild]);
                Postorder(countryDataTable[node.RightChild]);
                Console.Write(node);
            }
        }

        //starts the snapshot or not depending on the boolean value and updates thelof file accordingly
        public void FinishUp(bool snapshot, TheLog theLog, BSTree countries)
        {
            if (snapshot)
            {
                theLog.displayThis("CODE STATUS > Snapshot started\n");
                theLog.displayThis("N: " + n + ", NextEmpty: " + nextEmpty + ", RootPtr: " + rootPtr + "\n");
                theLog.displayThis("[SUB] CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE LCh RCh");

                Snapshot(countryDataTable[countries.RootPointer], theLog);
                theLog.displayThis("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                theLog.displayThis("CODE STATUS > Snapshot finished - " + numberOfNodesDisplayed + " nodes displayed");
            }
        }

        //performs the snapshot using a recursive inorder traversal
        public void Snapshot(Node node, TheLog theLog)
        {  
            if (node != null)
            {
                
                if (node.LeftChild != -1)
                {
                    Snapshot(countryDataTable[node.LeftChild], theLog);
                }

                    if (node.Tombstoned)
                    {
                        stringBuilder.AppendFormat("[{0, -3:000}] TOMBSTONE", node.Index);
                        theLog.displayThis(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }

                    if (!node.Tombstoned)
                    {
                        stringBuilder.AppendFormat("[{0, -3:000}] {3, -3} {4, -18} {5, 13} {6, 10:##,###,###} {7, 13:#,###,###,###} {8, -4:00.0} {1, 3:#00} {2, 3:#00}", node.Index, node.LeftChild, node.RightChild, node.Code, node.Name, node.Continent, node.Area, node.Population, node.LifeExpectancy);
                        theLog.displayThis(stringBuilder.ToString());
                        ++numberOfNodesDisplayed;
                        stringBuilder.Clear();
                    }
                

                if (node.RightChild != -1)
                {
                    Snapshot(countryDataTable[node.RightChild], theLog);
                }
            }            
            
        }             
    }

    //defines each node and what it is made of
    public class Node
    {
        private int leftChildPtr = -1, rightChildPtr = -1;
        private string code, name, continent;
        private int population, area, index = -1;//index is the index of the node in the country data table array (used in snapshot)
        private float lifeExpectancy;
        private bool tombstoned = false;

        //fills in the fields for a newly created node
        public Node(string information, BSTree countries)
        {

            index = countries.NextEmpty;
            name = System.Text.RegularExpressions.Regex.Replace(information.Split(',')[1].Trim(), "'", "");
            code = information.Split('\'')[1].Trim();
            continent = information.Split('\'')[5].Trim();
            area = int.Parse(information.Split(',')[4].Trim());
            population = int.Parse(information.Split(',')[6].Trim());
            lifeExpectancy = float.Parse(information.Split(',')[7].Trim());
        }

        //public accessors
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