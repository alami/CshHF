using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FThreadPoolWorker
    {
        public void testthreadPW ()
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            ThreadPoolWorker threadPoolWorker = new ThreadPoolWorker(new Action<object>(StarWriter));
            threadPoolWorker.Start('*');

            for (int i = 0; i < 40; i++)
            {
                Console.Write("-");
                Thread.Sleep(50);
            }
            threadPoolWorker.Wait();
            Console.WriteLine($"\nМетод Main закончил работу...");
        }

        private void StarWriter(object arg)
        {
            char item = (char)arg;
            for (int i = 0; i < 120; i++)
            {
                Console.Write(item);
                Thread.Sleep(50);
            }
        }
    }
}
