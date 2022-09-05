using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F806BlockingCollection
{
    internal class Program
    {
        static async Task Main()
        {
            Console.SetWindowSize(80, 44);
            using (BlockingCollection<int> collection
                = new BlockingCollection<int>(new ConcurrentStack<int>(), 5))
            {
                Console.WriteLine($"Максимальный внутренний размер: {collection.BoundedCapacity}\n");
                Action<int, int> action = (startValue, endValue) =>
                {
                    for (int i = startValue; i <= endValue; i++)
                    {
                        bool isSuccess = collection.TryAdd(i);
                        ShowElementStatus(isSuccess, i);
                    };
                };
                var producer1 = Task.Run(() => action.Invoke(1, 25));
                var producer2 = Task.Run(() => action.Invoke(26, 50));
                var consumer = Task.Run(() => {

                    List<int> elements = new List<int>();
                    while (collection.IsCompleted == false)
                    {
                        if (collection.TryTake(out int item) == true) { 
                            elements.Add(item);
                            Console.WriteLine($"Извлечен элемент  {item}");
                        }
                        Thread.Sleep(500);
                    };

                    Console.Write($"\n\nПолученные элементы ({elements.Count}) через TryTake:");
                    foreach (var item in elements) { Console.Write($"{item} "); }
                    Console.WriteLine();
                });

                await Task.WhenAll( producer1, producer2);
                ShowPropertiesInfo(collection, "До CompletedAdding");

                collection.CompleteAdding();
                ShowPropertiesInfo(collection, "После CompletedAdding");
                
                await consumer;
                ShowPropertiesInfo(collection, "После работы Consumer");
            }
            Console.WriteLine($"Работа завершена");
            Console.ReadKey();

        }

        private static void ShowElementStatus(bool isSuccess, int value)
        {
            if (isSuccess == true)
            {
                Console.WriteLine($"Добавлен элемент: {value}");
            } else
            {
                Console.WriteLine($"                Не добавлен элемент: {value}");
            }
        }

        private static void ShowPropertiesInfo(BlockingCollection<int> bc, string when)
        {
            Console.WriteLine($"\n{when}");
            Console.WriteLine($"Св-во IsCompleted: {bc.IsCompleted}");
            Console.WriteLine($"Св-во IsAddingCompleted: {bc.IsAddingCompleted}");
            Console.WriteLine(new String('-', 80));
        }
    }
}
