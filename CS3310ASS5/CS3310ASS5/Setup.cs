/*This is the SetupUtility class
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */


using System;
using System.IO;
using System.Text;

public class SetupUtility
{
    private string roadsFilePath, cityNamesFilePath, mapData, cityName, cityNameFileData;
    private string[] cityNameArray;
    private FileStream roads;
    private StreamWriter cityNames;
    private BinaryWriter roadsFileWriter;
    private BinaryReader roadsFileReader;
    private UTF8Encoding utf8 = new UTF8Encoding();
    private StreamReader cityNamesReader;
    private StringBuilder stringBuilder = new StringBuilder();
    private MapData MapDatas;
    private UI ui;
    private TheLog theLog = new TheLog();
    private bool directedGraph = false;
    private int i, x, y, sizeOfARecord, sizeOfHeaderRecord, byteOffset, z ;//i x y z just used in loops
    private short[,] adjacencyMatrix;
    private short numberOfCityNames = 0, result, arrayIndex = 0, index = 0, distance;

    //**********************************************************************************************************************************

    public TheLog TheLog
    {
        get
        {
            return theLog;
        }

        set
        {
            theLog = value;
        }
    }

    //**********************************************************************************************************************************

    public void BuildMap(string fileNameSuffix, SetupUtility setup)//builds the binary file matrix
    {
        cityNamesFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "CityNames.txt";//path to the file on my PC
        roadsFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "Roads.bin";
        roads = File.Create(roadsFilePath);
        roads.Close();

        if (z == 0)
        {
            theLog.Open();
            ++z;
        }

        cityNames = new StreamWriter(cityNamesFilePath, false);
        arrayIndex = index = 0;
        roadsFileWriter = new BinaryWriter(File.Open(roadsFilePath, FileMode.Open), utf8);
        MapDatas = new MapData(fileNameSuffix);
        sizeOfHeaderRecord = sizeof(short);
        ui = new UI(fileNameSuffix, setup);
        mapData = " ";

        while (mapData != "x")// while not end of mapdata data is obtained
        {
            MapDatas.Data = MapDatas.GetTransData(fileNameSuffix);
            mapData = MapDatas.Data;

            if (mapData != "" && mapData.Substring(0, 1) != "%")// skip comments in the file
            {
                if (mapData.Substring(0, 1) == "D" && mapData.Substring(1, 1) == " ")//if it's a directed graph
                {
                    directedGraph = true;
                    numberOfCityNames = short.Parse(mapData.Split(' ')[1].Trim());
                    adjacencyMatrix = new short[numberOfCityNames, numberOfCityNames];
                    cityNameArray = new string[numberOfCityNames];
                    sizeOfARecord = numberOfCityNames * sizeof(short);
                }

                else if (mapData.Substring(0, 1) == "U")//if it's not directed
                {
                    directedGraph = false;
                    numberOfCityNames = short.Parse(mapData.Split(' ')[1].Trim());
                    adjacencyMatrix = new short[numberOfCityNames, numberOfCityNames];
                    cityNameArray = new string[numberOfCityNames];
                    sizeOfARecord = numberOfCityNames * sizeof(short);
                }

                else if (!short.TryParse(mapData.Substring(0, 1), out result))// get city names
                {
                    cityName = mapData;
                    arrayIndex = index;

                    if (arrayIndex < numberOfCityNames)
                    {
                        cityNameArray[arrayIndex] = cityName;
                    }

                    ++index;
                }

                else// fill in the matrix with distances
                {
                    adjacencyMatrix[short.Parse(mapData.Split(' ')[0]), short.Parse(mapData.Split(' ')[1])] = short.Parse(mapData.Split(' ')[2]);

                    if (!directedGraph)//if its not directed set A -> B = B -> A
                    {
                        adjacencyMatrix[short.Parse(mapData.Split(' ')[1]), short.Parse(mapData.Split(' ')[0])] = short.Parse(mapData.Split(' ')[2]);
                    }
                }
            }

        }
            cityNames.WriteLine(fileNameSuffix + " " + numberOfCityNames + "\n");// fill in city names files

            for (i = 0; i < arrayIndex; ++i )
            {
                cityNames.WriteLine(cityNameArray[i]);// fill up city name file
            }

            for (y = 0; y < numberOfCityNames; ++y)//put infinity in blank spaces
            {
                for (x = 0; x < numberOfCityNames; ++x)
                {
                    if (adjacencyMatrix[y, x] == 0)
                    {
                        adjacencyMatrix[y, x] = short.MaxValue;
                    }
                }
            }

            for (i = 0; i < numberOfCityNames; ++i )//put 0s in diagonal
            {
                adjacencyMatrix[i, i] = 0;
            }           

            //now to fill in the roads.bin file
            roadsFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            roadsFileWriter.Write(numberOfCityNames);

            for (x = 0; x < numberOfCityNames; ++x)// for each row in adjacency matrix
            {
                for (y = 0; y < numberOfCityNames; ++y)//for each column in a row
                {
                    roadsFileWriter.Write(adjacencyMatrix[x,y]);//writes each records to the roads file
                }
            }
        

        cityNames.Close();
        roadsFileWriter.Close();
        PrettyPrint(numberOfCityNames);// pretty print
    }

    //**********************************************************************************************************************************

    public void PrettyPrint(int numberOfCityNames)// pretty prints the name files and road files
    {
        roadsFileReader = new BinaryReader(File.Open(roadsFilePath, FileMode.Open), utf8);
        cityNamesReader = new StreamReader(cityNamesFilePath, false);
        cityNameFileData = cityNamesReader.ReadLine();

        if(cityNameFileData != " ")
        {
            theLog.displayThis("Map Data:  " + cityNameFileData.Split(' ')[0] + "  Number of cities:  " + cityNameFileData.Split(' ')[1]);
        }

        while (!cityNamesReader.EndOfStream)// filling name files
        {
            cityNameFileData = cityNamesReader.ReadLine();

            if (cityNameFileData != " ")
            {
                theLog.displayThis(cityNameFileData);
            }
        }

        stringBuilder.Append("  ");

        for (x = 0; x < numberOfCityNames; ++x) //column numbers
        {
            stringBuilder.Append("     " + x);
        }

        ui.WriteThis(stringBuilder.ToString() + "\n");
        stringBuilder.Clear();

        //now lines
        stringBuilder.Append("   ");

        for (x = 0; x < numberOfCityNames; ++x) //lines
        {
            if (x >= 10)
            {
                stringBuilder.Append("-------");
            }

            else
            {
                stringBuilder.Append("------");
            }
        }

        ui.WriteThis(stringBuilder.ToString().Remove(stringBuilder.ToString().LastIndexOf('-')));
        stringBuilder.Clear();
        
        //now the rest of table (row numbers and distances)
        for(x = 0; x < numberOfCityNames; ++x)
        {

            stringBuilder.AppendFormat("{0, 2:#0}" + "|", x);

            byteOffset = sizeOfHeaderRecord + (sizeOfARecord * x);
            roadsFileReader.BaseStream.Seek(byteOffset, SeekOrigin.Begin);

            for (y = 0; y < numberOfCityNames; ++y)
            {
                if (y >= 10)
                {
                    distance = BitConverter.ToInt16(roadsFileReader.ReadBytes(2), 0);
                    stringBuilder.AppendFormat("{0, 6:###0} ", distance);
                }

                else
                {
                    distance = BitConverter.ToInt16(roadsFileReader.ReadBytes(2), 0);
                    stringBuilder.AppendFormat("{0, 5:###0} ", distance);
                }
            }

            ui.WriteThis(stringBuilder.ToString());            
            stringBuilder.Clear();
        }

        ui.WriteThis("");
        roadsFileReader.Close();
    }
}
