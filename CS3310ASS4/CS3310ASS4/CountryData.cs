/*This is the country Data Procedural class used to process the country data files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Text;

public class CountryDataTable
{
    private int sizeOfDataRecord, byteOffset;
    private FileStream file;
    private byte[] buffer;
    private StringBuilder stringBuilder = new StringBuilder();

    //**********************************************************************************************************************************

    public CountryDataTable(TheLog theLog, int countryDataFileNumber)//opens the specified country data file
    {
        file = new FileStream(@"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS4\CS3310ASS4\bin\Debug\CountryData" + countryDataFileNumber + ".txt", FileMode.Open, FileAccess.Read);//creates the bin file
        sizeOfDataRecord =  25;
        buffer = new byte[sizeOfDataRecord];
    }

    //**********************************************************************************************************************************
    //gets and returns a record given a DRP from the current country data  text file
    public string GetRecord(short id)
    {
        byteOffset = sizeOfDataRecord * (id - 1) + 2;
        file.Seek(byteOffset, SeekOrigin.Begin);
        file.Read(buffer, 0, sizeOfDataRecord);
        stringBuilder.Clear();

        foreach (byte bytes in buffer)
        {
            stringBuilder.Append((char) bytes);
        }

        return stringBuilder.ToString();
    }

    //**********************************************************************************************************************************
    //Closes the current data file
    public void FinishUp()
    {
        file.Close();
    }
}
