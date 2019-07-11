using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmasChecker
{
    public class Main
    {
        private TestDelegate t = new TestDelegate();

        public bool Method()
        {
            return t.test1(t.TestString);
        }

        public int run()
        {
            return t.Run(t.Sub, 5, 3);
            //t.Run(t.Sum, 6, 7);
        }
    }

    internal class TestDelegate
    {
        public Predicate<string> p;

        public delegate int Operation(int a, int b);

        public int Run(Operation args, int a, int b)
        {
            return args(a, b);
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }

        public bool test1(Predicate<string> p)
        {
            if (p("yes"))
            {
                return true;
            }

            return false;
        }

        public bool TestString(string a)
        {
            if (a == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}