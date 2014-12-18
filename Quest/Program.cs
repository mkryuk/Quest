using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Quest
{
    class Program
    {
        public static BigInteger Fact(BigInteger val)
        {
            if (val == 0)
            {
                return 1;
            }
            return val * Fact(--val);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Fact(104) * 10 / 3600);
        }
    }
}
