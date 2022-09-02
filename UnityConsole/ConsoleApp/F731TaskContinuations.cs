using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F731TaskContinuations
    {
        public static async Task test()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<int> task = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Задача #1 выполнилась.");
                return 10;
            }, cts.Token);

            Task c1 = task.ContinueWith((t) => {
                Console.WriteLine($"Задача #2 (продолжение) выполнилась.");
                if (t.Status != TaskStatus.Canceled && t.Status != TaskStatus.Faulted)
                    Console.WriteLine($"Результат = {t.Result}");
            });

            cts.Cancel();
            Thread.Sleep(3000);
            Console.WriteLine($"\nСтатус задачи #1 - {task.Status}.");
            Console.WriteLine($"\nСтатус задачи #2 - {c1.Status}.");


            Console.WriteLine("Основной поток завершен.");
            Console.ReadKey();
        }
    }        
}
