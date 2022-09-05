using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F808Concurrent
    {
        public static void test()
        {

            BlockingCollection<int>[] collections = new BlockingCollection<int>[3];
            
            collections[0] = new BlockingCollection<int>(10);
            collections[1] = new BlockingCollection<int>(3);
            collections[2] = new BlockingCollection<int>(2);

            int counter = 0;

            for (int i = 0; i < 20; i++)
                if (BlockingCollection<int>.TryAddToAny(collections,i) == -1) 
                    counter++; 

            Console.WriteLine($"Кол-во невыполненных добавлений = {counter}");

            counter = 0;

            while (BlockingCollection<int>.TryTakeFromAny(collections, out int item) != -1)
                    counter++;

            Console.WriteLine($"Кол-во извлеченных элементов = {counter}");

            Console.ReadKey();
        }
    }        
}
