using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F732TaskContinuations
    {
        public static async Task test()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<int> task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"Задача #1 выполнилась.");
                return 10;
            });

            Task<int> c1 = task.ContinueWith((t) => {
                Console.WriteLine($"Задача #2 (продолжение) выполнилась.");
                return t.Result * 2;
            }, cts.Token, TaskContinuationOptions.LazyCancellation, TaskScheduler.Default);

            Task c2 = c1.ContinueWith((t) => {
                Console.WriteLine($"Задача #3 (продолжение) выполнилась.");
                try {
                    Console.WriteLine($"Результат выполнения = {t.Result}");
                }
                catch (AggregateException ex) {
                    Console.WriteLine($"    Ошибка {ex.InnerException.GetType()}");
                    Console.WriteLine($"    Сообщение:{ex.InnerException.Message}");
                }
            });

            cts.Cancel();
            Thread.Sleep(5000);
            Console.WriteLine($"\nСтатус задачи #1 - {task.Status}.");
            Console.WriteLine($"\nСтатус задачи #2 - {c1.Status}.");
            Console.WriteLine($"\nСтатус задачи #3 - {c2.Status}.");


            Console.WriteLine("Основной поток завершен.");
            Console.ReadKey();
        }
    }        
}
