using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F23TaskOptions
    {
        public void test()
        {

            Console.WriteLine($"Task Id of method Main - {Task.CurrentId ?? -1}");
            Console.WriteLine($"Id потока (Thread) of method Main - {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));

            Task task = new Task(new Action(DoSomething),
                TaskCreationOptions.PreferFairness |
                TaskCreationOptions.LongRunning);
            task.Start();
            Thread.Sleep(50);  //даем поработать методу DoSomething

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"        Задача выполняется.");
                Thread.Sleep(100);
            }
            Console.WriteLine($"Задача завершена в потоке - {Thread.CurrentThread.ManagedThreadId}");
        }
        public static void DoSomething()
        {
            Console.WriteLine($"Task Id of method DoSomething - {Task.CurrentId}");
            Console.WriteLine($"Id потока (Thread) of method DoSomething - {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"        Main выполняется.");
                Thread.Sleep(100);
            }
            Console.WriteLine($"Main завершена в потоке - {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
