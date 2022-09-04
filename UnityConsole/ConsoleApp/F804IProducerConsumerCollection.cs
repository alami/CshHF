using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F804IProducerConsumerCollection
    {
        public static void test()
        {
            //IProducerConsumerCollection<int> collection = new ConcurrentQueue<int>();
            //IProducerConsumerCollection<int> collection = new ConcurrentStack<int>();
            IProducerConsumerCollection<int> collection = new ConcurrentBag<int>();
            collection.TryAdd(1);
            collection.TryAdd(2);
            collection.TryAdd(3);
            collection.TryAdd(4);
            collection.TryAdd(5);
            Enumerate(collection);

            bool successTake = collection.TryTake(out int takeRes);
            
            Console.WriteLine(successTake ? $"TryTake получил элемент {takeRes}" : "Нет рез-та");
            
            Enumerate(collection);

        }

        private static void Enumerate<T>(IEnumerable<T> collection)
        {
            Console.WriteLine();
            Console.WriteLine($"Кол-во элементов: {collection.Count()}");
            Console.Write($"Элементы: ");
            foreach (var item in collection) {
                Console.Write($" {item}");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-',80));

        }
    }        
}
