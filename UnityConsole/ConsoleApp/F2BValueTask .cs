using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class F2BValueTask
    {
        public void test()
        {
            int res = Sum(1, 0).Result;
            Console.WriteLine(res);
            Console.ReadKey(); 
        }

        private static ValueTask<int> Sum(int v1, int v2)
        {
            if (v1 == 0)
            return new ValueTask<int>(v2);
            else if (v2 == 0)
                return new ValueTask<int>(v1);
            else
                return new ValueTask<int>(Task.Run(() => {
                    return v1 + v2;
            }));
        }
    }
}
