/*this is the country index class used to implement the country index table as a B tree
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Text;

public class CodeIndex
{
    private short M = 0, N = 0, rootPtr = 0, nodePtr;//nodePtr used to point to individual nodes using tree pointers
    private short[] TPs, DRPs;
    private string[] KVs;
    private byte[] aShort = new byte[2];//used to read shorts in the indexfile and flip them for conversions between little and big endian types
    private int i, sizeOfDataRec, sizeOfHeaderRec, byteOffset;
    private bool lessThan, found = false;
    private BinaryReader indexFileReader;
    private string filepath;//path to the index file
    private UTF8Encoding utf8 = new UTF8Encoding();

    //******************************************************************************************************************************

    public CodeIndex(int indexFileNumber)//constructor reading the header record
    {
        filepath = @"C:\Users\Administrateur\Documents\Visual Studio 2010\Projects\CS3310ASS4\CS3310ASS4\bin\Debug\CodeIndex" + indexFileNumber + ".bin";
        indexFileReader = new BinaryReader(File.Open(filepath, FileMode.Open), utf8);
        sizeOfHeaderRec = sizeof(short) * 3;
        indexFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
        //reading in 3 shorts at top of file for M N AND RootPtr
        M = ReadInt16();
        nodePtr = rootPtr = ReadInt16();
        N = ReadInt16();
        //3 parallel arrays
        TPs = new short[M];
        KVs = new string[M - 1];
        DRPs = new short[M - 1];
        sizeOfDataRec = sizeof(short) * M + (M - 1) * 3 + sizeof(short) * (M - 1);
    }

    //******************************************************************************************************************************

    //this method reads in a short from the index file and filps the bytes to convert between little and big endian
    private Int16 ReadInt16()
    {
        aShort = indexFileReader.ReadBytes(2);
        Array.Reverse(aShort);
        return BitConverter.ToInt16(aShort, 0);
    }

    //******************************************************************************************************************************
    //just does what its name says
    private void SearchANode()
    {
        byteOffset = sizeOfDataRec * (nodePtr - 1) + sizeOfHeaderRec;
        indexFileReader.BaseStream.Seek(byteOffset, SeekOrigin.Begin);//seeks position determined by tree pointer
    }

    //******************************************************************************************************************************

    //just doing what its name says
    private void ReadANode()
    {
        for (i = 0; i < M; ++i)
        {
            TPs[i] = ReadInt16();//reads in 5 TPs
        }

        for (i = 0; i < M - 1; ++i)
        {
           //reads in 4 KVs
           KVs[i] = Convert.ToString(indexFileReader.ReadChar()) + Convert.ToString(indexFileReader.ReadChar()) + Convert.ToString(indexFileReader.ReadChar());
        }

        for (i = 0; i < M - 1; ++i)
        {
            DRPs[i] = ReadInt16();//reads in 4 DRPs
        }
    }

    //**********************************************************************************************************************************

    //calls SearchAnode and ReadANode and does all the KV comparisons
    public void SelectByCode(string KV, ref int numberOfNodesRead, ref short DRP)
    {
        if (!found)
        {
            DRP = 0;
        }

        if (nodePtr != -1 && nodePtr <= N && !found)
        {
            SearchANode();//searching a node
            ReadANode();//reading a nonde

            i = 0;
            lessThan = false;
            ++numberOfNodesRead;
            found = KV.Equals(KVs[i], StringComparison.OrdinalIgnoreCase);//comparing using OrdinalIgnoreCase

            while (!found && i < M - 1 && !lessThan)
            {

                lessThan = String.Compare(KV, KVs[i], StringComparison.OrdinalIgnoreCase) < 0;

                if (lessThan)//checks if code is less than KV being read
                {
                    nodePtr = TPs[i];
                }

                else
                {
                    ++i;

                    if (i < M - 1)
                    {
                        found = KV.Equals(KVs[i], StringComparison.OrdinalIgnoreCase);
                    }
                }
            }

            if (found)
            {
                DRP = DRPs[i];//gets the DRP if found
            }

            else if (!lessThan)
            {
                nodePtr = TPs[M - 1];
            }

            //recursive calls to find the desired record
            SelectByCode(KV, ref numberOfNodesRead, ref DRP);
        }

        //setting back found and nodePtr to default values for use for the next search process
        found = false;
        nodePtr = rootPtr;
    }

    //**********************************************************************************************************************************
    //closes the current index file
    public void FinishUp()
    {
        indexFileReader.Close();
    }

}     
