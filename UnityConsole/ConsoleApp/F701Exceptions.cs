using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F701Exceptions
    {
        public static void test()
        {
            Thread thread = new Thread(Method);
            thread.Start();

            //ThreadPool.QueueUserWorkItem(Method);

            while (true)
            {
                Console.Write("*");
                Thread.Sleep(100);
            }
        }

        private static void Method(object _)
        {try
            {
                throw new Exception("Проверка исключения во 2-ричном потоке");
            }catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
