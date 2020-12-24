using System;
using System.Collections.Generic;
using Stos;

namespace StosConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StosWTablicy<string> s = new StosWTablicy<string>(2);
            s.Push("km");
            s.Push("aa");
            s.Push("xx");

            Console.WriteLine("Array enumerator");

            int count = 0;

            foreach (var x in s.ToArray())
            {
                count++;
                Console.WriteLine(x);
            }

            Console.WriteLine("Iterate count: " + count);
            count = 0;

            Console.WriteLine("StosEnumerator");

            foreach (var xx in s)
            {
                count++;
                Console.WriteLine(xx);
            }

            Console.WriteLine("Iterate count: " + count);
            count = 0;

            Console.WriteLine("YieldEnumerator");

            foreach (var xxx in s.GetEnumeratorYield)
            {
                count++;
                Console.WriteLine(xxx);
            }

            Console.WriteLine("Iterate count: " + count);
            count = 0;

            Console.WriteLine("YieldEnumerator reverse (from new to old)");

            foreach (var xxx in s.GetEnumeratorReverseYield)
            {
                count++;
                Console.WriteLine(xxx);
            }

            Console.WriteLine("Iterate count: " + count);

            Console.WriteLine("ToArray test vs ToArrayReadOnly");

            foreach (var ax in s.ToArray())
            {
                string sample = ax + "D";
                Console.WriteLine(sample);
            }

            Console.WriteLine("ToArrayReadOnly test vs ToArray");

            foreach (var axx in s.ToArrayReadOnly())
            {
                string samplex = axx + "S";
                Console.WriteLine(samplex);
            }

            Console.WriteLine();
        }
    }
}
