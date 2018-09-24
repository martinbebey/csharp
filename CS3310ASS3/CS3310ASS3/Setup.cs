/*This is the setup procedural class used to build the the country data table implemented as a binary file using data obtained from the raw data files
 * it also builds creates a backup file for the index table before it stops executing
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//using namespaces
using Countries;
using Country;
using System.IO;
using System.Linq;

public class Setup
{
    private int count, numberOfDataRecordsRead = 0, drp, numberOfIndexTableNodes;//counts the number of countries processed into the country data table
    private RawData rawData;//raw data object instance used to access raw data files
    private TheLog theLog = new TheLog();
    private CountryDataTable countries;
    private CountryIndex country = new CountryIndex();
    private short id;
    private string filePath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS3\CS3310ASS3\bin\Debug\CountryIndex.txt";
    private StreamWriter indexFileWriter;

    //**********************************************************************************************************************************

    public Setup()//setup constructor
    {
        countries = new CountryDataTable(theLog);
        count = 0;
    }

    //**********************************************************************************************************************************

    //property
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

    //this method builds from the raw data sample file or calls another method to build from the rawDataAll file
    public void CountryDataTableBuilder()
    {
        theLog.displayThis("CODE STATUS > Setup started");//updates the code status in the log file
        rawData = new RawData(theLog);
        rawData.Data = " ";//retrieves the sample raw data

        while (rawData.Data != "x")//adds each  country in rawdata sample file to the country file and index table and counts as it goes
        {
            rawData.Data = rawData.GetDataA3(theLog);
            if (rawData.Data != "" && rawData.Data != "x")
            {
                ++count;
                id = Convert.ToInt16(rawData.Data.Split('(')[1].Split(',')[0]);
                drp = countries.Add(id, rawData.Data, countries, theLog, ref numberOfDataRecordsRead);
                numberOfDataRecordsRead = 0;
                country.Add(country.LinkedList, rawData.Data, drp);
            }
        }

        indexFileWriter = new StreamWriter(filePath, false);
        numberOfIndexTableNodes = count;

        foreach (Node node in country.LinkedList)//loop fills in the index backup file using the index table
        {
            if (node == null && numberOfIndexTableNodes != 0)
            {
                indexFileWriter.WriteLine(",,-1");
            }

            else if(numberOfIndexTableNodes != 0)
            {
                indexFileWriter.WriteLine(node.CountryCode + "," + node.DRP + "," + node.Link);
                --numberOfIndexTableNodes;
            }
        }

        indexFileWriter.Close();
        theLog.displayThis("CODE STATUS > Setup finsished - " + count + " countries processed");//updates the log
        countries.FinishUp(countries, theLog, true);//calls snapshot after binary file and index backup have been created
        country.FinishUp(country.LinkedList, theLog, true);
    }

    //**********************************************************************************************************************************

    public void FinishUp()
    {        
        countries.FinishUp(countries, theLog, true);
        country.FinishUp(country.LinkedList, theLog, true);
        theLog.FinishUp();
    }
}