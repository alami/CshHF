using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F801Concurrent
    {
        public static void test()
        {
            var queue = new ConcurrentQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            Enumerate(queue);

            bool successPeek = queue.TryPeek(out int peekRes);
            bool successDequeue = queue.TryDequeue(out int dequeueRes);
             Console.WriteLine(successPeek ? $"TryPeek получил элемент {peekRes}":"Нет рез-та");
            Console.WriteLine(successDequeue ? $"TryDequeue получил элемент {dequeueRes}":"Нет рез-та");
            Enumerate(queue);

        }

        private static void Enumerate<T>(IEnumerable<T> queue)
        {
            Console.WriteLine();
            Console.WriteLine($"Кол-во элементов: {queue.Count()}");
            Console.Write($"Элементы: ");
            foreach (var item in queue) {
                Console.Write($" {item}");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-',80));

        }
    }        
}
