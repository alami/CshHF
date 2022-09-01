using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F703ExceptionsNested
    {
        public static void test()
        {
            Task parent = new Task(() => {
                new Task(() => {
                    Thread.Sleep(500);
                    Console.WriteLine("Вложенная задача #1 завершила работу");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => {
                    Thread.Sleep(600);
                    Console.WriteLine("Вложенная задача #2 завершила работу");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => {
                    Thread.Sleep(700);
                    throw new Exception("Вложенная задача #3 совершила ошибку");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => {
                    Thread.Sleep(800);
                    Console.WriteLine("Вложенная задача #4 завершила работу");
                }).Start();
                new Task(() => {
                    new Task(() => {
                        throw new Exception("Вложенная задача #5-1 совершила ошибку");
                    }, TaskCreationOptions.AttachedToParent).Start();
                    Thread.Sleep(900);
                    throw new Exception("Вложенная задача #5 совершила ошибку");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => {
                    Thread.Sleep(800);
                    Console.WriteLine("Вложенная задача #6 завершила работу");
                }, TaskCreationOptions.AttachedToParent).Start();
            });
            parent.Start(); //--ждет все дочерние, даже с исключениями

            try
            {
                parent.Wait();
            } catch (AggregateException ex) {
                Console.WriteLine($"~~~~ Ups! ~~~");
                /*foreach (var item in ex.InnerExceptions)
                {
                    if (item is AggregateException aggregateException)
                    {
                        foreach (var innerException in aggregateException.InnerExceptions) {
                            Console.WriteLine($"Сообщение из исключения дочерней задачи - {innerException.Message}");
                        }
                    } else
                    {
                        Console.WriteLine($"Сообщение из исключения родительской задачи - {item.Message}");
                    }
                }*/
                HandleTaskExceptions(ex);
            }
            Console.WriteLine();
            Console.WriteLine($"Статус родительской задачи - {parent.Status}");
            Console.WriteLine("Метод Main завершил свою работу");
            Console.ReadKey();
        }

        private static void HandleTaskExceptions(AggregateException parentException)
        {
            foreach (var innerException in parentException.InnerExceptions)
            {
                if (innerException is AggregateException aggregateException)
                {
                    HandleTaskExceptions(aggregateException);
                }
                else
                {
Console.WriteLine($"Сообщение из исключения родительской задачи - {innerException.Message}");
                }
            }
        }
    }
}
