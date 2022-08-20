using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F2CValueTask
    {
        public void test()
        {
            int salary = 10000;
            ValueTask<double> valueTask = GetIndexing(salary);
            
            while (!valueTask.IsCompleted)
            {
                Console.Write('*');
                Thread.Sleep(200);
            }
            Task<double> task = valueTask.AsTask();
            task.ContinueWith((t) => Console.WriteLine($"\nИндексация зп {salary} = {t.Result}%"));

            Console.ReadKey(); 
        }
        private ValueTask<double> GetIndexing(int salary)
        {
            Thread.Sleep(500);
            if (salary <= 0)
                return new ValueTask<double>(0);
            else if (salary < 5000)
                return new ValueTask<double>(0);
            else if (salary == 5000)
                return new ValueTask<double>(0.1);
            else
                return new ValueTask<double>(Task.Run( ()=>
                {
                    double index = 0.0;
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(500);
                        index+=0.1;
                    }
                    return index;
                }));
        }
    }
}
