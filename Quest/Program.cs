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
            var fileReader = new StreamReader("..//..//schedule.dat");
            var quantity = Int32.Parse(fileReader.ReadLine());            
            var tasks = new List<dynamic>();
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine().Split(' ');                
                tasks.Add(new {StartFrom = Int32.Parse(line[0]),Duration = Int32.Parse(line[1])});

            }            
            fileReader.Close();
            var maxCount = 0;
            var sameTimeTasks = new List<dynamic>();
            //from 0 to 24 hour
            for (var i = 0; i <= 24; i++)
            {
                //select all items that recorded in current hour
                var items = tasks.Where(item => (item.StartFrom <= i) && ((item.StartFrom + item.Duration) >= i));
                //count them
                var count = items.Count();                
                if (maxCount >= count) continue;
                maxCount = count;
                sameTimeTasks = items.ToList();
            }

            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                writer.WriteLine("recorded simultaneously: {0}", maxCount);
                sameTimeTasks.ForEach(writer.WriteLine);
            }
        }
    }
}
