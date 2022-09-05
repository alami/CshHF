using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F807GetConsumingEnumerable
    {
        public static void test()
        {

            using (BlockingCollection<int> collection = new BlockingCollection<int>())
            {
                Console.WriteLine($"\nКоллекция пустая (Count = {collection.Count}).");
                Console.WriteLine($"\nРабота с коллекцией завершена -  {collection.IsCompleted}");
                var producer = Task.Run(() =>
                {
                    for (int i = 0; i < 25; i++)
                    {
                        collection.Add(i);
                        Thread.Sleep(1000);
                    }
                });
                Console.WriteLine($"Спим ... Даем поработать задаче");
                Thread.Sleep(3000);
                Console.WriteLine($"Проснулись");
                Console.WriteLine($"\nЭлементы");
                foreach (var item in collection.GetConsumingEnumerable())
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine($"\nКоллекция пустая (Count = {collection.Count}).");
                Console.WriteLine($"\nРабота с коллекцией завершена -  {collection.IsCompleted}");
            }
            Console.WriteLine($"\n\n\nКонец работы");
            Console.ReadKey();
        }
    }        
}
