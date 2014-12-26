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

        //calculate bricks quantity for stairs
        static int Sum(int value)
        {
            return value == 0 ? 0 : value + Sum(--value);
        }

        static void Main(string[] args)
        {
            const int stairsHeght = 85;
            //show the result
            Console.WriteLine("You need {0} bricks",Sum(stairsHeght));
        }
    }
}
