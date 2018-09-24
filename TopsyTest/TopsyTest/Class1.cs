using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Class1
    {
        public static void Main()
        {
            const string fileName = "data.csv";
            //FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //FileInfo fileInfo = new FileInfo(fileName);
            //int fileSize = Convert.ToInt32(fileInfo.Length);
            //StreamReader reader = new StreamReader(inFile);

            //string header = Convert.ToString(reader.ReadLine());
            //string line;
            string[] data = File.ReadAllLines(fileName);
            var ordered = data.OrderByDescending(x => double.Parse(x.Split(',')[3]));

            foreach (var line in ordered)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
