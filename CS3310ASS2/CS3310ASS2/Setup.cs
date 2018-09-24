/*This is the setup procedural class used to build the the country data table implemented as a binary file using data obtained from the raw data files
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
    private short id;

    //**********************************************************************************************************************************

    public Setup()
    {
        countries = new CountryDataTable(theLog);
        count = 0;
    }

    //**********************************************************************************************************************************

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
        rawData.Data = " ";

        while (rawData.Data != "x")//adds each  country in rawdata sample file to the country file one line at a time
        {
            rawData.Data = rawData.GetDataA2(theLog);//retrieves the sample raw data one line at a time
            if (rawData.Data != "x")
            {
                ++count;
                id = Convert.ToInt16(rawData.Data.Split('(')[1].Split(',')[0]);
                countries.Add(id, rawData.Data, countries, theLog);
            }
        }

        theLog.displayThis("CODE STATUS > Setup finsished - " + count + " countries processed");//updates the log
        countries.FinishUp(countries, theLog, true);
    }
}