using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackRank
{
    class Program
    {
        static string onceInATram(int x)
        {
            // Complete this function
            string f1 = x.ToString();
            int a = 1; int b = 7; int c = 5;
            int d = 4; int e = 0; int f = 0;
            int first = a + b + c;
            if (first > (d + e) )
            {
                if (first - d > 18)
                {
                    while (first - d != 19)
                    {
                        d++;
                    }
                }
                else
                {
                    e = first - (d + f);
                }
            }
            else
            {
                f = first - (d + e);
            }
            return "";
        }

        static void Main(String[] args)
        {
            int x = Convert.ToInt32(Console.ReadLine());
            string result = onceInATram(x);
            Console.WriteLine(result);
        }
    }
}
