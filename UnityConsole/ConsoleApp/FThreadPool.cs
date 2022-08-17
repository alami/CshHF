using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FThreadPool
    {
        public void testthreads ()
        {
            Console.WriteLine($"Id потока метода Маin - {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example1));
            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example2));
            Report();
            Console.ReadKey();
            Report();
        }
        private static void Example1(object state)
        {
            Console.WriteLine($"метоа Example1 начал вып-ся в потоке - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"метоа Example1 начал вып-ся в потоке - {Thread.CurrentThread.ManagedThreadId}");
        }
        private static void Example2(object state)
        {
            Console.WriteLine($"метоа Example2 начал вып-ся в потоке - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"метоа Example2 начал вып-ся в потоке - {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Report()
        {
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxPortThreads);
            ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);
            Console.WriteLine($"Рабочие потоки {workerThreads} из {maxWorkerThreads}");
            Console.WriteLine($"IO потоки {portThreads} из {maxPortThreads}");
            Console.WriteLine(new string('-', 80));
        }
    }
}
