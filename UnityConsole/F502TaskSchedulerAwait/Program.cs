using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace F50TaskSchedulerAwait
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.SetWindowSize(90,40);
            ShowData("Main выполнился до await");
            //var mainTask = new Task<Task>(MethodAsync);
            var mainTask = new Task<Task>(MethodAsync);
            mainTask.Start(new AwaitableTestTaskScheduler());
            await await mainTask;
            //var result = await mainTask;
            //await result;
            ShowData("Main выполнился после await");
            Console.ReadKey();
        }
        private static async Task MethodAsync()
        {
            ShowData("MethodAsync выполнился до await");
            var task = new Task(TestMethod, TaskCreationOptions.HideScheduler);
            task.Start();
            await task;
            ShowData("MethodAsync выполнился после await");
        }
        private static void TestMethod()
        {
            ShowData("TestMethod выполнился");
        }
        private static void ShowData(string v)
        {
            Console.WriteLine($"Имя потока: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Id потока: {Thread.CurrentThread.ManagedThreadId}." +
                $" Поток из пула потоков: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Id Задачи: {Task.CurrentId}");
            Console.WriteLine($"Текущий планировщик задач: " +
$"{typeof(TaskScheduler).GetProperty("InternalCurrent", BindingFlags.Static| BindingFlags.NonPublic).GetValue(typeof(TaskScheduler))}");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine();
        }
    }
}
