using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F401Async
    {
        public static void test()
        {
            Console.WriteLine($"Method Main/* {Task.CurrentId}" +
                        $" начал работу  в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
            WriteCharAsync('#');//асинх
            WriteChar('*');    //синх
            Console.WriteLine($"\nMethod Main/* {Task.CurrentId}" +
                        $" закончил работу  в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
            Console.ReadKey();
        }

        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Method WriteCharAsync/# {Task.CurrentId}" +
                        $" начал работу  в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
            await Task.Run(()=> WriteChar(symbol));
            Console.WriteLine($"\nMethod WriteCharAsync/# {Task.CurrentId}" +
                        $" закончил работу  в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");

        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Id задачи {Task.CurrentId}" +
                        $" Id потока {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
            for (int i = 0; i < 80; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
}
