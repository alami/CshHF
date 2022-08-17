using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Fdelegate
    {
        public void fdelegate()
        {
            //        foreach (int i in Countdown(5,1)) { Console.WriteLine(i); }
            //                Console.WriteLine("\nHello, World!");
            //ex1-delegate
            Message mes;            // 2. Создаем переменную делегата
            mes = Hello;            // 3. Присваиваем этой переменной адрес метода
            mes();                  // 4. Вызываем метод
            void Hello() => Console.WriteLine("Hello METANIT.COM");
            //mes -= Hello;  mes(); //Unhandled exception. System.NullReferenceException
            //ex2-delegate wt class
            mes += Welcome.Print;
            mes += new Hello().Display;
            mes -= Hello;
            mes(); // Welcome
                   //ex3-delegate wt парам
            Operation op = Add;
            Console.WriteLine(op?.Invoke(3, 4));
            int Add(int x, int y) => x + y;
        }
    }
    delegate int Operation(int x, int y);
    delegate void Message();
    class Welcome
    {
        public static void Print() => Console.WriteLine("Welcome");
    }
    class Hello
    {
        public void Display() => Console.WriteLine("Привет");
    }

}
