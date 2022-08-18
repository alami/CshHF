using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F15AsyncProg
    {
        public void F15AsyncProgTest()
        {
            int number = 13;
            long result = CalculatorFactorial(number);
            Console.WriteLine($"Result - {result}");
            //CalculatorFactorialAsync(number).ContinueWith(t => Console.WriteLine($"Result - {t.Result}"));
            while (true)
            {
                Console.Write("*");
                Thread.Sleep(300);
            }
        }
        /*private static async Task<long> CalculatorFactorialAsync(int number)
        {
            var a = 0;
            await CalculatorFactorial(number);
            return 0;
        }*/
        private static long CalculatorFactorial(int number)
        {
            Thread.Sleep(500);
            if (number == 1) return number;
            else return CalculatorFactorial(number-1)*number;
        }
    }
}
