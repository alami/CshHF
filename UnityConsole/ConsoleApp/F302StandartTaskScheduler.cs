using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F302StandartTaskScheduler
    {
        public void test()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");
            Timer timer = new Timer(ShowThreadPoolInfo, null, 1000, 1000);
            Task[] tasks = new Task[30];

            TaskScheduler scheduler = null;
            //scheduler = TaskScheduler.Default;
            scheduler = new F303ThreadTaskScheduler();

            Console.WriteLine($"TaskScheduler - {scheduler.GetType()}");

            for (int i = 0; i < 30; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine($"Задача {Task.CurrentId}" +
                        $" выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}" +
                        $" из пула потоков - {Thread.CurrentThread.IsThreadPoolThread}");
                });
                tasks[i].Start(scheduler);
            }
            Task.WaitAll(tasks);
            Thread.Sleep(2000);
            timer.Dispose();

            Console.ReadKey();
        }

        private void ShowThreadPoolInfo(object _)
        {
            ThreadPool.GetAvailableThreads(out int threads, out int completionPorts);
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPorts);
            Console.WriteLine($"            Worker Threads - [{threads}:{maxThreads}]");
            Console.WriteLine($"            Worker Completion Ports - [{completionPorts}:{maxCompletionPorts}]");
        }
    }
}