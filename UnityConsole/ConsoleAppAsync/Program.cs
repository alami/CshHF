using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppAsync
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine($"Method Async Main/* {Task.CurrentId}" +
                $" начал работу  в потоке {Thread.CurrentThread.ManagedThreadId}");
            await WriteCharAsync('#'); //aсинх
            WriteChar('*');    //синх
            Console.WriteLine($"\nMethod Main/* {Task.CurrentId}" +
                $" закончил работу  в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }
        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Method WriteCharAsync/# {Task.CurrentId}" +
                $" начал работу  в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => WriteChar(symbol));            
            Console.WriteLine($"\nMethod WriteCharAsync/# {Task.CurrentId}" +
                $" закончил работу  в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Id задачи {Task.CurrentId}" +
                $" Id потока {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
            for (int i = 0; i < 20; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
}
