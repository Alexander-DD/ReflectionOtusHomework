using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            F f = F.Get();

            Stopwatch stopWatch = new Stopwatch();

            // Сериализация.
            // Задания 1 - 4.
            string serialysed = string.Empty;
            stopWatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                serialysed = MyCustomSerialyzer.SerializeFromObjectToCSV(f);
            }
            stopWatch.Stop();
            long timeSerialization = stopWatch.ElapsedMilliseconds;

            // Задание 6.
            stopWatch.Restart();
            Console.WriteLine($"Getted string:");
            Console.WriteLine(serialysed);
            Console.WriteLine($"Milliseconds spent: {timeSerialization}");
            stopWatch.Stop();
            long timeOutput = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"timeOutput: {timeOutput}");

            // Задания 7 - 8.
            string newtonsoftRes = string.Empty;
            stopWatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                newtonsoftRes = JsonConvert.SerializeObject(f);
            }
            stopWatch.Stop();
            long timeNewtonsoft = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"timeNewtonsoft: {timeNewtonsoft}");

            // Десериализация.
            // Задания 9 - 10.
            object deserialysedObj = null;
            stopWatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                deserialysedObj = MyCustomSerialyzer.DeserializeFromCSVToObject(serialysed);
            }
            stopWatch.Stop();
            long timeDeserialization = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"timeDeserialization: {timeDeserialization}");

            // Десериализация newtonsoft.
            F newtonsoftObj = null;
            stopWatch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                newtonsoftObj = JsonConvert.DeserializeObject<F>(newtonsoftRes);
            }
            stopWatch.Stop();
            long timeNewtonsoftDeser = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"timeNewtonsoftDeser: {timeNewtonsoftDeser}");


            F fDeserialysed = (F)deserialysedObj;
            Console.WriteLine("Before:");
            Console.WriteLine(f);
            Console.WriteLine("After:");
            Console.WriteLine(fDeserialysed);
        }
    }
}
