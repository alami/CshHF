using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F305ProgramPriorityTS
    {
        public void test()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");
            Timer timer = new Timer(ShowThreadPoolInfo, null, 1000, 1000);


            F306PriorityTaskScheduler scheduler = new F306PriorityTaskScheduler();
            Console.WriteLine($"TaskScheduler - {scheduler.GetType()}");

            Task[] tasks = new Task[30];
            

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
            Task lowPriorityTask = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"НИЗКОПРИОРИТЕТНАЯ Задача {Task.CurrentId}" +
                    $" выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
            });
            lowPriorityTask.Start(scheduler);
            scheduler.Deprioritize(lowPriorityTask);
            
            Console.WriteLine("Высокоприоритетные задачи начались позже,но выполняться первее.");
            for (int i = 0; i < 15; i++)
            {
                Task task = new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine($"ВЫСОКОПРИОРИТЕТНАЯ Задача {Task.CurrentId}" +
                        $" выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                });
                task.Start(scheduler);
                scheduler.Prioritize(task);
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