using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F705Exceptions
    {
        public static async Task test()
        {
            Console.WriteLine($"Метод Майн начал свою работу.");

            try
            {
                await OperationAsync();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Исключение - {ex.GetType()}");
                Console.WriteLine($"Сообщение - {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine($"Метод Майн закончил свою работу.");
            Console.ReadKey();
        }

        private static async Task OperationAsync()
        {
            Console.WriteLine($"Код метода OperationAsync до ошибки");

            await Task.Run(() => throw new Exception("Ошибка в методе OperationAsync"));

            Console.WriteLine($"Код метода OperationAsync после ошибки");
        }
    }
}
