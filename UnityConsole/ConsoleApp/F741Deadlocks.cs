using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F741Deadlocks
    {
        private static object syncRoot1 = new object();
        private static object syncRoot2 = new object();
        public static async Task test()
        {
            Task t1 = Task.Run(() => //Console.WriteLine($"Задача #1 выполнилась!"));
            {
                lock (syncRoot1)
                {
                    Thread.Sleep(1000);
                    lock(syncRoot2)
                    {
                        Console.WriteLine($"Задача #1 выполнилась!");
                    }
                }
            });

            Task t2 = Task.Run(() =>
            {
                lock (syncRoot2)
                {
                    Thread.Sleep(1000);
                    lock (syncRoot1)
                    {
                        Console.WriteLine($"Задача #2 выполнилась!");
                    }
                }
            });
            //Task t1 = Task.Run(Solution);
            //Task t2 = Task.Run(Solution);

            await Task.WhenAll(t1,t2);
            Console.WriteLine($"Метод Майн закончился!");
            Console.ReadKey();
        }
        private static void Solution()
        {
            lock (syncRoot1)
            {
                Thread.Sleep(1000);
                lock (syncRoot2)
                {
                    Console.WriteLine($"Задача #1 выполнилась!");
                }
            }

        }
    }        
}
