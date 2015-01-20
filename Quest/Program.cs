using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{
    struct Branch
    {
        public List<int> path;
        public int sum;
    }
    class Program
    {

        public static void Permutations(int[] positions, int left, Dictionary<int, List<int>> engList, ref Branch result)
        {
            if (left == positions.Length - 1)
            {
                var data = (int[])positions.Clone();
                //sum task time in current position t = positions[i]
                var sum = positions.Select((t, i) => engList[t][i]).Sum();
                //if result wasn't set
                if (result.sum == -1)
                {
                    result.sum = sum;
                    result.path.Clear();
                    result.path.AddRange(data);
                }
                if (result.sum <= sum) return;
                result.sum = sum;
                result.path.Clear();
                result.path.AddRange(data);
            }
            else
            {
                for (var i = left; i < positions.Length; i++)
                {
                    Swap(ref positions[left], ref positions[i]);
                    Permutations(positions, left + 1, engList, ref result);
                    Swap(ref positions[left], ref positions[i]);
                }
            }
        }        

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//engineers.dat");
            //Dictionary of engineers key = index val = collection of time for task
            var engList = new Dictionary<int, List<int>>();
            var index = 0;
            while (!fileReader.EndOfStream)
            {
                var readLine = fileReader.ReadLine();
                var line = new List<int>();
                if (readLine == null) continue;
                readLine.Trim().Split(' ')
                    .ToList()
                    .ForEach((item) => line.Add(Int32.Parse(item)));
                engList.Add(index++, line);
            }           

            //initial position of engineers
            var positions = new[] { 0, 1, 2, 3, 4, 5, 6 };
            var result = new Branch() { sum = -1, path = new List<int>() };
            Permutations(positions, 0, engList, ref result);
            var text = new StringBuilder();
            text.Append(String.Format("минимальное количество часов {0};", result.sum));            
            for (var i = 0; i < engList.Count; i++)
            {
                var taskIndex = result.path.IndexOf(i);
                text.Append(String.Format(" Инженер № {0} назначается на блок №{1} работает {2} часов;", i + 1, taskIndex + 1, engList[i][taskIndex]));                    
            }
            Console.Write(text);
            var writer = new StreamWriter("..//..//result.txt");
            writer.Write(text);
            writer.Close();
        }
    }
}
