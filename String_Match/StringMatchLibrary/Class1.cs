using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMatchLibrary
{
    public class StringMatch
    {
        public bool Match(string pattern, string value)
        {
            int pindex, vindex, plength = pattern.Length, vlength = value.Length;
            bool[,] f = new bool[plength + 1, vlength + 1];
            for (pindex = 0; pindex <= plength; pindex++)
            {
                for (vindex = 0; vindex <= vlength; vindex++)
                {
                    f[pindex, vindex] = false;
                }
            }
            f[0, 0] = true;
            for (pindex = 1; pindex <= plength && pattern[pindex - 1] == '*'; pindex++)
            {
                f[pindex, 0] = true;
            }
            for (pindex = 1; pindex <= plength; pindex++)
            {
                for (vindex = 1; vindex <= vlength; vindex++)
                {
                    if (f[pindex, vindex] == true)
                    {
                        continue;
                    }
                    else if (pattern[pindex - 1] == '*')
                    {
                        f[pindex, vindex] = f[pindex - 1, vindex - 1] || f[pindex - 1, vindex] || f[pindex, vindex - 1];
                    }
                    else if (pattern[pindex - 1] == '+')
                    {
                        f[pindex, vindex] = f[pindex - 1, vindex - 1] || f[pindex, vindex - 1];
                    }
                    else if (pattern[pindex - 1] == '?')
                    {
                        f[pindex, vindex] = f[pindex - 1, vindex - 1];
                    }
                    else if (pattern[pindex - 1] == '\\')
                    {
                        f[pindex, vindex] = f[pindex - 1, vindex - 1] && pattern[pindex] == value[vindex - 1];
                        f[pindex + 1, vindex] = f[pindex, vindex];
                    }
                    else
                    {
                        f[pindex, vindex] = f[pindex - 1, vindex - 1] && pattern[pindex - 1] == value[vindex - 1];
                    }
                }
            }
            return (f[plength, vlength]);
        }
    }
}
