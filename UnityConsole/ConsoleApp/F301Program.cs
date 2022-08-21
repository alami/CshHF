using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F301Program
    {
        public void test()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");

            Task[] tasks = new Task[10];
            F301ReviewTaskScheduler reviewTaskScheduler = new F301ReviewTaskScheduler();

            //QueueTaskTesting(tasks, reviewTaskScheduler);
            //TryExecuteTaskInlineTesting(tasks, reviewTaskScheduler);
            TryDequeueTesting(tasks, reviewTaskScheduler);

            try
            {
                Task.WaitAll(tasks);
            } catch {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Несколько задач были отменены!");
                Console.ResetColor();
            } finally {
                Console.WriteLine($"Метод Main закончил свое выполнение");
            }
            Console.ReadKey();
        }

        private static void QueueTaskTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                });
                tasks[i].Start(scheduler);
            }
        }
        private static void TryExecuteTaskInlineTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                    return 1;
                });
            }
            foreach (var task in tasks)
            {
                task.Start(scheduler);
                task.Wait();
                // int result = ((Task<int>)task).Result;
            }
        }
        private static void TryDequeueTesting(Task[] tasks, TaskScheduler scheduler)
        {
            #region скоординированная отмена
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            cts.CancelAfter(555);
            #endregion
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                }, token);
                tasks[i].Start(scheduler);
            }
        }
    }
}