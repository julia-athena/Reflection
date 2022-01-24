using System;
using System.Text.Json;

namespace Reflection
{
    class F { public int i1, i2, i3, i4, i5; 
        public F() { } 
        public static F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; 
    }
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1000;
            CsvTest1();
            CsvTest2(n);
            JsonTest1(n);
            CsvTest4();
            Console.WriteLine("_____");
            CsvTest5(n);
            JsonTest2(n);
        }


        static void CsvTest1()
        {
            var f = F.Get();
            var f1 = new F() { i1 = 5, i2 = 4, i3 = 3, i4 = 2, i5 = 1 };
            Media<F> csv = new CsvMedia<F>();
            csv = csv.MediaWith(f);
            csv = csv.MediaWith(f1);
            var str = csv.AsString();
            Console.WriteLine(str);
        }

        static void CsvTest2(int n)
        {
            var f = F.Get();
            Media<F> csv = new CsvMedia<F>();
            csv = csv.MediaWith(f);
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                var str = csv.AsString();
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }

        static void JsonTest1(int n)
        {
            var f = F.Get();
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                var str = JsonSerializer.Serialize<F>(f, options);
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }
        static void CsvTest4()
        {
            var f = F.Get();
            var f1 = new F() { i1 = 5, i2 = 4, i3 = 3, i4 = 2, i5 = 1 };
            Media<F> media = new CsvMedia<F>();
            media = media.MediaWith(f).MediaWith(f1);
            var csv = media.AsString();
            Content<F> content = new CsvContent<F>(csv);
            var data = content.Data();
        }
        static void CsvTest5(int n)
        {
            var f = F.Get();
            Media<F> media = new CsvMedia<F>();
            media = media.MediaWith(f);
            string csv = media.AsString();
            Content<F> content = new CsvContent<F>(csv);
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                var data = content.Data();
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }
        static void JsonTest2(int n)
        {
            var f = F.Get();
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            var str = JsonSerializer.Serialize<F>(f, options);
            DateTime start;
            DateTime stop;
            TimeSpan elapsed = new TimeSpan();
            start = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                var data = JsonSerializer.Deserialize<F>(str, options);
            }
            stop = DateTime.Now;
            elapsed = stop.Subtract(start);
            Console.WriteLine(Convert.ToString(elapsed.TotalMilliseconds));
        }
    }
}
