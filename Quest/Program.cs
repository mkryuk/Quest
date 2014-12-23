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
            return data.Sum(t => Convert.ToInt32(t));
        }

        private static bool IsHappy(ref string data)
        {
            //there is no happy numbers in number with odd digits count
            if ((data.Length % 2) != 0)           
                return false;
            
            var left = SumString(data.Substring(0, data.Length/2));
            var right = SumString(data.Substring(data.Length / 2, data.Length / 2));
            return left == right;
        }

        static void Main(string[] args)
        {
            var count = 0;
            var iterator = 0;
            const int digitsInTicket = 6;
            const int _base = 9;
            //find iteration quantity
            var end = (int)Math.Pow(_base, digitsInTicket);
            while (iterator != end)
            {
                var data = ToNBase(iterator, _base, digitsInTicket);
                if (IsHappy(ref data))                
                    count++;
                
                iterator++;
            }
            Console.WriteLine(count);
        }

        
    }
}
