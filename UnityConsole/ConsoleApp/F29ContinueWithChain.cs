using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F29ContinueWithChain
    {
        public void test()
        {
            Task<int> task = Task.Run<int>(new Func<int>(GetValue));
            /*Task<int> c1 = task.ContinueWith<int>(Increment);
            Task<int> c2 = c1.ContinueWith<int>(Increment);
            Task<int> c3 = c2.ContinueWith<int>(Increment);
            Task<int> c4 = c3.ContinueWith<int>(Increment);
            Task<int> c5 = c4.ContinueWith<int>(Increment);
            c5.ContinueWith(ShowRes);*/

            task.ContinueWith<int>(Increment)
                .ContinueWith<int>(Increment)
                .ContinueWith<int>(Increment)
                .ContinueWith<int>(Increment)
                .ContinueWith<int>(Increment)
                .ContinueWith(ShowRes);

            Console.WriteLine($"Метод Main завершил работу...");

            Console.ReadKey();
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
