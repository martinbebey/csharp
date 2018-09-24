/*This is the setup procedural class used to build the the country data table implemented as a Countries using data obtained from the raw data files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//using namespaces
using Countries;
using System.IO;
using System.Linq;

public class Setup
{
    private int count;//counts the number of countries processed into the country data table
    private RawData rawData;//raw data object instance used to access raw data files
    private TheLog theLog = new TheLog();
    private CountryDataTable countries;


    public Setup()
    {
        countries = new CountryDataTable(theLog);
        rawData = new RawData();
        count = 0;
    }

    public CountryDataTable Countries
    {
        get
        {
            return countries;
        }

        set
        {
            countries = value;
        }
    }

    //this method builds from the raw data sample file or calls another method to build from the rawDataAll file
    public void CountryDataTableBuilder()
    {
        theLog.Open();//opens the log file
        theLog.displayThis("CODE STATUS > Setup started");//updates the code status in the log file

        //if (buildRawDataAll)//if rawDataAll is to be read/built from, the method in this statement is called to read it
        //{
            //AllCountryDataTableBuilder(theLog, countries);
        //}

        //if (buildSample)//else the current method reads/builds from the raw data sample file
        //{

            rawData.Data = rawData.GetDataA2(theLog);//retrieves the sample raw data


            foreach (string countryInfo in rawData.Data)//adds each  country in rawdata sample file to the country data table and counts as it goes
            {
                ++count;
                countries.Add(Convert.ToInt16(countryInfo.Split('(')[1].Split(',')[0]), countryInfo, countries, theLog);
            }
        //}

        theLog.displayThis("CODE STATUS > Setup finsished - " + count + " countries processed");//updates the log
        countries.FinishUp(countries, theLog, true);
        theLog.FinishUp();//opens the log file
    }

    //public void AllCountryDataTableBuilder(TheLog theLog, CountryDataTable countries)//similarly to the above procedure, but builds from the rawDataAll file
    //{
    //    rawData.Data = rawData.GetDataAll(theLog);


    //    foreach (string countryInfo in rawData.Data)
    //    {
    //        ++count;
    //        countries.Add(countryInfo, countries);
    //    }

    //}
}