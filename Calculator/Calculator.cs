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

        public double Calculate(List<string> result)
        {
            double num1, num2;
            if (result.Count == 1)
            {
                if (double.TryParse(result[0].ToString(), out num1))
                {
                    return num1;
                }
                else
                {
                    if (result[0][0] == '-' && result[0][1] == '(')
                    {
                        return -Calculate(SplitByOperation(result[0].ToString().Substring(1, result[0].ToString().Length - 1)));
                    }

                    if (result[0][0] == '(')
                    {
                        return Calculate(SplitByOperation(result[0].ToString().Substring(1, result[0].ToString().Length - 2)));
                    }
                }
            }
            if (result.Count == 3)
            {
                if (!(double.TryParse(result[0].ToString(), out num1)))
                {
                    num1 = Calculate(SplitByOperation(result[0].ToString()));
                }
                if (!(double.TryParse(result[2].ToString(), out num2)))
                {
                    num2 = Calculate(SplitByOperation(result[2].ToString()));
                }
                return DoCalc(num1, num2, result[1][0]);
            }
            int indMaxPrior = 1, MaxPrior = 0;
            for (int i = 1; i < result.Count; i += 2)
            {
                if (Priopity[result[i][0]] >= MaxPrior)
                {
                    indMaxPrior = i;
                    MaxPrior = Priopity[result[i][0]];
                }
            }
            num1 = Calculate(result.GetRange(0, indMaxPrior));
            num2 = Calculate(result.GetRange(indMaxPrior + 1, result.Count() - indMaxPrior - 1));
            return DoCalc(num1, num2, result[indMaxPrior][0]);
        }

        static double DoCalc(double num1, double num2, char action)
        {

            switch (action)
            {
                case '+': return num1 + num2;
                case '-': return num1 - num2;
                case '*': return num1 * num2;
                case '/':
                    if (num2 != 0)
                    {
                        return num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("Error! Trying to divide by zero");
                        return 0;
                    }
                default:
                    Console.WriteLine("Error! Onerator not defined: " + action);
                    return 0;
            }
        }

        public List<string> SplitByOperation(string str)
        {
            if (str.Length == 0)
            {
                Console.WriteLine("Error! Empty line");
            }
            if (Signs.Contains(str[0]) && !(str[0] == '-'))
            {
                Console.WriteLine("Error! Missing number before operator: " + str);
            }
            if (Signs.Contains(str[str.Length - 1]))
            {
                Console.WriteLine("Error! Missing number after operator: " + str);
            }

            List<string> result = new List<string> { };
            int j = 0, count = str[0] == '(' ? 1 : 0;

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '(') count++;

                if (count == 0 && Signs.Contains(str[i]) && !(i == j && str[i] == '-' && str[i - 1] != ')'))
                {
                    if (i == j)
                    {
                        Console.WriteLine("Operator error:" + str[i - 1] + str[i]);
                    }
                    result.Add(str.Substring(j, i - j));
                    result.Add(str[i].ToString());
                    j = i + 1;
                }

                if (str[i] == ')') count--;
            }
            result.Add(str.Substring(j, str.Length - j).ToString());
            if (count != 0)
            {
                Console.WriteLine("Error! The number of open brackets is not equal to the number of closed brackets");
            }
            return result;
        }
    }
}

