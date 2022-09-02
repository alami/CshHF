using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F722LinkedToken
    {
        public static async Task test()
        {
            CancellationTokenSource parentCts1 = new CancellationTokenSource();
            CancellationTokenSource parentCts2 = new CancellationTokenSource();
            CancellationTokenSource parentCts3 = new CancellationTokenSource();

            CancellationTokenSource linkedCts4 = CancellationTokenSource
                .CreateLinkedTokenSource(parentCts1.Token, parentCts2.Token);
            CancellationTokenSource linkedCts5 = CancellationTokenSource
                .CreateLinkedTokenSource(linkedCts4.Token, parentCts3.Token);

            CancellationToken parentToken1 = parentCts1.Token;
            CancellationToken parentToken2 = parentCts2.Token;
            CancellationToken parentToken3 = parentCts3.Token;
            CancellationToken linkedToken1 = linkedCts4.Token;
            CancellationToken linkedToken2 = linkedCts5.Token;

            var t1 = Task.Run(() => Do("1", parentToken1), parentToken1);
            var t2 = Task.Run(() => Do("2", parentToken1), parentToken2);
            var t3 = Task.Run(() => Do("3", parentToken1), parentToken3);
            var t4 = Task.Run(() => Do("4", linkedToken1), linkedToken1);
            var t5 = Task.Run(() => Do("5", linkedToken2), linkedToken2);

            parentToken1.Register(() => Canceled(1));
            parentToken2.Register(() => Canceled(2));
            parentToken3.Register(() => Canceled(3));
            linkedToken1.Register(() => Canceled(4));
            linkedToken2.Register(() => Canceled(5));

            parentCts1.CancelAfter(1500);

            Console.WriteLine("Основной поток завершен.");
            Console.ReadKey();
        }

        private static void Do(string taskId, CancellationToken token)
        {
            Console.WriteLine($"Задача #{taskId} в потоке " +
                $"{Thread.CurrentThread.ManagedThreadId} начала свою работу.");
            Thread.Sleep(1000);
            int sum = 0;
            for (int i = 0; i < 150; i++)
            {  
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1);
                sum += i;
            }
            Console.WriteLine($"\tЗадача #{taskId} в потоке " +
                $"{Thread.CurrentThread.ManagedThreadId} насчитала - {sum}.");
        }
        private static void Canceled(int taskId)
        {
            Console.WriteLine($"--- Задача #{taskId} в потоке " +
                $"{Thread.CurrentThread.ManagedThreadId} была отменена.---");
        }
    }        
}
