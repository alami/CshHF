using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F24TaskStatus
    {
        public void test()
        {
            Task task = new Task(new Action(Method));
            Console.WriteLine($"{task.Status}");                // Created
            task.Start();

            Console.WriteLine($"{task.Status}");                //WaitingToRun 
            Thread.Sleep(1000);  //даем поработать методу Method

            Console.WriteLine($"{task.Status}");                 //Running
            Thread.Sleep(2000);  //даем поработать методу Method

            Console.WriteLine($"{task.Status}");                //RanToCompletion
            Thread.Sleep(1000);  //даем поработать методу Method

        }

        private void Method()
        {
            Thread.Sleep(2000);  //даем поработать методу Main
        }
    }
}
