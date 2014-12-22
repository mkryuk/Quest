using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{
    struct Translator
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    class Program
    {
        public static List<Translator> Translators = new List<Translator>();
        static void Main(string[] args)
        {
            var fileStream = new StreamReader("..\\..\\translators.data");
            const string from = "Исландский";
            const string to = "Албанский";
            while (!fileStream.EndOfStream)
            {
                var translator = fileStream.ReadLine().Split(' ');
                Translators.Add(new Translator() { Name = translator[0], From = translator[1], To = translator[2] });
            }
            var result = new List<int>();
            FindBranches(from, to, 0, ref result);
            Console.WriteLine("Minimum count of translators is {0}", result.Min());
        }

        public static void FindBranches(string from, string to, int depth, ref List<int> pathLengthes)
        {
            var translators = Translators.FindAll((item) => item.From == from);
            var currentDepth = ++depth;
            foreach (var translator in translators)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-20} Depth {3}", translator.Name, translator.From, translator.To, depth);
                if (translator.To == to)
                {
                    pathLengthes.Add(currentDepth);
                    Console.WriteLine("");
                    return;
                }
                FindBranches(translator.To, to, currentDepth, ref pathLengthes);
            }
        }
    }
}
