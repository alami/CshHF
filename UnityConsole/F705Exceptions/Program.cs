using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F705Exceptions
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine($"Метод Майн начал свою работу.");

            try
            {
                //await OperationAsync();
                await OperationsAsync();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Исключение - {ex.GetType()}");
                Console.WriteLine($"Сообщение - {ex.Message}");
                Console.ResetColor();
            }
            /*--обработка неск-ких асинх исключений 1-вар
            Task tasks = OperationsAsync();
            try { await tasks; }
            catch
            {
                AggregateException taskExceptions = tasks.Exception;
                ReadOnlyCollection<Exception> exceptions = taskExceptions.InnerExceptions;
                foreach (Exception exception in exceptions)
                {
                    Console.WriteLine($"Сообщения исключения: {exception.Message}");
                }
            }
            //2-вар
            try
            {
                Task tasks = OperationsAsync();
                await tasks.ContinueWith((t) => { }, TaskContinuationOptions.ExecuteSynchronously); 
                tasks.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                Console.WriteLine($"Сообщения исключения: {exception.Message}");
            }
            //3-вар
            await OperationsAsync().ContinueWith(t =>
            {
                try { t.Wait(); }
                catch (AggregateException ex)
                {
                    foreach (var exception in ex.InnerExceptions)
                        Console.WriteLine($"Сообщения исключения: {exception.Message}");
                }
            },TaskContinuationOptions.ExecuteSynchronously);
            */

            Console.WriteLine($"Метод Майн закончил свою работу.");
            Console.ReadKey();
        }

        private static async Task OperationAsync()
        {
            Console.WriteLine($"Код метода OperationAsync до ошибки");

            await Task.Run(() => throw new Exception("Ошибка в методе OperationAsync"));

            Console.WriteLine($"Код метода OperationAsync после ошибки");
        }
        private static Task OperationsAsync()
        {         
            Action<int> operation = (taskNumber) => {
                Thread.Sleep(taskNumber);
                throw new Exception($"Задача #{taskNumber} выбросила исключение в методе OperationAsync");
            };
            
            Task t1 = Task.Run(() => operation.Invoke(1));
            Task t2 = Task.Run(() => operation.Invoke(2));
            Task t3 = Task.Run(() => operation.Invoke(3));

            return Task.WhenAll(t1, t2, t3);
        }
    }
}
