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
        //result is used for integer type comparisons
        private string countryData, countryCode, name, continent, countryInfo, entry, filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\CountryData.bin";
        private int x = 0, size, result, sizeOfDataRec, MAX_N_LOC = 30, sizeOfHeaderRec, byteOffset, headerRec = 0, rrn;//x is used to ensure that the bin file is only created once and highestID is used to keep track of the highest position of the bin file to read the last data from
        private string[] countryTable = new string[300];
        private UTF8Encoding utf8 = new UTF8Encoding();
        private FileStream file;
        private BinaryReader binFileReader;
        private BinaryWriter binFileWriter;
        private StringBuilder stringBuilder = new StringBuilder();
        private float lifeExpectancy;
        private long population;
        private byte[] buffer;
        private short homeRRN;

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

        private short HashFuntion(short id, int MAX_N_LOC)//hash function for the country data table uses division/remainder method
        {
            homeRRN = Convert.ToInt16(id % MAX_N_LOC);

            if (homeRRN == 0)
            {
                homeRRN = Convert.ToInt16(MAX_N_LOC);
            }

            return homeRRN;
        }

        //**********************************************************************************************************************************

        //collision resolution for country data file using linear with wrap-around with embedded overflow
        public int CollisionResolution(int rrn)
        {
            rrn = (rrn + 1) % MAX_N_LOC;

            if(rrn == 0)
            {
                rrn = MAX_N_LOC;
            }

            return rrn;
        }

        //**********************************************************************************************************************************

        //checks to see if a country is present by its id, using direct/random access
        private bool Find(short id, CountryDataTable countries, TheLog theLog, FileStream file, ref int numberOfDataRecordsRead, ref string countryInfo, bool searchByCode)
        {
            rrn = HashFuntion(id, MAX_N_LOC);
            int originalRRN = rrn;//this is used to know when all possible locations have been searched, given a specific id/rrn
            countries.OpenRead(theLog);

            do
            {
                if (rrn != 0)//nothing is done if the id is 0. A false value is simply returned indicating that the country was not found
                {
                    
                    buffer = new byte[sizeOfDataRec];

                    binFileReader.BaseStream.Seek(sizeOfDataRec * (rrn - 1) + sizeOfHeaderRec, SeekOrigin.Begin);//seeks position determined by id in the file
                    binFileReader.Read(buffer, 0, sizeOfDataRec);//reads the corresponding data
                    foreach (byte bytes in buffer)
                    {
                        stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                    }

                    countryData = stringBuilder.ToString();
                    stringBuilder.Clear();
                    countryData = countries.BinaryToString(countryData);//converts it back to a string
                    ++numberOfDataRecordsRead;

                    if (searchByCode)//this is used only by the transcode "SC" to display the country info if found as the result
                    {
                        binFileReader.Close();
                        countries.FinishUp(countries, theLog, false);
                        stringBuilder.AppendFormat("{0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 11:###,###,###} {5, 13:#,###,###,##0} {6, 4:00.0}", countryData.Substring(0, 3), countryData.Substring(3, 3), countryData.Substring(6, 17), countryData.Substring(23, 13), long.Parse(countryData.Substring(36, 8)), long.Parse(countryData.Substring(44, 10)), float.Parse(countryData.Substring(54, 4)));
                        countryInfo = stringBuilder.ToString();//updates country info
                        stringBuilder.Clear();
                        return true;
                    }

                    //this checks to see if a country is found, based on its ID
                    if (countryData != "" && int.TryParse(countryData.Substring(0, 3), out result) && id == int.Parse(countryData.Substring(0, 3)))//checks if IDs match
                    {
                        binFileReader.Close();
                        countries.FinishUp(countries, theLog, false);
                        stringBuilder.AppendFormat("{0, -3:000} {1, -3:000} {2, -18} {3, -13} {4, 11:###,###,###} {5, 13:#,###,###,##0} {6, 4:00.0}", countryData.Substring(0, 3), countryData.Substring(3, 3), countryData.Substring(6, 17), countryData.Substring(23, 13), long.Parse(countryData.Substring(36, 8)), long.Parse(countryData.Substring(44, 10)), float.Parse(countryData.Substring(54, 4)));
                        countryInfo = stringBuilder.ToString();//updates country info
                        stringBuilder.Clear();
                        return true;
                    }

                    else //if country was not found, check the next location according to the resolution algorithm
                    {
                        rrn = CollisionResolution(rrn);
                    }
                }
            }
            while (rrn != originalRRN);

            binFileReader.Close();//closes the file
            countries.FinishUp(countries, theLog, false);
            return false;// country was not found
        }

        //**********************************************************************************************************************************

        //calls the Find method to see if a country already exists in file
        public virtual bool Contains(short id, CountryDataTable countries, TheLog theLog, ref int numberOfDataRecordsRead, ref string countryInfo, bool searchByCode)
        {
            return this.Find(id, countries, theLog, file, ref numberOfDataRecordsRead, ref countryInfo, searchByCode);
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

        //adds/inserts a country to the country file using direct/random access, by id
        public int Add(short id, string country, CountryDataTable countries, TheLog theLog, ref int numberOfDataRecordsRead)
        {         

            if (!countries.Find(id, countries, theLog, file, ref numberOfDataRecordsRead, ref countryInfo, false))//nothing is done if the country already exists
            {
                rrn = HashFuntion(id, MAX_N_LOC);
                int originalRNN = rrn;//this is used to know when all possible locations have been searched, given a specific id/rrn

                do
                {
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
                    entry = id.ToString().PadLeft(3, '0') + countryCode + name + continent + size.ToString().PadLeft(8, '0') + population.ToString().PadLeft(10, '0') + lifeExpectancy.ToString().PadLeft(4, '0');
                    sizeOfDataRec = entry.Length;
                    sizeOfHeaderRec = sizeof(int);
                    byteOffset = sizeOfHeaderRec + sizeOfDataRec * (rrn - 1);
                    countries.OpenRead(theLog);
                    buffer = new byte[sizeOfDataRec];
                    binFileReader.BaseStream.Seek(sizeOfDataRec * (rrn - 1) + sizeOfHeaderRec, SeekOrigin.Begin);//seeks position determined by id in the file
                    binFileReader.Read(buffer, 0, sizeOfDataRec);//reads the corresponding data

                    foreach (byte bytes in buffer)
                    {
                        stringBuilder.Append(Convert.ToString(bytes, 2).PadLeft(8, '0'));
                    }

                    countryData = stringBuilder.ToString();
                    stringBuilder.Clear();
                    countryData = countries.BinaryToString(countryData);//converts it back to a string
                    binFileReader.Close();

                    if (countryData == "" || !int.TryParse(countryData.Substring(0, 3), out result) || countryData == null)//checks if location is free
                    {
                        
                        ++headerRec;//updates the number of country data in the file
                        countries.Open(theLog);
                        binFileWriter.Seek(0, SeekOrigin.Begin);
                        binFileWriter.Write(Encoding.ASCII.GetBytes(headerRec.ToString().PadLeft(4, '0')), 0, sizeOfHeaderRec);//updates headerRec first
                        binFileWriter.Seek(byteOffset, SeekOrigin.Begin);
                        binFileWriter.Write(Encoding.ASCII.GetBytes(entry), 0, sizeOfDataRec);//the writes the data wherever it should be
                        binFileWriter.Close();
                        originalRNN = rrn;
                    }

                    else if(id == int.Parse(countryData.Substring(0, 3)))//checks if IDs match so that a country is not inserted twice
                    {
                        originalRNN = rrn;
                    }

                    else//if spot is not free and IDs don't match collision resolution is used to check another location
                    {
                        rrn = CollisionResolution(rrn);
                    }
                }

                while (rrn != originalRNN);
            }

            return rrn;

        }

        //**********************************************************************************************************************************

        //public remove used to tomCountriesone selected countries in the table using the private method
        public virtual bool Remove(short id, CountryDataTable countries, TheLog theLog)
        {
            return DeleteById(countries, theLog, id);
        }

        //**********************************************************************************************************************************

        //used to delete countries in the file, by ID
        private bool DeleteById(CountryDataTable countries, TheLog theLog, short id)
        {
            theLog.displayThis("SORRY, deleteById not yet working\n");
            Console.WriteLine("SORRY, deleteById not yet working\n");
            return false;
        }

        //**********************************************************************************************************************************

        //used for the SA transaction code (the log utility). It traverses the file and prints each country records using sequential access
        public void GetAll(CountryDataTable countries, TheLog theLog)
        {
            //there is no SA command in this assignment
        }

        //**********************************************************************************************************************************

        //Closes the bin file, starts the snapshot or not depending on the boolean value and updates thelog file accordingly
        public void FinishUp(CountryDataTable countries, TheLog theLog, bool snapshot)
        {   
            file.Close();

            if (snapshot)//if true, snapshot is launched
            {
                theLog.displayThis("CODE STATUS > Snapshot started");
                Console.WriteLine("\n [RRN] ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");
                Snapshot(countries, theLog);
                theLog.displayThis(" +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                ++x;

                if (x == 3)
                {
                    theLog.displayThis("FILE STATUS > CountryData FILE closed");
                }
                
                Console.WriteLine(" +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
            }
        }

        //**********************************************************************************************************************************

        //performs the snapshot for the country data table
        public void Snapshot(CountryDataTable countries, TheLog theLog)
        {
           
            countries.OpenRead(theLog);
            theLog.displayThis("\nHeader Record: " + headerRec + "  MAX_N_HOME_LOC: " + MAX_N_LOC + "\n");
            theLog.displayThis(" [RRN] ID  CDE NAME-------------- CONTINENT----  ------AREA ---POPULATION LIFE");
            buffer = new byte[sizeOfDataRec];
            for (int i = 0; i < MAX_N_LOC; ++i)//loops through the file reading each relative position
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

                //this if/else just prints every country data and empty spaces for empty records
                if (int.TryParse(countryData.Substring(0, 1), out result) && countryData != null && countryData != "")
                {
                    stringBuilder.AppendFormat(" [{0, -3:000}] {1, -3:000} {2, -3:000} {3, -18} {4, -13} {5, 11:###,###,###} {6, 13:#,###,###,##0} {7, 4:00.0}", rrn, countryData.Substring(0, 3), countryData.Substring(3, 3), countryData.Substring(6, 17), countryData.Substring(23, 13), long.Parse(countryData.Substring(36, 8)), long.Parse(countryData.Substring(44, 10)), float.Parse(countryData.Substring(54, 4)));
                    Console.WriteLine(stringBuilder.ToString());
                    theLog.displayThis(stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                else
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