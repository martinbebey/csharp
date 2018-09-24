using System;
using System.IO;
using System.Linq;

public class TopsyTest
{
    public static void Main()
    {
        const string fileName = "data.csv";
        string[] data = File.ReadAllLines(fileName);
        string title = data[0];       

        var ordered = data.Skip(1).OrderBy(x => double.Parse(x.Split(',')[3]));
        Console.WriteLine(title);

        foreach (var line in ordered)
        {
            Console.WriteLine(line);
        }
        
        Console.ReadLine();
    }
}