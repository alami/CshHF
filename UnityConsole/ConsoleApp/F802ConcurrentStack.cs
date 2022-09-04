using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F802ConcurrentStack
    {
        public static void test()
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            Enumerate(stack);

            bool successPeek = stack.TryPeek(out int peekRes);
            bool successPop = stack.TryPop(out int popRes);
            Console.WriteLine(successPeek ? $"TryPeek получил элемент {peekRes}":"Нет рез-та");
            Console.WriteLine(successPop ? $"TryPop получил элемент {popRes}":"Нет рез-та");
            Enumerate(stack);

            stack.PushRange(new int[] { 6, 7, 8 });
            stack.PushRange(new int[] { 9, 10, 11 }, 0 ,2);
            Enumerate(stack);    // 10 9 8 7 6 4 3 2 1 

            int[] items = new int[5];
            int popAmount = stack.TryPopRange(items);
            Console.WriteLine($"popAmount : {popAmount}"); //5
            Enumerate(items);    // Элементы:  10 9 8 7 6
            Enumerate(stack);    // Элементы:  4 3 2 1


            //int[] items = new int[stack.Count];

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
