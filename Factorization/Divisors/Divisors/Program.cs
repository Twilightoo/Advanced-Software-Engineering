using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using FactorLibrary;

namespace Divisors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Divisors");
            string s = Console.ReadLine();
            DateTime t1 = System.DateTime.Now;
            BigInteger num = BigInteger.Parse(s);
            Factorization n = new Factorization();
            n.findfac(num);
            DateTime t2 = System.DateTime.Now;
            TimeSpan t = t2.Subtract(t1);
            Console.Write(t.TotalMilliseconds);
            Console.Write("ms");
            Console.WriteLine("");
        }
    }
}
