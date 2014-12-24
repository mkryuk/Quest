using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{
    class Program
    {

        static void Main(string[] args)
        {
            var fileStream = new StreamReader("..\\..\\notes.txt");
            var judges = new Dictionary<string, int>();
            var fineList = new List<int>();
            var pointSum = 0;
            while (!fileStream.EndOfStream)
            {
                var readLine = fileStream.ReadLine();
                if (readLine == null) continue;
                var line = readLine.Split(' ');
                var fine = Int32.Parse(line[0]);
                var points = Int32.Parse(line[1]);
                if (fine >= 0)
                {
                    var temp = fine;
                    fine = points;
                    points = temp;
                    if (judges.ContainsKey(line[2]))
                    {
                        judges[line[2]]++;
                    }
                    else
                    {
                        judges.Add(line[2], 1);
                    }
                }
                fineList.Add(fine);
                pointSum += points;
            }
            Console.WriteLine("average of fine points:{0} points sum:{1} most failed judge:{2}",
                (int)fineList.Average() * -1,
                pointSum,
                judges.Count == 0
                    ? "nobody failed"
                    : judges.Aggregate((left, right) => left.Value > right.Value ? left : right).Key);
        }
    }
}
