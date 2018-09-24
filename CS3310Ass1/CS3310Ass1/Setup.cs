/*This is the setup procedural class used to build the the country data table implemented as a BST using data obtained from the raw data files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;//using namespaces
using BST;
using System.IO;
using System.Linq;

public class Setup
{
    private int count;//counts the number of countries processed into the country data table
    private RawData rawData;//raw data object instance used to access raw data files
   

    public Setup()
    {
        rawData = new RawData();
        count = 0;
    }

    //this method builds from the raw data sample file or calls another method to build from the rawDataAll file
    public void CountryDataTableBuilder(bool buildRawDataAll, bool buildSample, TheLog theLog, BSTree countries)
    {
        
        theLog.displayThis("CODE STATUS > Setup started");//updates the code status in the log file

        if(buildRawDataAll)//if rawDataAll is to be read/built from, the method in this statement is called to read it
        {
            AllCountryDataTableBuilder(theLog, countries);
        }

        if (buildSample)//else the current method reads/builds from the raw data sample file
        {            
            
            rawData.Data = rawData.GetDataSample(theLog);//retrieves the sample raw data
            

            foreach (string countryInfo in rawData.Data)//adds each  country in rawdata sample file to the country data table and counts as it goes
            {
                ++count;
                countries.Add(countryInfo, countries);                        
            }         
        }

        theLog.displayThis("CODE STATUS > Setup finsished - " + count + " countries processed");//updates the log
    }

    public void AllCountryDataTableBuilder(TheLog theLog, BSTree countries)//similarly to the above procedure, but builds from the rawDataAll file
    {         
        rawData.Data = rawData.GetDataAll(theLog);
       

        foreach (string countryInfo in rawData.Data)
        {
            ++count;
            countries.Add(countryInfo, countries);                    
        }
        
    }
}