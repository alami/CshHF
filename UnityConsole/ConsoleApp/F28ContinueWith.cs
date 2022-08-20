using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F28ContinueWith
    {
        public void test()
        {
            int a = 2, b = 3;
            Task<int> task = Task.Run<int>(() => Calc(a, b));
            task.ContinueWith(Continuation);
            /*task.ContinueWith((t) => { 
                Console.WriteLine($"Continuation task Id #{Task.CurrentId}." 
                    + $"Thread Id #{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Operation result -  {t.Result}");
                });*/
            
            Console.ReadKey();
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
