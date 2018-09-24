/*This is the shortest path class used to find the shortest path between 2 points in the map
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;
using System.IO;
using System.Text;

public class Shortestpath
{
    private short distance, shortestRoute = 0, smallestDistance;
    private int sizeOfARecord, sizeOfHeaderRecord, byteOffset, i, z, a = 0, r = 0; //i z a r...used in loops
    private string roadsFilePath;
    private StringBuilder stringBuilder = new StringBuilder();
    private string[] previousCity;
    private short[] distanceArray;
    private bool[] included;
    private byte[] buffer;
    private BinaryReader roadsFileReader;
    private UTF8Encoding utf8 = new UTF8Encoding();

    private bool Included, pathNotFound;
    private short nodeNumber, x, y;
    private short[] done, previous;// done is the included array and previous keeps track of paths, to get the shortes path
    private short[,] distanceTo;// keeps track of the distances SO FAR
    private string result, path, traceOfTargets, pattern;

    //**********************************************************************************************************************************

    public string FindPath(short startCityNumber, short destinationCityNumber, string fileNameSuffix, Map map)//Dijkstra's shortest path algorithm
    {
        nodeNumber = startCityNumber;
        a = z = smallestDistance = 0;
        pathNotFound = false;
        pattern = "";
        traceOfTargets = "";

        Initialize(map.n, fileNameSuffix);//initialize

        for (i = 0; i < sizeOfARecord / sizeof(short); ++i)//steps
        {
            if (nodeNumber != destinationCityNumber && smallestDistance != short.MaxValue)
            {
                byteOffset = sizeOfHeaderRecord + (sizeOfARecord * nodeNumber);
                roadsFileReader.BaseStream.Seek(byteOffset, SeekOrigin.Begin);

                for (x = 0; x < sizeOfARecord / sizeof(short); ++x)
                {
                    distanceArray[x] = BitConverter.ToInt16(roadsFileReader.ReadBytes(2), 0);
                }

                if (i == 0)//step 0
                {
                    done[i] = startCityNumber;
                    previous[nodeNumber] = -1;

                    for (x = 0; x < distanceArray.Length; ++x)
                    {
                        distanceTo[i, x] = distanceArray[x];

                        if (distanceTo[i, x] != short.MaxValue && distanceTo[i, x] != 0)
                        {
                            previous[x] = startCityNumber;
                        }
                    }
                }

                else
                {
                    done[i] = nodeNumber;

                    if (a == 0)
                    {
                        previous[nodeNumber] = startCityNumber;
                        ++a;
                    }

                    for (x = 0; x < distanceArray.Length; ++x)
                    {
                        if (x == previous[nodeNumber] && distanceTo[i-1, x] == 0)
                        {
                            distanceTo[i, x] = distanceArray[x];
                        }

                        else if ((distanceArray[x] + distanceTo[(i-1), nodeNumber]) < distanceTo[(i - 1), x])// the big question
                        {
                            distanceTo[i, x] = Convert.ToInt16((distanceArray[x] + distanceTo[(i - 1), nodeNumber]));
                            previous[x] = nodeNumber;
                        }

                        else
                        {
                            distanceTo[i, x] = distanceTo[(i - 1), x];
                        }
                    }
                }

                for (x = 0; x < distanceArray.Length; ++x)//finding the next node to include
                {
                        //check if it's not included
                        for (y = 0; y < done.Length; ++y)
                        {
                            if (done[y] == x)
                            {
                                Included = true;
                            }
                        }

                        if (r == 0 && !Included)
                        {
                            smallestDistance = distanceTo[i, x];
                            shortestRoute = x;
                            ++r;
                        }

                        if (distanceTo[i, x] < smallestDistance && !Included)
                        {
                            smallestDistance = distanceTo[i, x];
                            shortestRoute = x;
                        }

                        Included = false;
                   
                }

                r = 0;
                nodeNumber = shortestRoute;
            }

            else
            {
                done[i] = nodeNumber; 

                if (smallestDistance == short.MaxValue)//unreacheable node
                {
                    pathNotFound = true;
                    for (x = 1; x < done.Length; ++x)
                    {
                        traceOfTargets += " " + map.WhatsCityName(done[x]);
                    }

                    result = "" + "'" + "" + "'" + traceOfTargets;
                }

                else if (done[0] == destinationCityNumber)//start = end
                {
                    //sourceEqualsDestination = true;
                    distance = 0;
                    pattern = map.WhatsCityName(startCityNumber);
                    traceOfTargets = pattern;
                }

                else//path found
                {
                    distance = distanceTo[i-1, destinationCityNumber];

                    for (x = 1; x < done.Length - 1; ++x)//get trace of targets
                    {
                        traceOfTargets += " " + map.WhatsCityName(done[x]);
                    }

                    path = map.WhatsCityName(nodeNumber);//get the path

                    while (previous[nodeNumber] != startCityNumber)
                    {
                        path += " > " + map.WhatsCityName(previous[nodeNumber]);
                        nodeNumber = previous[nodeNumber];
                    }

                    path += " > " + map.WhatsCityName(previous[nodeNumber]);

                    for (x = Convert.ToInt16(path.Split(' ').Length); x > 0; --x)// reverses the path
                    {
                        pattern += path.Split(' ')[x-1];
                        pattern += " ";
                    }
                }

                if (!pathNotFound)
                {
                    result = distance + "'" + pattern + "'" + traceOfTargets;
                }

                i = sizeOfARecord;
            }
        }

        roadsFileReader.Close();

        return result;
    }

    //**********************************************************************************************************************************

    private void Initialize(short N, string fileNameSuffix)
    {
        done = new short[N];
        previous = new short[N+1];

        for (x = 0; x < done.Length; ++x)
        {
            done[x] = -1;
        }

        distanceTo = new short[N, N];
        previousCity = new string[N];
        distanceArray = new short[N];
        included = new bool[N];
        roadsFilePath = @"C:\Users\Martin\Documents\Visual Studio 2010\Projects\CS3310ASS5\CS3310ASS5\bin\Debug\" + fileNameSuffix + "Roads.bin";

        if (z == 0)
        {
            roadsFileReader = new BinaryReader(File.Open(roadsFilePath, FileMode.Open), utf8);
            ++z;
        }

        sizeOfARecord = N * sizeof(short);
        sizeOfHeaderRecord = sizeof(short);
        buffer = new byte[sizeOfARecord];
    }

    //**********************************************************************************************************************************

    public void FinishUp()
    {

    }
}