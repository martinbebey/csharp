/*This is the Map object used to access city names and roads.bin files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */


using System;
using System.IO;
using System.Text;


public class Map
{
    private short N, i, cityNumber; // i used for looping
    private string[] cityNames;
    private string cityNamesFilePath, roadsFilePath, cityName;
    private UTF8Encoding utf8 = new UTF8Encoding();
    private StreamReader cityNamesFileReader;

    //**********************************************************************************************************************************

    public Map(string fileNameSuffix)//constructor
    {
        cityNamesFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "CityNames.txt";//path to the file on my PC
        roadsFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "Roads.bin";
        cityNamesFileReader = new StreamReader(cityNamesFilePath);
        LoadCityNameArray(fileNameSuffix);
        N = short.Parse(cityNames[0].Split(' ')[1]);
    }

    //**********************************************************************************************************************************

    public short n//property for N
    {
        get
        {
            return N;
        }

        set
        {
            N = value;
        }
    }

    //**********************************************************************************************************************************

    private void LoadCityNameArray(string fileNameSuffix)
    {
        cityNames = File.ReadAllLines(fileNameSuffix + "CityNames.txt");
    }

    //**********************************************************************************************************************************

    public void GetCityNumbers(ref short startCityNumber, ref short destinationCityNumber, string startCityName, string destinationCityName)
    {
        startCityNumber = WhatsCityNumber(startCityName);
        destinationCityNumber = WhatsCityNumber(destinationCityName);
    }

    //**********************************************************************************************************************************

    public string WhatsCityName(short cityNumber)
    {
        cityName = cityNames[cityNumber + 2];
        return cityName;
    }

    //**********************************************************************************************************************************

    public short WhatsCityNumber(string cityName)
    {
        for (i = 2; i < cityNames.Length; ++i)
        {
            if (cityNames[i] == cityName)
            {
                cityNumber = Convert.ToInt16(i - 2);
                return cityNumber;
            }
        }

        cityNumber = -1;
        return cityNumber;
    }

    //**********************************************************************************************************************************

    public void FinishUp()
    {
        cityNamesFileReader.Close();
    }
}