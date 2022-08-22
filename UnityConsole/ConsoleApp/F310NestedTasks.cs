using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F310NestedTasks
    {
        public static void test()
        {
            Task parent = new Task(() =>
            {
                new Task(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Nested 1 completed");
                }).Start();
                new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Nested 2 completed");
                }).Start();

                Thread.Sleep(200);
            });
            parent.Start();
            parent.Wait();
            Console.WriteLine("Completed");
            Console.ReadKey();
        }
    }
}
