using System;
using System.Threading.Tasks;

namespace F504AsyncLambda
{
    internal class Program
    {
        private static async Task Main()
        {
            Func<Task> func = async () =>
            {
                Console.WriteLine("Lambda start");
                await Task.Run(() => Console.WriteLine("Task"));
                Console.WriteLine("Lambda end");
            };
            await func();
            Console.ReadKey();
        }
    }
}
