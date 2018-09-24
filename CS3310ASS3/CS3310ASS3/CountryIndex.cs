/*this is the country index class used to implement the country index table 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Countries;//country data table namespace

namespace Country
{
    public class CountryIndex
    {
        private int  i, nHome = 0, nextEmpty, subscript, collisions, character1, character2, character3, homeRRN, MAX_N_LOC = 20;//i used for looping purposes
        private Node[] linkedList = new Node[300];
        private StringBuilder stringBuilder = new StringBuilder();

        //******************************************************************************************************************************

        //Property
        public Node[] LinkedList
        {
            get
            {
                return linkedList;
            }

            set
            {
                linkedList = value;
            }
        }

        //**********************************************************************************************************************************

        private int HashFuntion(string code, int MAX_N_LOC)//the hash function
        {
            character1 = char.Parse(code.Substring(0, 1));
            character2 = char.Parse(code.Substring(1, 1));
            character3 = char.Parse(code.Substring(2, 1));

            homeRRN = (character1 * character2 * character3) % MAX_N_LOC;

            return homeRRN;
        }

        //**********************************************************************************************************************************

        //checks to see if a country is present in the index table by its code and updates its RRN so the country's info can be retrieved from the country data table
        public bool Find(Node[] linkedList, string key, ref int numberOfDataRecordsRead, ref int numberOfIndexNodesVisited, ref int countryRRN)
        {
            subscript = HashFuntion(key, MAX_N_LOC);

            while (subscript != -1)
            {
                if (linkedList[subscript] != null && linkedList[subscript].CountryCode == key)
                {
                    ++numberOfDataRecordsRead;
                    ++numberOfIndexNodesVisited;
                    countryRRN = linkedList[subscript].DRP;
                    return true;//country found
                }

                else
                {
                    ++numberOfIndexNodesVisited;
                    subscript = linkedList[subscript].Link;
                }
            }

            return false;//country not found
        }

        //**********************************************************************************************************************************

        //calls the Find method to see if a country already exists in file
        public virtual bool Contains(Node[] linkedList, string key, ref int numberOfDataRecordsRead, ref int numberOfIndexNodesVisited, ref int countryDRP)
        {
            return this.Find(linkedList, key, ref numberOfDataRecordsRead, ref numberOfIndexNodesVisited, ref countryDRP);
        }

        //**********************************************************************************************************************************

        public void InsertCodeInIndex(TheLog theLog)
        {
            theLog.displayThis("SORRY, insertCodeInIndex not yet working\n");
            Console.WriteLine("SORRY, insertCodeInIndex not yet working\n");
        }

        //***********************************************************************************************************************************************************

        //adds/inserts a country to the country index table using direct/random access
        public void Add(Node[] linkedList, string country, int drp)
        {           
            Node node = new Node();
            node.CountryCode = country.Split('\'')[1];
            node.DRP = drp;
            int originalSubscript = HashFuntion(node.CountryCode, MAX_N_LOC);

            if (linkedList[originalSubscript] == null)
            {                
                linkedList[originalSubscript] = node;
                ++nHome;
            }

                //nothing is done if the country with that code already exists
            else if (linkedList[originalSubscript].CountryCode != node.CountryCode)
            {
                subscript = CollisionResultion(subscript, linkedList, node, ref collisions);                
                linkedList[subscript] = node;
                linkedList[subscript].Link = linkedList[originalSubscript].Link;
                linkedList[originalSubscript].Link = subscript;
            }
        }

        //***********************************************************************************************************************************************************
   
        //public remove used to call the private delete method
        public virtual bool Remove(short id, CountryIndex countries, TheLog theLog)
        {
            return DeleteByCode(countries, theLog, id);
        }

        //**********************************************************************************************************************************

        //used to delete countries by code
        private bool DeleteByCode(CountryIndex countries, TheLog theLog, short id)
        {
            theLog.displayThis("SORRY, deleteByCode not yet working\n");
            Console.WriteLine("SORRY, deleteByCode not yet working\n");
            return false;
        }

        //**********************************************************************************************************************************

        //resolves collisions in the index table using chaining with separate overflow
        public int CollisionResultion(int subscript, Node[] linkedList,  Node node, ref int collisions)
        {
            if (linkedList[subscript] == null)
            {
                return subscript;
            }

            else
            {
                ++collisions;
                while (linkedList[nextEmpty] != null)
                {
                    nextEmpty = MAX_N_LOC - 1 + collisions;
                }             
                                
                return nextEmpty;
            }
        }

        //**********************************************************************************************************************************

        //continues the snapshot or not depending on the boolean value 
        public void FinishUp(Node[] linkedList, TheLog theLog, bool snapshot)
        {
            if (snapshot)
            {
                Snapshot(linkedList, theLog);
            }
        }

        //**********************************************************************************************************************************

        //performs the snapshot for the country index table
        public void Snapshot(Node[] linkedList, TheLog theLog)
        {
            theLog.displayThis("\nCODE INDEX> MAX_N_HOME_LOC: " + MAX_N_LOC + ", nHome: " + nHome + ", nColl: " + collisions + "\n");
            Console.WriteLine("\nCODE INDEX> MAX_N_HOME_LOC: " + MAX_N_LOC + ", nHome: " + nHome + ", nColl: " + collisions + "\n");
            Console.WriteLine("[SUB] CODE | DRP | LINK |");
            theLog.displayThis("[SUB] CODE | DRP | LINK |");

            for(i = 0; i <= nextEmpty; ++i)//loops through the table and prints out every node's content
            {
                if (linkedList[i] == null)
                {
                    stringBuilder.AppendFormat("[{0, -3:000}] EMPTY", i);
                    theLog.displayThis(stringBuilder.ToString());
                    Console.WriteLine(stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                else
                {
                    stringBuilder.AppendFormat("[{0, -3:000}] {1, -3}  | {2, -3:000} | {3, 3:#00}  |", i, linkedList[i].CountryCode, linkedList[i].DRP, linkedList[i].Link);
                    theLog.displayThis(stringBuilder.ToString());
                    Console.WriteLine(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            theLog.displayThis("\nCODE STATUS > Snapshot finished");
        }
    }

    //**********************************************************************************************************************************
    //**********************************************************************************************************************************

    public class Node//the node class
    {
        private string countryCode;
        private int drp, link = -1;

        //**********************************************************************************************************************************

        public string CountryCode
        {
            get
            {
                return countryCode;
            }

            set
            {
                countryCode = value;
            }
        }

        //**********************************************************************************************************************************

        public int DRP
        {
            get
            {
                return drp;
            }

            set
            {
                drp = value;
            }
        }

        //**********************************************************************************************************************************

        public int Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }
    }
}