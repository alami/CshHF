using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FThread
    {
        public void testthread ()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(WriteChar));
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            thread.Start('*');
            for (int i = 0; i < 80; i++)
            {
                Console.Write("-");
                Thread.Sleep(70);
            }

        }

        private static void WriteChar(object arg)
        {
            char item = (char)arg;
            for (int i = 0; i < 80; i++)
            {
                Console.Write(item);
                Thread.Sleep(70);
            }

        }
    }
}
