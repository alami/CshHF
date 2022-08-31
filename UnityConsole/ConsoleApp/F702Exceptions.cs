using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F702Exceptions
    {
        public static void test()
        {
            Task<int> task = Task.Run(DoWork);
            /* */  
            try
            {
                //task.Wait();
                Console.WriteLine(task.Result);
                Console.WriteLine($"Код метода Майн продолжил выполнение без ошибок...");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Внешнее исключение - {ex.GetType()}");
                Console.WriteLine($"   и его Сообщение - [{ex.Message}]");

                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine(new string('-', 80));

                    Console.WriteLine($"Вложенное исключение - {item.GetType()}");
                    Console.WriteLine($"     и его сообщение - [{item.Message}]");
                }
            }
            /*  */
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Press any key..");
            Console.ReadKey();
        }
        private static int DoWork()
        {
            //try
            //{
                Console.WriteLine($"Код метода DoWork до ошибки");
                throw new Exception("Ошибка в методе DoWork");
            /*}
            catch (Exception ex)
            {
                Console.WriteLine($"Код метода DoWork после catch");
            }*/
            Console.WriteLine($"Код метода DoWork после ошибки");
            return -1;
        }
    }
}
