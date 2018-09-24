/*This is the mapData class controlling access to the map data files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;

public class MapData
{
    private string mapData;//data from the map files
    private StreamReader files;//a file reader

    //**********************************************************************************************************************************

    public MapData(string fileNameSuffix)//constructor
    {
        files = new StreamReader(@"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "MapData.txt"); //opens transdata file
        //theLog.displayThis("FILE STATUS > " + fileNameSuffix + "MapData FILE opened\n"); //updates the file status inthe log file
    }

    //**********************************************************************************************************************************

     public string Data//public accessor for the private field data
     {
         get
         {
             return mapData;
         }

         set
         {
             mapData = value;
         }
     }

     //**********************************************************************************************************************************

     public string GetTransData(string fileNameSuffix)//gets data from the map file and returns it 1 line at a time
     {
         if (!files.EndOfStream)
         {
             mapData = files.ReadLine();// reads a line in the file
             return mapData;
         }

         else
         {
             FinishUp(fileNameSuffix);// closes  file
             return "x";//returns "x" to indicate the end of the file has been reached
         }

     }

     //********************************************************************************************************************************************************
     
    public void FinishUp(string fileNameSuffix)//closes mapData file
     {
         //theLog.displayThis("FILE STATUS > " + fileNameSuffix + "MapData FILE closed");
         files.Close();
     }
}