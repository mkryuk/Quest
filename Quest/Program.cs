using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;


namespace Quest
{

    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//map.dat");
            var matrix = new List<List<int>>();
            while (!fileReader.EndOfStream)
            {
                matrix.Add(fileReader.ReadLine().ToList().ConvertAll(item => Int32.Parse(item.ToString())));
            }
            fileReader.Close();

            var square = 0;
            var n = matrix.Count;
            var m = matrix[0].Count;
            var d = new int[m];
            for (var i = 0; i < m; i++) d[i] = -1;
            var d1 = new int[m];
            var d2 = new int[m];
            var st = new Stack<int>();

            int top = 0, left = 0, right = 0, bottom = 0;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                    if (matrix[i][j] == 1)
                        d[j] = i;

                while (st.Count != 0) st.Pop();
                for (var j = 0; j < m; ++j)
                {
                    while (st.Count != 0 && d[st.Peek()] <= d[j]) st.Pop();
                    d1[j] = st.Count == 0 ? -1 : st.Peek();
                    st.Push(j);
                }
                while (st.Count != 0) st.Pop();                
                for (var j = m - 1; j >= 0; --j)
                {
                    while (st.Count != 0 && d[st.Peek()] <= d[j]) st.Pop();
                    d2[j] = st.Count == 0 ? m : st.Peek();
                    st.Push(j);
                }
                for (var j = 0; j < m; ++j)
                {
                    var tempSquare = (i - d[j]) * (d2[j] - d1[j] - 1);
                    if (tempSquare <= square) continue;
                    square = tempSquare;
                    top = d[j] + 1;
                    bottom = i;
                    left = d1[j] + 1;
                    right = d2[j] - 1;
                }

            }

            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                //correct the result, stage should not touch any building
                square = ((bottom - 1) - (top)) * ((right - 1) - (left));
                writer.WriteLine(square);
            }
        }
    }
}
