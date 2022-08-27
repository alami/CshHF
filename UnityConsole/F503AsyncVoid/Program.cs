﻿using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace F503AsyncVoid
{
    internal class Program
    {
        static Program()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSyncContext());
        }
        private static void Main()
        {
            MethodAsync();
            Console.ReadKey();
        }

        private static async void MethodAsync()
        {
            Console.WriteLine($"Код до await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => Console.WriteLine($"Задача выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}"));
            //await Task.Run(() => throw new AsyncVoidException ("Ошибка в асинхронном коде"));
            //throw new AsyncVoidException("Ошибка в асинхронном коде");
            Console.WriteLine($"Код после await выполнился в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
    internal class AsyncVoidException : Exception
    {
        public AsyncVoidException(string message) : base(message) { }
    }
}
