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
using System.Text.RegularExpressions;


namespace Countries
{
    public class CountryDataTable
    {
        //private Node[] countryDataTable;
        //private int rootPtr, n, nextEmpty, numberOfNodesDisplayed;
        private string countryData, entry, filePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\CountryData.bin";
        private int x = 0, size, result, sizeOfDataRec, highestID = 0, sizeOfHeaderRec, byteOffset, headerRec = 0, rrn;//index is the index of the node in the country data table array (used in snapshot)
        private bool removed = false;
        private string[] countryTable = new string[300];
        private UTF8Encoding utf8 = new UTF8Encoding();
        private FileStream file;
        //private byte[] fileBytes;
        private BinaryReader binFileReader;
        private BinaryWriter binFileWriter;
        private StringBuilder stringBuilder = new StringBuilder();
        private string countryCode, name, continent;
        private float lifeExpectancy;
        private long population;
        private byte[] buffer;

        public CountryDataTable(TheLog theLog)
        {
            file = File.Create(filePath);
            theLog.Open();
            theLog.displayThis("FILE STATUS > CountryData FILE opened");
            theLog.displayThis("FILE STATUS > CountryData FILE closed");
            file.Close();
            theLog.FinishUp();
            //rootPtr = -1;
            //countryDataTable = new Node[500];
            //stringBuilder = new StringBuilder();
            //n = nextEmpty = numberOfNodesDisplayed = 0;
        }

        public void Open(TheLog theLog)
        {
            if (x == 0)
            {
                file = File.Create(filePath);
                file.Close();
                ++x;
            }
            
            binFileWriter = new BinaryWriter(File.Open(filePath, FileMode.Open), utf8);
            
            theLog.displayThis("FILE STATUS > CountryData FILE opened");
        }

        public void OpenRead(TheLog theLog)
        {
            binFileReader = new BinaryReader(File.Open(filePath, FileMode.Open), utf8);
            theLog.displayThis("FILE STATUS > CountryData FILE opened");
        }

        //public int RootPointer
        //{
        //    get
        //    {
        //        return rootPtr;
        //    }

        //    set
        //    {
        //        rootPtr = value;
        //    }

        //}

        //public Node[] CountryDataTable
        //{
        //    get
        //    {
        //        return countryDataTable;
        //    }

        //    set
        //    {
        //        countryDataTable = value;
        //    }
        //}

        //public int NextEmpty
        //{
        //    get
        //    {
        //        return nextEmpty;
        //    }

        //    set
        //    {
        //        nextEmpty = value;
        //    }
        //}

        //checks to see if a country is present by its id, starting at the root
        public bool Find(int id, CountryDataTable countries, TheLog theLog, FileStream file)
        {
            if (id != 0)
            {
                countries.OpenRead(theLog);
                buffer = new byte[sizeOfDataRec];

                binFileReader.BaseStream.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);
                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);
                if (countryData != "" && int.TryParse(countryData.Substring(0, 2), out result) && id == int.Parse(countryData.Substring(0, 2)))
                {
                    binFileReader.Close();
                    countries.FinishUp(countries, theLog, false);
                    return true;
                }

                binFileReader.Close();
                countries.FinishUp(countries, theLog, false);
                return false;
            }

            else
            {
                return false;
            }
        }

        //calls the Find method to see if it contains the country
        public virtual bool Contains(int id, CountryDataTable countries, TheLog theLog)
        {
            return this.Find(id, countries, theLog, file);
        }

        //public method adds/inserts a country to the country table by calling the private method Add, then updates n, nextEmpty and the rootpointer if the tree was empty
        //public void Add(string item, CountryDataTable countries)
        //{
        //    countries.Add(item, countries);
        //    ++n;
        //}

        private string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        //adds/inserts a country to the country table
        public void Add(short id, string country, CountryDataTable countries, TheLog theLog)
        {           

                if (!countries.Find(id,countries, theLog, file))
                {
                    if (id > highestID)
                    {
                        highestID = id;
                    }

                    name = country.Split('\'')[3].Trim().PadRight(17, ' ');
                    int length = name.Length;
                    countryCode = country.Split('\'')[1].Trim().PadRight(3, ' ');
            
                    continent = country.Split('\'')[5].Trim().PadRight(13, ' '); ;
              
                    size = int.Parse(country.Split(',')[5].Trim());
                
                    population = long.Parse(country.Split(',')[7].Trim());
                   
                    lifeExpectancy = float.Parse(country.Split(',')[8].Trim());
                  
                    entry = id.ToString().PadLeft(2, '0') + countryCode + name + continent + size.ToString().PadLeft(8, '0') + population.ToString().PadLeft(10, '0') + lifeExpectancy.ToString().PadLeft(4, '0');
                    sizeOfDataRec = entry.Length;
                    sizeOfHeaderRec = sizeof(int);
                    byteOffset = sizeOfHeaderRec + sizeOfDataRec * (id - 1);
                    ++headerRec;
                    countries.Open(theLog);
                    binFileWriter.Seek(0, SeekOrigin.Begin);                   
               
                    binFileWriter.Write(Encoding.ASCII.GetBytes(headerRec.ToString().PadLeft(4, '0')), 0, sizeOfHeaderRec);
                    binFileWriter.Seek(byteOffset, SeekOrigin.Begin);
                    binFileWriter.Write(Encoding.ASCII.GetBytes(entry), 0, sizeOfDataRec);
                    binFileWriter.Close();                    
                }
            
        
            countries.FinishUp(countries, theLog, false);

        }

        //public remove used to tomCountriesone selected countries in the table using the private method
        public virtual bool Remove(int id, CountryDataTable countries, TheLog theLog)
        {
            return Remove(countries, theLog, id);
        }

        //used to tomCountriesone countries in the table. works similarly to the Add function
        private bool Remove(CountryDataTable countries, TheLog theLog, int id)
        {
            if (id != 0)
            {
                countries.OpenRead(theLog);
                buffer = new byte[sizeOfDataRec];
                binFileReader.BaseStream.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);
                binFileReader.Close();
                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);
                if (id == int.Parse(countryData.Substring(0, 2)))
                {
                    countries.Open(theLog);
                    binFileWriter.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);
                    countryData = " ";
                    binFileWriter.Write(Encoding.ASCII.GetBytes(countryData.PadRight(57, ' ')), 0, sizeOfDataRec);
                    binFileWriter.Close();
                    countries.FinishUp(countries, theLog, false);
                    removed = true;
                    return removed;
                }

                countries.FinishUp(countries, theLog, false);
                return removed;
            }

            else
            {
                removed = false;
                return removed;
            }
        }

        //preorder andpost order traversals are not used in this program. their Algorithm is just written as they are part of a Countries
        //public void Preorder(Node node)
        //{
        //    if (node != null)
        //    {
        //        Console.Write(node);
        //        Preorder(countryDataTable[node.LeftChild]);
        //        Preorder(countryDataTable[node.RightChild]);
        //    }
        //}

        //used for the SA transaction code (the log utility). It traverses the tree in order and prints out country information in the desired format
        public void GetAll(CountryDataTable countries, TheLog theLog)
        {
            countries.OpenRead(theLog);
            buffer = new byte[sizeOfDataRec];
            for (int i = 0; i < highestID; ++i)
            {
                binFileReader.BaseStream.Seek(sizeOfDataRec * (i) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);

                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);

                if (int.TryParse(countryData.Substring(0, 1), out result) && countryData != null && countryData != "")
                {
                    stringBuilder.AppendFormat("   {0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 10:##,###,###} {5, 13:#,###,###,###} {6, 4:00.0}", countryData.Substring(0, 2), countryData.Substring(3, 3), countryData.Substring(5, 16), countryData.Substring(21, 13), countryData.Substring(34, 8), countryData.Substring(42, 10), countryData.Substring(52, 4));
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            binFileReader.Close();
            countries.FinishUp(countries, theLog, false);
        }

        //public void Postorder(Node node)
        //{
        //    if (node != null)
        //    {
        //        Postorder(countryDataTable[node.LeftChild]);
        //        Postorder(countryDataTable[node.RightChild]);
        //        Console.Write(node);
        //    }
        //}

        //starts the snapshot or not depending on the boolean value and updates thelof file accordingly
        public void FinishUp(CountryDataTable countries, TheLog theLog, bool snapshot)
        {
            file.Close();
            if (snapshot)
            {
                theLog.displayThis("CODE STATUS > Snapshot started\n");
                theLog.displayThis("  ID  CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                Console.WriteLine("   ID  CDE NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
                //theLog.displayThis("N: " + n + ", NextEmpty: " + nextEmpty + ", RootPtr: " + rootPtr + "\n");
                

                Snapshot(countries, theLog);
                theLog.displayThis("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                theLog.displayThis("CODE STATUS > Snapshot finished");
            }
        }

        //performs the snapshot using a recursive inorder traversal
        public void Snapshot(CountryDataTable countries, TheLog theLog)
        {

            countries.OpenRead(theLog);
            buffer = new byte[sizeOfDataRec];
            for (int i = 0; i < highestID; ++i)
            {
                binFileReader.BaseStream.Seek(sizeOfDataRec * (i) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);
                
                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);

                if (int.TryParse(countryData.Substring(0, 1), out result) && countryData != null && countryData != "")
                {
                    stringBuilder.AppendFormat("   {0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 10:##,###,###} {5, 13:#,###,###,###} {6, 4:00.0}", countryData.Substring(0, 2), countryData.Substring(3, 3), countryData.Substring(5, 16), countryData.Substring(21, 13), countryData.Substring(34, 8), countryData.Substring(42, 10), countryData.Substring(52, 4));
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            binFileReader.Close();
            countries.FinishUp(countries, theLog, false);
        }

        ////defines each node and what it is made of
        //public class Node
        //{
        //    private int leftChildPtr = -1, rightChildPtr = -1;
        //    private string code, name, continent;
        //    private int population, area, index = -1;//index is the index of the node in the country data table array (used in snapshot)
        //    private float lifeExpectancy;
        //    private bool tomCountriesoned = false;

        //    //fills in the fields for a newly created node
        //    public Node(string information, CountryDataTable countries)
        //    {

        //        index = countries.NextEmpty;
        //        name = System.Text.RegularExpressions.Regex.Replace(information.Split(',')[1].Trim(), "'", "");
        //        code = information.Split('\'')[1].Trim();
        //        continent = information.Split('\'')[5].Trim();
        //        area = int.Parse(information.Split(',')[4].Trim());
        //        population = int.Parse(information.Split(',')[6].Trim());
        //        lifeExpectancy = float.Parse(information.Split(',')[7].Trim());
        //    }

        //    //public accessors
        //    public bool TomCountriesoned
        //    {
        //        get
        //        {
        //            return tomCountriesoned;
        //        }

        //        set
        //        {
        //            tomCountriesoned = value;
        //        }
        //    }

        //    public int Index
        //    {
        //        get
        //        {
        //            return index;
        //        }

        //        set
        //        {
        //            index = value;
        //        }
        //    }

        //    public string Code
        //    {
        //        get
        //        {
        //            return code;
        //        }

        //        set
        //        {
        //            code = value;
        //        }
        //    }

        //    public string Name
        //    {
        //        get
        //        {
        //            return name;
        //        }

        //        set
        //        {
        //            name = value;
        //        }
        //    }

        //    public string Continent
        //    {
        //        get
        //        {
        //            return continent;
        //        }

        //        set
        //        {
        //            continent = value;
        //        }
        //    }


        //    public int Population
        //    {
        //        get
        //        {
        //            return population;
        //        }

        //        set
        //        {
        //            population = value;
        //        }
        //    }




        //    public int Area
        //    {
        //        get
        //        {
        //            return area;
        //        }

        //        set
        //        {
        //            area = value;
        //        }
        //    }

        //    public float LifeExpectancy
        //    {
        //        get
        //        {
        //            return lifeExpectancy;
        //        }

        //        set
        //        {
        //            lifeExpectancy = value;
        //        }
        //    }

        //    public int LeftChild
        //    {
        //        get { return leftChildPtr; }
        //        set { leftChildPtr = value; }
        //    }

        //    public int RightChild
        //    {
        //        get { return rightChildPtr; }
        //        set { rightChildPtr = value; }
        //    }
        //}

    }
}