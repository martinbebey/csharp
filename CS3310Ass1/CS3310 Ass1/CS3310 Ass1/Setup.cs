using System;
using BinarySearchTree;
using System.IO;
using System.Linq;

public class Setup
{ 

    public void CountryDataTableBuilder()
    {
        TheLog theLog = new TheLog();
        theLog.StatusUpdate("Setup started", 0 );
        int count = 0;
        RawData rawData = new RawData();
        rawData.Data = rawData.GetData();
        CountryDataTable<string> countries = new CountryDataTable<string>();

        foreach (string countryInfo in rawData.Data)
        {
            ++count;
            countries.Add(countryInfo);
            //country.Name = countryInfo.Split('\'')[3];
            //country.Code = countryInfo.Split('\'')[1];
            //country.Continent = countryInfo.Split('\'')[5];
            //country.Area = int.Parse(countryInfo.Split('\'')[4]);
            //country.Population = int.Parse(countryInfo.Split('\'')[6]);
            //country.LifeExpectancy = float.Parse(countryInfo.Split(',')[7]);            
        }

        theLog.StatusUpdate("Setup finsished", count);
    }
}