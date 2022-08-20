using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F29FactoryTask
    {
        private static Random random = new Random(); 
        public void test()
        {
            TaskFactory taskFactory = new TaskFactory();  //= Task.Factory;

            Task<double> t1 = taskFactory.StartNew(() => { return Calculate(1); });
            Task<double> t2 = taskFactory.StartNew(() => { return Calculate(2); });
            Task<double> t3 = taskFactory.StartNew(() => { return Calculate(3); });
            Task<double> t4 = taskFactory.StartNew(() => { return Calculate(4); });
            Task<double> t5 = taskFactory.StartNew(() => { return Calculate(5); });

            taskFactory.ContinueWhenAll(new Task[] { t1, t2, t3, t4, t5 },
                completedTasks =>
                {
                    double sum = 0.0;
                    foreach (Task<double> item in completedTasks) sum+=item.Result;
                    Console.WriteLine($"Result - {sum:N}");
                });                
            Console.WriteLine($"Метод Main завершил работу...");
            Console.ReadKey();
        }

        private double Calculate(int x)
        {
            double res = 0.0;
            for (int i = 0; i < 10; i++)
            {
                res += (i * random.Next(1, x) / (x * 2) * x);
            }
            return res;
        }

        private static void ShowRes(Task<int> arg1)
        {
            Console.WriteLine($"Продолжение task Id #{Task.CurrentId}. "
                + $"Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Результат {arg1.Result}");
        }

        private static int Increment(Task<int> arg1)
        {
            Console.WriteLine($"Продолжение task Id #{Task.CurrentId}. "
                + $"Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            return arg1.Result + 1;
        }

        private static int GetValue()
        {
            return 10;
        }

        private static void Continuation(Task<int> t)
        {
            Console.WriteLine($"Continuation task Id #{Task.CurrentId}."
                + $"Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Operation result -  {t.Result}");
        }

        private static int  Calc(int a, int b)
        {
            Console.WriteLine($"Task Id #{Task.CurrentId}."
                + $"Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            return a + b;
        }
    }
}
