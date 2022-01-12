using System;

namespace Reflection
{
    class F { public int i1, i2, i3, i4, i5; public static F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
    class Program
    {
        static void Main(string[] args)
        {
            CsvTest1();
            CsvTest2();
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

        static public void CsvTest2()
        {
            var f = F.Get();
            Media<F> csv = new CsvMedia<F>();
            csv = csv.MediaWith(f);
            //замерить время
            for (int i = 0; i < 1000; i++)
            {
                var str = csv.AsString();
            }
            //замерить время 
            //разница времен
        }

        static public void JsonTest()
        {
            var f = F.Get();
        }
    }
}
