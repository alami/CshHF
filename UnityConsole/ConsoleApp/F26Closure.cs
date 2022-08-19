using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public struct Box
    {
        public int a;
        public int b;
    }
    public class F26Closure
    {
        private static int Calc (object arg)
        {
            Box box = (Box)arg;
            return box.a+box.b;
        }
        private static int Calc(int a, int b)
        {
            return a + b;
        }
        public void test()
        {
            int a = 3, b = 2;
            Box box; box.a = a; box.b = b;

            Task<int> task = new Task<int>(Calc, box);
            task.Start();            
            
            Console.WriteLine($"Сумма чисел: {task.Result}");
            Console.WriteLine(new string('-',80));

            Task<int> lambda = new Task<int>(() => Calc(a, 5));
            lambda.Start();

            Console.WriteLine($"Сумма чисел: {lambda.Result}");
            Console.WriteLine(new string('-', 80));

            Task<int> taskRun = Task<int>.Run<int>(() =>
            {
                int a1 = 5;
                int b1 = 5;
                return Calc(a1,b1) + Calc(a,b);
            });

            Console.WriteLine($"Сумма чисел: {taskRun.Result}");

            Task.Run(() => ShowSelfParameters(1, false, 'c', "hello", 3.14, new object(), box, new Program(), taskRun));
            Console.ReadKey();
        }

        private void ShowSelfParameters(int v1, bool v2, char v3, string v4, double v5, object v6, Box box, Program program, Task<int> taskRun)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(v1);
            Console.WriteLine(v2);
            Console.WriteLine(v3);
            Console.WriteLine(v4);
            Console.WriteLine(v5);
            Console.WriteLine(v6);
            Console.WriteLine(box.a+" "+box.b);
            Console.WriteLine(program.GetType().Name);
            Console.WriteLine(taskRun);
            Console.WriteLine(new string('-', 80));
        }
    }
}
