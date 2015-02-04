using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace Quest
{
    

    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//names.dat");
            var names = new Dictionary<string,int>();
            //read file and load data
            while (!fileReader.EndOfStream)
            {
                var name = fileReader.ReadLine();
                if (names.ContainsKey(name))
                    names[name]++;
                
                else                
                    names.Add(name,1);
            }
            fileReader.Close();
            //select all bad singers in alphabet order
            var result = from name in names where name.Value == names.Max(item=>item.Value) orderby name.Key select name.Key;

            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                var strResult = new StringBuilder();
                result.ToList().ForEach(item => strResult.Append(String.Format("{0}, ", item)));
                writer.WriteLine(strResult.ToString().Trim(new []{' ',','}));
            }
        }
    }
}
