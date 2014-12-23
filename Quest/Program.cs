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
            var fileStream = new StreamReader("..\\..\\musetalks.data");
            var langList = new List<List<string>>();
            while (!fileStream.EndOfStream)
            {
                var readLine = fileStream.ReadLine();
                if (readLine == null) continue;

                var line = readLine.Split(' ');

                //looking up in all list of languages
                var found = langList.FindAll((item) =>
                {       
                    var result = false;

                    //looking up read muse in existing list
                    line.ToList().ForEach((muse) =>
                    {
                        var index = item.FindIndex((oneOfMuses) => oneOfMuses == muse);
                        if (index == -1) return;
                        //add both muses to their language partners
                        item.AddRange(line);
                        result = true;
                    });
                    return result;
                });

                //add muses to the new language list
                if (found.Count == 0)
                {
                    langList.Add(line.ToList());
                }
                //in the case both muses exists in different lines of language list 
                //combine this lines together and remove remaining
                else if (found.Count > 1)
                {
                    found[0].AddRange(found[1]);
                    langList.Remove(found[1]);
                }
            }
            //find out the longest language chain
            //distinct is for eliminate muse repetition
            var longestLanguageChain = langList.Max((list)=>list.Distinct().Count());

            Console.WriteLine("maximum muses chain speaks one language {0}",longestLanguageChain);
        }
    }
}
