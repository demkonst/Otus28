using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Otus28
{
    internal class Program
    {
        private const int Count1 = 100_000;
        private const int Count2 = 1_000_000;
        private const int Count3 = 10_000_000;

        private static void Main(string[] args)
        {
            Evaluate(Count1);
            Evaluate(Count2);
            Evaluate(Count3);
        }

        private static void Evaluate(int count)
        {
            var rnd = new Random();
            var data = new List<int>(count);
            for (var i = 0; i < count; i++)
            {
                data.Add(rnd.Next(99));
            }

            var sw = new Stopwatch();

            var e1 = new SequentalEvaluator();
            sw.Start();
            var r1 = e1.Evaluate(data);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(r1);
            Console.WriteLine();

            sw.Reset();
            var e2 = new ThreadEvaluator(4);
            sw.Start();
            var r2 = e2.Evaluate(data);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(r2);
            Console.WriteLine();

            sw.Reset();
            var e3 = new ParallelEvaluator();
            sw.Start();
            var r3 = e3.Evaluate(data);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(r3);
            Console.WriteLine();
        }
    }
}