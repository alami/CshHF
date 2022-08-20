using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F2AValueTask
    {
        public void test()
        {
            CalculationAndShowAsync(99).GetAwaiter().GetResult();
            Console.ReadKey(); 
        }

        private static ValueTask CalculationAndShowAsync(int ceiling)
        {
            if (ceiling < 0)
            {
                //return Task.CompletedTask;
                return new ValueTask();
            } else {
                return new ValueTask(Task.Run(() => {
                    Calcular(ceiling);
                }));
            }

        }

        private static void Calcular(int ceiling)
        {
            int sum = 0;
            for (int i = 0; i < ceiling; i++) sum += i;
            Console.WriteLine($"результат - {sum}. Найден  в задаче #{Task.CurrentId} "
                + $" в потоке #{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
