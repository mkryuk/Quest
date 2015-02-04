using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace Quest
{
    internal class Circle
    {
        public Point CenterPoint { get; set; }
        public int Radius { get; set; }

    }

    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//data.dat");
            var points = new List<Circle>();
            var singerPoint = new Point();
            //read file and load data
            while (!fileReader.EndOfStream)
            {
                var coords = fileReader.ReadLine().Split(' ');
                switch (coords.Length)
                {
                    case 3:
                        points.Add(new Circle()
                        {                            
                                CenterPoint = new Point(Int32.Parse(coords[0]), Int32.Parse(coords[1])),
                                Radius = Int32.Parse(coords[2])}
                        );
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
                var score = 0;
                var result = new StringBuilder();
                points.ForEach(circle =>
                {
                    //calculate radius from center point of the circle to singer by Pythagoras' theorem
                    var tempRadius = Math.Pow(circle.Radius, 2);
                    var radToSinger = Math.Pow(Math.Abs(circle.CenterPoint.X - singerPoint.X), 2) +
                                      Math.Pow(Math.Abs(circle.CenterPoint.Y - singerPoint.Y), 2);
                    //if singer in the circle or on it's border 
                    //add position to result and calculate the score
                    if (radToSinger <= tempRadius)
                    {
                        score += 499;
                        result.Append(String.Format("{0} ", position));
                    }
                    position++;
                });
                if (result.Length == 0)
                {
                    //if singer didn't fail
                    result.Append("0");
                }
                else
                {
                    result.Append(score);
                }
                //trim spaces and write result
                writer.Write(result.ToString());
            }
        }
    }
}
