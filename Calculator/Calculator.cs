using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculator
    {
        public static char[] Signs = new char[] { '+', '-', '/', '*' };
        public static Dictionary<char, int> Priopity = new Dictionary<char, int>()
        {
        {'+',2 },
        {'-',2 },
        {'/',1 },
        {'*',1 },
        };
    }
}
