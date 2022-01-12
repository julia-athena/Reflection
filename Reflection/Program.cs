using System;
using System.Text.Json;

namespace Reflection
{
    class F { public int i1, i2, i3, i4, i5; public static F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1000;
            CsvTest1();
            CsvTest2(n);
            JsonTest1(n);
        }

        static public void CsvTest1()
        {
            var f = F.Get();
            var f1 = new F() { i1 = 5, i2 = 4, i3 = 3, i4 = 2, i5 = 1 };
            Media<F> csv = new CsvMedia<F>();
            csv = csv.MediaWith(f);
            csv = csv.MediaWith(f1);
            var str = csv.AsString();
            Console.WriteLine(str);
        }

        static public void CsvTest2(int n)
        {
            var f = F.Get();
            Media<F> csv = new CsvMedia<F>();
            csv = csv.MediaWith(f);
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i <= n; i++)
            {
                var str = csv.AsString();
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }

        static public void JsonTest1(int n)
        {
            var f = F.Get();
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i <= n; i++)
            {
                var str = JsonSerializer.Serialize<F>(f);
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }
    }
}
