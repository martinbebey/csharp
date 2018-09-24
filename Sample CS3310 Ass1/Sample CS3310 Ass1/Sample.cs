using System;
using System.IO;
using System.Linq;

public class Sample
{
    public static void Main()
    {
        const string file = "sample.txt";
        string[] data = File.ReadAllLines(file);
        string name = data[0].Split('\'')[3];
        Console.Write(name);
        Console.ReadKey();
    }
}