using System;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Meirmanova_HW5_Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            int cnt = 100;
            Console.WriteLine($"Количество итераций CSV: {cnt}");
            CSVSerializer(cnt);

            Console.WriteLine("\n" + $"Количество итераций JSON: {cnt}");
            JsonSerializer(cnt);
        }

        public static void CSVSerializer(int cnt)
        {
            var serSW = new Stopwatch();
            var deserSW = new Stopwatch();
            var ser = F.Get();

            for (int i = 0; i < cnt; i++)
            {
                serSW.Start();
                var csv = Serializer.SerializeToCsv(ser);
                serSW.Stop();
            }

            Console.WriteLine($"Сериалиализация: {serSW.ElapsedMilliseconds} мс");
            Console.WriteLine($"Десериалиализация: {deserSW.ElapsedMilliseconds} мс");
        }

        private static void JsonSerializer(int cnt)
        {
            var serSW = new Stopwatch();
            var deserSW = new Stopwatch();

            var instance = F.Get();
            for (var i = 0; i < cnt; i++)
            {
                serSW.Start();
                var csvContent = JsonConvert.SerializeObject(instance);
                serSW.Stop();

                deserSW.Start();
                var deserialized = JsonConvert.DeserializeObject(csvContent, typeof(F));
                deserSW.Stop();
            }

            Console.WriteLine($"Сериалиализация: {serSW.ElapsedMilliseconds} мс");
            Console.WriteLine($"Десериалиализации: {deserSW.ElapsedMilliseconds} мс");
        }
    }
}