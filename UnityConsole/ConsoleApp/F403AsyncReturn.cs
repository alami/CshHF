using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F403AsyncReturn
    {
        public static async void test()
        {
            int x = 3, y = 5;

            Task<int> additionTask = AdditionAsync("[async]",x,y);
            int syncSum = Addition("[sync]",x,y);
            int asyncSum = 0;
            asyncSum = additionTask.Result;
            //asyncSum = additionTask.GetAwaiter().GetResult();
            //asyncSum = await additionTask;
            Console.WriteLine($"Async result {asyncSum}");
            Console.WriteLine($"Sync result {syncSum}");

            Console.WriteLine($"\nMethod Main  {Task.CurrentId}" +
                        $" закончил работу  в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
            Console.ReadKey();
        }

        private static int Addition(string operationName, int x, int y)
        {
            Console.WriteLine($"\nMethod Addition вызван {operationName} " +
                $" в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            return x+y;
        }

        private static async Task<int> AdditionAsync(string operationName, int x, int y)
        {
            //1st var
            int result = await Task.Run<int>(() => Addition(operationName, x, y));
            return result;
            //2st var
            //return await Task.Run<int>(() => Addition(operationName, x, y));
            //Err var
            //return Task.Run<int>(() => Addition(operationName, x, y));


        }

    }
}
