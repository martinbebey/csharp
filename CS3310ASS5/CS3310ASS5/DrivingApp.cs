/*This is the driving app class used to process requests from the city pair files
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;

public class DrivingApp
{
    private UI ui;
    private Map map;
    private Shortestpath shortestPath = new Shortestpath();
    private string uiTrans, startCityName, destinationCityName, result;
    private short startCityNumber, destinationCityNumber;
    private int numberOfTargets = 0;

    //**********************************************************************************************************************************

    public void ProcessUITrans(string fileNameSuffix, SetupUtility setup)// processes city pair requests
    {
        ui = new UI(fileNameSuffix, setup);
        map = new Map(fileNameSuffix);
        uiTrans = " ";

        while (uiTrans != "x")// while not end of city pair file
        {
            uiTrans = ui.GetCityPairs(fileNameSuffix);

            if (uiTrans != "x" && uiTrans != "" && uiTrans.Substring(0, 1) != "%")
            {
                startCityName = uiTrans.Split(' ')[0];
                destinationCityName = uiTrans.Split(' ')[1];
                ui.WriteThis("#   #   #   #   #   #   #   #   #   #   #   #   #   #   #   #");
                ui.WriteThis(startCityName + " (" + map.WhatsCityNumber(startCityName) + ") " + "TO " + destinationCityName + " (" + map.WhatsCityNumber(destinationCityName) + ")");

                if (map.WhatsCityNumber(startCityName) == -1 || map.WhatsCityNumber(destinationCityName) == -1)//if city doesn't exits
                {
                    ui.WriteThis("ERROR - one of the cities is not on this map\n");
                }

                else  
                {
                    map.GetCityNumbers(ref startCityNumber, ref destinationCityNumber, startCityName, destinationCityName);
                    result = shortestPath.FindPath(startCityNumber, destinationCityNumber, fileNameSuffix, map);//get shortest path

                    if (result.Split('\'')[0] == "") // if destination is unreacheable
                    {
                        ui.WriteThis("DISTANCE:  ?");
                        ui.WriteThis("PATH:  SORRY - can't reach destination city from the start city\n");
                        ui.WriteThis("TRACE OF TARGETS: " + result.Split('\'')[1]);
                    }

                    else
                    {
                        ui.WriteThis("DISTANCE:  " + result.Split('\'')[0]);
                        ui.WriteThis("PATH:  " + result.Split('\'')[1] + "\n");
                        ui.WriteThis("TRACE OF TARGETS: " + result.Split('\'')[2]);
                    }

                    if (result.Split('\'')[2] == "")
                    {
                        numberOfTargets = 0;
                    }

                    else
                    {
                        if (result.Split('\'')[0] == "0")
                        {
                            numberOfTargets = 0;
                        }

                        else
                        {
                            numberOfTargets = result.Split('\'')[2].Trim().Split(' ').Length;
                        }
                    }

                    ui.WriteThis("# Targets:  " + numberOfTargets  + "\n");
                }
            }
        }

        //finish up for 3 objects
        ui.FinishUp(fileNameSuffix);
        shortestPath.FinishUp();
        map.FinishUp();
    }
}