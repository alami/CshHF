using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FThreadPoolWorker2
    {
        public void testthreadPW ()
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            ThreadPoolWorker2<int> threadPoolWorker = new ThreadPoolWorker2<int>(SumNumber);
            threadPoolWorker.Start(1000);

            while (threadPoolWorker.Completed==false)  //for (int i = 0; i < 40; i++)
            {
                Console.Write("*");
                Thread.Sleep(35);
            }
                                                    //threadPoolWorker.Wait();
            Console.WriteLine();
            Console.WriteLine($"\nРезультат асинх оп-ции = {threadPoolWorker.Result:N}");
        }

        private static int SumNumber(object arg)
        {
            int number = (int)arg;
            int sum = 0;
            for (int i = 0; i < number; i++)
            {
                sum += 1;// Console.Write(item);
                Thread.Sleep(1);
            }
            return sum;
        }
    }
}
