using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F22ReturnResults
    {
        public void test()
        {
            //Action threadOutput = new Action(ThreadOutput);
            //Task task = new Task(threadOutput); //(1)
            Task<int> task = new Task<int>(new Func<int>(GetIntResult)); //(1)

            task.Start();

            //MainOutput();
            Console.WriteLine($"Результа асинк оп-ции (Int) #1 - {task.Result}");
            Thread.Sleep(1000);

            TaskFactory taskFactory = new TaskFactory();  //(2)
            Task<bool> task2 = taskFactory.StartNew(new Func<bool>(GetBoolResult));
            Console.WriteLine($"Результа асинк оп-ции (Bool) #2 - {task2.Result}");
            Thread.Sleep(1000);

            Task<bool> task3 = Task.Run(new Func<bool>(GetBoolResult));      //(3)
            Console.WriteLine($"Результа асинк оп-ции (Bool) #3 - {task3.Result}");

        }
        private static bool flag = false;
        public static int GetIntResult() { return 1; }
        public static bool GetBoolResult() {
            if (flag)
            {
                flag = false;
                return true;
            } else
            {
                flag = true;
                return false;
            }
        }
    }
}
