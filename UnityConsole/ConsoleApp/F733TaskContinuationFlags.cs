using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F733TaskContinuationFlags
    {
        public static async Task test()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<int> task = Task.Run(() => 555);
            //Task<int> task = Task.Run(() => 555, cts.Token);
            //Task<int> task = Task.Run(() => { throw new Exception("Проверка продолжений"); return 555; });            

            //cts.Cancel();

            task.ContinueWith((t) => {
                Console.WriteLine($"Результат задачи = {t.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith((t) => {
                Console.WriteLine($"Задача была отменена.");
            }, TaskContinuationOptions.OnlyOnCanceled);

            task.ContinueWith((t) => {
                Console.WriteLine($"Обработка ошибок задачи.");
                Console.WriteLine($"Сообщение ошибки: {t.Exception.InnerException.Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);

            Console.ReadKey();
        }
    }        
}
