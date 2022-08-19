using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F27ContinueWith
    {
        public void test()
        {
    
            Task task = new Task(new Action<object>(OperationAsync), "Hello world");
            Task cont = task.ContinueWith(new Action<Task>(Continuation));

            Console.WriteLine($"Статус продолжения: {cont.Status}");
            task.Start();            
            
            Console.ReadKey();
        }

        private void OperationAsync(object arg)
        {
            Console.WriteLine($"\nЗадача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Argument value - {arg.ToString()}");
            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}.");
        }
        private void Continuation(Task task)
        {
            Console.WriteLine($"\nПродолжение #{Task.CurrentId} сработало в потоке {Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Параметр задачи - {task.AsyncState}");
            Console.WriteLine($"Сразу после выполнения задачи.");
        }
    }
}
