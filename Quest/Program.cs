using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{



    class Program
    {
        public static string ToNBase(int number, int _base, int digitsInTicket)
        {
            var result = "";
            while (true)
            {
                result = (number % _base) + result;
                number /= _base;
                if (number >= _base) continue;
                if (number != 0)
                {
                    result = number + result;
                }
                //adding left zeros
                result = result.PadLeft(digitsInTicket, '0');
                break;
            }
            return result;
        }

        public static int SumString(string data)
        {
            return data.Sum(t => Int32.Parse(t.ToString()));
        }

        private static bool IsCommonHappy(ref string data)
        {
            for (var i = 1; i < data.Length; i++)
            {
                var left = SumString(data.Substring(0, i));
                var right = SumString(data.Substring(i, data.Length - i));
                if (left == right)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            var count = 0;
            var iterator = 0;
            const int digitsInTicket = 8;
            const int _base = 4;
            //find iteration quantity
            var end = (int)Math.Pow(_base, digitsInTicket);
            while (iterator != end)
            {
                var data = ToNBase(iterator, _base, digitsInTicket);
                if (IsCommonHappy(ref data))
                    count++;

                iterator++;
            }
            Console.WriteLine(count);
        }


    }
}
