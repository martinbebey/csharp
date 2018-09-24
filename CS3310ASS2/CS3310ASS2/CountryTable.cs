/*This is the country Data Procedural class used to process the country data according to the Data obtained from the transdatafiles and raw files
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
        //entry is the data stored in the bin file representing a country's records in the specified format and length
        private string countryData, countryInfo, entry, filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS2\CS3310ASS2\bin\Debug\CountryData.bin";
        private int x = 0, size, result, sizeOfDataRec, highestID = 0, sizeOfHeaderRec, byteOffset, headerRec = 0, rrn;//x is used to ensure that the bin file is only created once and highestID is used to keep track of the highest position of the bin file to read the last data from
        private bool removed = false;
        private string[] countryTable = new string[300];
        private UTF8Encoding utf8 = new UTF8Encoding();
        private FileStream file;
        private BinaryReader binFileReader;
        private BinaryWriter binFileWriter;
        private StringBuilder stringBuilder = new StringBuilder();
        private string countryCode, name, continent;
        private float lifeExpectancy;
        private long population;
        private byte[] buffer;

        //**********************************************************************************************************************************

        public CountryDataTable(TheLog theLog)
        {
            file = File.Create(filePath);//creates the bin file
            theLog.Open();
            theLog.displayThis("FILE STATUS > CountryData FILE opened");
            file.Close();
        }

        //**********************************************************************************************************************************

        public void Open(TheLog theLog)// opens the bin file for writer
        {
            if (x == 0)
            {
                file = File.Create(filePath);
                file.Close();
                ++x;
            }

            binFileWriter = new BinaryWriter(File.Open(filePath, FileMode.Open), utf8);
        }

        //**********************************************************************************************************************************

        public void OpenRead(TheLog theLog)//opens the bin file for the reader
        {
            binFileReader = new BinaryReader(File.Open(filePath, FileMode.Open), utf8);
        }

        //**********************************************************************************************************************************

        //checks to see if a country is present by its id, using direct/random access
        public bool Find(short id, CountryDataTable countries, TheLog theLog, FileStream file, ref string countryInfo)
        {
            if (id != 0)//nothing is done if the id is 0. A false value is simply returned indicating that the country was not found
            {
                countries.OpenRead(theLog);
                buffer = new byte[sizeOfDataRec];

                binFileReader.BaseStream.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);//seeks position determined by id in the file
                binFileReader.Read(buffer, 0, sizeOfDataRec);//reads the corresponding data
                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);//converts it back to a string

                if (countryData != "" && int.TryParse(countryData.Substring(0, 2), out result) && id == int.Parse(countryData.Substring(0, 2)))//checks if IDs match
                {
                    binFileReader.Close();                    
                    countries.FinishUp(countries, theLog, false);
                    stringBuilder.AppendFormat("{0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 11:###,###,###} {5, 13:#,###,###,##0} {6, 4:00.0}", countryData.Substring(0, 2), countryData.Substring(2, 3), countryData.Substring(5, 17), countryData.Substring(22, 13), long.Parse(countryData.Substring(35, 9)), long.Parse(countryData.Substring(44, 9)), float.Parse(countryData.Substring(53, 4)));
                    countryInfo = stringBuilder.ToString();//updates country info
                    stringBuilder.Clear();
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

        //**********************************************************************************************************************************

        //calls the Find method to see if a country already exists in file
        public virtual bool Contains(short id, CountryDataTable countries, TheLog theLog, ref string countryInfo)
        {
            return this.Find(id, countries, theLog, file, ref countryInfo);
        }

        //**********************************************************************************************************************************

        private string BinaryToString(string data)//converts binary data back to normal text
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        //**********************************************************************************************************************************

        //adds/inserts a country to the country file using direct/random access
        public void Add(short id, string country, CountryDataTable countries, TheLog theLog)
        {

            if (!countries.Find(id, countries, theLog, file, ref countryInfo))//nothing is doneis the country already exists
            {
                if (id > highestID)//keep track ofwhere thelast record is so we know where to stop when printing them out
                {
                    highestID = id;
                }

                //various  records are obtained from the data
                name = country.Split('\'')[3].Trim().PadRight(17, ' ');

                if (name.Length > 17)//some names are way too long and only so many bytes are available so they are cut short
                {
                    name = name.Substring(0, 17);
                }

                countryCode = country.Split('\'')[1].Trim().PadRight(3, ' ');

                continent = country.Split('\'')[5].Trim().PadRight(13, ' '); ;

                size = int.Parse(country.Split(',')[5].Trim());

                population = long.Parse(country.Split(',')[7].Trim());

                lifeExpectancy = float.Parse(country.Split(',')[8].Trim());

                entry = id.ToString().PadLeft(2, '0') + countryCode + name + continent + size.ToString().PadLeft(8, '0') + population.ToString().PadLeft(10, '0') + lifeExpectancy.ToString().PadLeft(4, '0');
                sizeOfDataRec = entry.Length;
                sizeOfHeaderRec = sizeof(int);
                rrn = id;//in this assignment, the relative postion of the country data in the bin file is the same as their ID
                byteOffset = sizeOfHeaderRec + sizeOfDataRec * (rrn - 1);
                ++headerRec;//updates the number of country data in the file
                countries.Open(theLog);
                binFileWriter.Seek(0, SeekOrigin.Begin);

                binFileWriter.Write(Encoding.ASCII.GetBytes(headerRec.ToString().PadLeft(4, '0')), 0, sizeOfHeaderRec);//updates headerRec first
                binFileWriter.Seek(byteOffset, SeekOrigin.Begin);
                binFileWriter.Write(Encoding.ASCII.GetBytes(entry), 0, sizeOfDataRec);//the writes the data wherever it should be
                
                binFileWriter.Close();
                countries.FinishUp(countries, theLog, false);
            }           

        }

        //**********************************************************************************************************************************

        //public remove used to tomCountriesone selected countries in the table using the private method
        public virtual bool Remove(short id, CountryDataTable countries, TheLog theLog, ref string countryInfo)
        {
            return Remove(countries, theLog, id, ref countryInfo);
        }

        //**********************************************************************************************************************************

        //used to delete countries in the file. works similarly to the Find function but removes a country data once it is found by direct/random access
        private bool Remove(CountryDataTable countries, TheLog theLog, short id, ref string countryInfo)
        {
            if (id != 0)
            {
                countries.OpenRead(theLog);
                buffer = new byte[sizeOfDataRec];
                binFileReader.BaseStream.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);//reads specific position
                binFileReader.Close();
                countries.FinishUp(countries, theLog, false);
                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);//gets the corresponding string
                if (id == int.Parse(countryData.Substring(0, 2)))//check if IDs match
                {
                    stringBuilder.AppendFormat("{0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 11:###,###,###} {5, 13:#,###,###,##0} {6, 4:00.0}", countryData.Substring(0, 2), countryData.Substring(2, 3), countryData.Substring(5, 17), countryData.Substring(22, 13), long.Parse(countryData.Substring(35, 9)), long.Parse(countryData.Substring(44, 9)), float.Parse(countryData.Substring(53, 4)));
                    countryInfo = stringBuilder.ToString();//updates country info
                    stringBuilder.Clear();
                    countries.Open(theLog);
                    binFileWriter.Seek(sizeOfDataRec * (id - 1) + sizeOfHeaderRec, SeekOrigin.Begin);
                    countryData = " ";//removes the data by replacing it all with spaces
                    binFileWriter.Write(Encoding.ASCII.GetBytes(countryData.PadRight(sizeOfDataRec, ' ')), 0, sizeOfDataRec);
                    binFileWriter.Close();
                    
                    countries.FinishUp(countries, theLog, false);
                    --headerRec;
                    removed = true;
                    return removed;
                }

                //countries.FinishUp(countries, theLog, false);
                return removed;
            }

            else
            {
                removed = false;
                return removed;
            }
        }

        //**********************************************************************************************************************************

        //used for the SA transaction code (the log utility). It traverses the file and prints each country records using sequential access
        public void GetAll(CountryDataTable countries, TheLog theLog)
        {
            countries.OpenRead(theLog);
            theLog.displayThis("   ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");
            buffer = new byte[sizeOfDataRec];
            for (int i = 0; i < highestID; ++i)//loops through the file
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
                //each piece of data isconverted into a string and printed in the desired format
                if (int.TryParse(countryData.Substring(0, 1), out result) && countryData != null && countryData != "")
                {
                    stringBuilder.AppendFormat("   {0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 11:###,###,###} {5, 13:#,000,000,000} {6, 4:00.0}", countryData.Substring(0, 2), countryData.Substring(2, 3), countryData.Substring(5, 17), countryData.Substring(22, 13), long.Parse(countryData.Substring(35, 9)), long.Parse(countryData.Substring(44, 9)), float.Parse(countryData.Substring(53, 4)));
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            binFileReader.Close();          
        }

        //**********************************************************************************************************************************

        //Closes the bin file, starts the snapshot or not depending on the boolean value and updates thelog file accordingly
        public void FinishUp(CountryDataTable countries, TheLog theLog, bool snapshot)
        {
            file.Close();

            if (snapshot)//if true, snapshot is launched
            {
                theLog.displayThis("CODE STATUS > Snapshot started");                
                Console.WriteLine(" [RRN] ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");
                Snapshot(countries, theLog);
                theLog.displayThis(" +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                ++x;

                if (x == 3)
                {
                    theLog.displayThis("FILE STATUS > CountryData FILE closed");
                }

                Console.WriteLine(" +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                theLog.displayThis("CODE STATUS > Snapshot finished");
            }
        }

        //**********************************************************************************************************************************

        //performs the snapshot Similarly to the GetAll method, except here the RRN is printed but it is still the same as ID in this assignment
        public void Snapshot(CountryDataTable countries, TheLog theLog)
        {

            countries.OpenRead(theLog);
            theLog.displayThis("\nHeader Record: " + headerRec + "\n");
            theLog.displayThis(" [RRN] ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");
            buffer = new byte[sizeOfDataRec];
            for (int i = 0; i < highestID; ++i)//loops through the file reading each relative position
            {
                binFileReader.BaseStream.Seek(sizeOfDataRec * (i) + sizeOfHeaderRec, SeekOrigin.Begin);
                binFileReader.Read(buffer, 0, sizeOfDataRec);

                foreach (byte bytes in buffer)
                {
                    stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                }

                countryData = stringBuilder.ToString();
                stringBuilder.Clear();
                countryData = countries.BinaryToString(countryData);//changes each piece back to a string
                rrn = i + 1;// RRN starts at 1 excluding header record

                //this just prints every country data and ignores the unfilled areas in the bin file or those filled with spaces
                if (int.TryParse(countryData.Substring(0, 1), out result) && countryData != null && countryData != "")
                {                    
                    stringBuilder.AppendFormat(" [{0, -3:000}] {1, -3:000} {2, -3:000} {3, -18} {4, -13} {5, 11:###,###,###} {6, 13:#,000,000,000} {7, 4:00.0}", rrn, countryData.Substring(0, 2), countryData.Substring(2, 3), countryData.Substring(5, 17), countryData.Substring(22, 13), long.Parse(countryData.Substring(35, 9)), long.Parse(countryData.Substring(44, 9)), float.Parse(countryData.Substring(53, 4)));
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                else//prints a space for empty locations
                {
                    stringBuilder.AppendFormat(" [{0, -3:000}] EMPTY                                           ", rrn);
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            binFileReader.Close();
        }
    }
}