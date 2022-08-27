using System.Reflection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace F503AsyncVoid
{
    internal class TestSyncContext : SynchronizationContext
    {
        public override void OperationCompleted()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"OperationCompleted");
            Console.ResetColor();
        }
        public override void OperationStarted()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"OperationStarted");
            Console.ResetColor();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            try
            {
                d.Invoke(state);
            } catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }
    }
}