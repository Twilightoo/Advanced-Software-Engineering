using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FactorLibrary
{
    public class Factorization
    {
        public const int times = 50;
        public int facnum = 0;
        public BigInteger[] factor = new BigInteger[100];
        public bool Fermat(BigInteger a, BigInteger num, BigInteger temp, BigInteger t)
        {
            BigInteger x1 = BigInteger.ModPow(a, temp, num);
            BigInteger x2 = x1;
            for (BigInteger i = 1; i <= t; i++)
            {
                x1 = (x1 * x1) % num;
                if (x1 == 1 && x2 != 1 && x2 != num - 1)
                {
                    return (true);
                }
                x2 = x1;
            }
            if (x1 != 1)
            {
                return (true);
            }
            return (false);
        }
        public bool IsPrime(BigInteger num)
        {
            if (num == 2)
            {
                return (true);
            }
            else if (num < 2 || num.IsEven)
            {
                return (false);
            }
            else
            {
                BigInteger temp = num - 1;
                BigInteger t = 0;
                while (temp.IsEven)
                {
                    temp /= 2;
                    t++;
                }
                for (int i = 0; i < times; i++)
                {
                    Random rd = new Random(i);
                    BigInteger a = rd.Next() % (num - 1) + 1;
                    if (Fermat(a, num, temp, t))
                    {
                        return (false);
                    }
                }
                return (true);
            }
        }
        public void findprimefac(BigInteger num)
        {
            if (IsPrime(num))
            {
                factor[facnum++] = num;
                return;
            }
            else
            {
                BigInteger p = num;
                while (p >= num)
                {
                    BigInteger i = 1, k = 2;
                    Random rd = new Random();
                    BigInteger c = rd.Next() % (num - 1) + 1;
                    BigInteger x0 = rd.Next() % p;
                    BigInteger y = x0;
                    while (true)
                    {
                        i++;
                        x0 = ((x0 * x0) % p + c) % p;
                        BigInteger d = BigInteger.GreatestCommonDivisor(y - x0, p);
                        if (d != 1 && d != p)
                        {
                            p = d;
                            break;
                        }
                        if (y == x0)
                        {
                            break;
                        }
                        if (i == k)
                        {
                            y = x0;
                            k += k;
                        }
                    }

                }
                findprimefac(p);
                findprimefac(num / p);
            }
        }
        public int division(BigInteger[] list, int low, int high)
        {
            while (low < high)
            {
                BigInteger num = list[low];
                if (num > list[low + 1])
                {
                    list[low] = list[low + 1];
                    list[low + 1] = num;
                    low++;
                }
                else
                {
                    BigInteger temp = list[high];
                    list[high] = list[low + 1];
                    list[low + 1] = temp;
                    high--;
                }
            }
            return low;
        }
        public void Quicksort(BigInteger[] list, int low, int high)
        {
            if (low < high)
            {
                int i = division(list, low, high);
                Quicksort(list, i + 1, high);
                Quicksort(list, low, i - 1);
            }
        }
        public void findfac(BigInteger num)
        {
            findprimefac(num);
            int[] primenum = new int[100];
            BigInteger[] totalfactor;
            int m = 0, i, different_prime;
            for (i = 0; i < facnum - 1; i++)
            {
                if (factor[i] == 0)
                {
                    continue;
                }
                int k = 1;
                for (int j = i + 1; j < facnum; j++)
                {
                    if (factor[j] == factor[i])
                    {
                        k++;
                        factor[j] = 0;
                    }
                }
                primenum[m] = k;
                m++;
            }
            if (factor[i] != 0)
            {
                primenum[m] = 1;
                different_prime = m + 1;
            }
            else
            {
                different_prime = m;
            }
            if (different_prime == 1)
            {
                primenum[0]++;
                totalfactor = new BigInteger[primenum[0]];
                for (i = 0; i < primenum[0]; i++)
                {
                    totalfactor[i] = BigInteger.Pow(factor[0], i);
                }
            }
            else
            {
                primenum[different_prime - 1]++;
                for (i = different_prime - 2; i >= 0; i--)
                {
                    primenum[i] = (primenum[i] + 1) * primenum[i + 1];
                }
                totalfactor = new BigInteger[primenum[0]];
                for (i = 0; i < primenum[0]; i++)
                {
                    BigInteger k = 1;
                    m = 1;
                    int j, location = i;
                    for (j = 0; j < facnum && m < different_prime; j++)
                    {
                        if (factor[j] == 0)
                        {
                            continue;
                        }
                        k *= BigInteger.Pow(factor[j], location / primenum[m]);
                        location %= primenum[m];
                        m++;
                    }
                    while (j < facnum)
                    {
                        if (factor[j] != 0)
                        {
                            k *= BigInteger.Pow(factor[j], location);
                        }
                        j++;
                    }
                    totalfactor[i] = k;
                }
                Quicksort(totalfactor, 0, primenum[0] - 1);
            }
            for (i = 0; i < primenum[0]; i++)
            {
                string s = totalfactor[i].ToString();
                if (s.Length < 3)
                {
                    Console.WriteLine(s);
                    continue;
                }
                if (s.Length % 3 != 0)
                {
                    for (int index = 0; index < s.Length % 3; index++)
                    {
                        Console.Write(s[index]);
                    }
                    Console.Write(" ");
                }
                for (int index = s.Length % 3; index < s.Length; index++)
                {
                    Console.Write(s[index]);
                    if ((index - s.Length) % 3 == 2)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
