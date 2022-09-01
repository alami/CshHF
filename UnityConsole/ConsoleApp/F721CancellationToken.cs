using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F721CancellationToken
    {
        public static async Task test()
        {
            Console.WriteLine("Основной поток запущен.");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Run(()=>MyTask(cts.Token), cts.Token);

            Thread.Sleep(2000);
            try
            {
                cts.Cancel();
                task.Wait();
            }
            catch (AggregateException ex)
            {
                if (task.IsCanceled) Console.WriteLine("\nЗадача task отменена.\n");
                if (task.IsFaulted) Console.WriteLine("\nЗадача task провалена.\n");
                Console.WriteLine(ex.InnerException.Message);
            } finally { 
                task.Dispose();
                cts.Dispose();
            }
            Console.WriteLine("Основной поток завершен.");
            Console.ReadKey();
        }

        private static void MyTask(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine("MyTask() запущен.");
            for (var count = 0; count < 10; count++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Получен запрос на отмену задачи");
                    cancellationToken.ThrowIfCancellationRequested();
                    //throw new OperationCanceledException(cancellationToken);
                }
                Thread.Sleep(500);
                Console.WriteLine("В методе MyTask(), счетчик = "+count);
            }
            Console.WriteLine("MyTask() завершен.");
        }

     }
}
