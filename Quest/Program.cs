using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace Quest
{

    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//data.dat");
            var points = new List<List<Point>>();
            var singerPoint = new Point();
            while (!fileReader.EndOfStream)
            {
                var coords = fileReader.ReadLine().Split(' ');
                switch (coords.Length)
                {
                    case 6:
                        points.Add(new List<Point>()
                        {
                            new Point(Int32.Parse(coords[0]), Int32.Parse(coords[1])),
                            new Point(Int32.Parse(coords[2]), Int32.Parse(coords[3])),
                            new Point(Int32.Parse(coords[4]), Int32.Parse(coords[5])),
                        });
                        break;
                    case 2:
                        singerPoint.X = Int32.Parse(coords[0]);
                        singerPoint.Y = Int32.Parse(coords[1]);
                        break;
                }
            }
            fileReader.Close();


            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                var position = 1;
                var result = new StringBuilder();
                points.ForEach(triangle =>
                {
                    //pseudoscalar product for all sides of the triangle and singerPoint
                    var a = (triangle[0].X - singerPoint.X) * (triangle[1].Y - triangle[0].Y) - (triangle[1].X - triangle[0].X) * (triangle[0].Y - singerPoint.Y);
                    var b = (triangle[1].X - singerPoint.X) * (triangle[2].Y - triangle[1].Y) - (triangle[2].X - triangle[1].X) * (triangle[1].Y - singerPoint.Y);
                    var c = (triangle[2].X - singerPoint.X) * (triangle[0].Y - triangle[2].Y) - (triangle[0].X - triangle[2].X) * (triangle[2].Y - singerPoint.Y);
                    
                    //if singerPoint neither in triangle nor lays on it's side
                    if (((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0)))
                    {                        
                        result.Append(String.Format("{0} ", position));
                    }
                    position++;
                });
                if (result.Length == 0)
                {
                    //if singer didn't fail
                    result.Append("0");
                }
                //trim spaces and write result
                writer.Write(result.ToString().Trim());
            }
        }
    }
}
