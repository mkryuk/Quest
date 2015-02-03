using System;
using System.Drawing;
using System.IO;
using System.Text;


namespace Quest
{

    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//coords.dat");
            var rc = fileReader.ReadLine().Split(' ');
            var riverA = new Point(Int32.Parse(rc[0]), Int32.Parse(rc[1]));
            var riverB = new Point(Int32.Parse(rc[2]), Int32.Parse(rc[3]));
            var result = new StringBuilder();
            while (!fileReader.EndOfStream)
            {
                var fc = fileReader.ReadLine().Split(' ');
                var fanPoint = new Point(Int32.Parse(fc[0]), Int32.Parse(fc[1]));

                //calculate pseudoscalar product of vectors
                var curFan = (riverB.X - riverA.X) * (fanPoint.Y - riverA.Y) -
                             (riverB.Y - riverA.Y) * (fanPoint.X - riverA.X);

                //point from the left
                if (curFan > 0)
                    result.Append("П");

                //point from the right
                else if (curFan < 0)
                    result.Append("И");

                //point on the line
                else
                    Console.WriteLine("ERROR");

            }
            fileReader.Close();

            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                writer.Write(result);
            }                     
        }
    }
}
