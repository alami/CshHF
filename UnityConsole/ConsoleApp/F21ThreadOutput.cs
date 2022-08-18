using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F21ThreadOutput
    {
        public void test()
        {
            Action threadOutput = new Action(ThreadOutput);

            Task task = new Task(threadOutput); //(1)
            task.Start();
            MainOutput();

            TaskFactory taskFactory = new TaskFactory();  //(2)
            taskFactory.StartNew(threadOutput);
            MainOutput();

            Task.Run(threadOutput);      //(3)
            MainOutput();

            task = new Task(threadOutput); //(4)
            task.RunSynchronously();
            MainOutput();
        }
        public void ThreadOutput ()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write('*');
                Thread.Sleep(75);
            }
        }
        public void MainOutput()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write('!');
                Thread.Sleep(75);
            }
        }
    }
}
