/*This is the Main driver program
 * 
 * by Martin Bebey WIN#: 607483766
 * 
 */

using System;

public class MainProgram
{
    public static void Main()
    {
        string fileNameSuffix = "Europe";
        SetupUtility setup = new SetupUtility();
        DrivingApp drivingApp = new DrivingApp();

        setup.BuildMap(fileNameSuffix, setup);// setup with europe
        drivingApp.ProcessUITrans(fileNameSuffix, setup);// drivingApp with europe
        fileNameSuffix = "Other";
        setup.BuildMap(fileNameSuffix, setup);// setup with other
        drivingApp.ProcessUITrans(fileNameSuffix, setup);// drivingApp with other
    }
}