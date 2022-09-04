using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F803ConcurrentBag
    {
        public static void test()
        {
            var bag = new ConcurrentBag<int>();
            bag.Add(1);
            bag.Add(2);
            bag.Add(3);
            bag.Add(4);
            bag.Add(5);
            Enumerate(bag);

            bool successPeek = bag.TryPeek(out int peekRes);
            bool successTake = bag.TryTake(out int takeRes);
             Console.WriteLine(successPeek ? $"TryPeek получил элемент {peekRes}":"Нет рез-та");
            Console.WriteLine(successTake ? $"TryTake получил элемент {takeRes}":"Нет рез-та");
            Enumerate(bag);

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
