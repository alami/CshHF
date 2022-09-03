using System;
using System.Threading;
using System.Threading.Tasks;

namespace F751Report
{
    internal class Operation
    {
        internal Task<int> SumNumbersAsync(int[] numbers, CancellationToken token = default,
            IProgress<int> progress=null)
        {
            return Task.Run(() => SumNumbers(numbers, token, progress), token);
        }

        private int SumNumbers(int[] numbers, CancellationToken token, IProgress<int> progress)
        {
            token.ThrowIfCancellationRequested();
            int sum = 0;
            for (int i = 0,j=1; i < numbers.Length; i++,j++)
            {
                Thread.Sleep(1000);
                sum += numbers[i];
                token.ThrowIfCancellationRequested();
                progress?.Report((j*100)/numbers.Length);
            }
            return sum;
        }
    }
}