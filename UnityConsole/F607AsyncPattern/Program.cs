using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F607AsyncPattern
{
    internal class Program
    {
        private static async Task Main()
        {
            try
            {
                string result = await APM_TAP2();
                Console.WriteLine($"File Content: {result}");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static Task<string> APM_TAP1()
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>(); 
            using (FileStream fs = new FileStream("text.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite,4096,true))
            {
                byte[] array = new byte[1024];
                fs.BeginRead(array, 0, array.Length, (IAsyncResult) => { 
                    try
                    {
                        int bytes = fs.EndRead(IAsyncResult);
                        Console.WriteLine($"Bytes read - {bytes}");
                        tcs.TrySetResult(Encoding.UTF8.GetString(array));
                    } catch (OperationCanceledException ex)
                    {
                        tcs.TrySetCanceled(ex.CancellationToken);
                    } catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }, null);
            }
            return tcs.Task;
        }
        private static Task<string> APM_TAP2()
        {
            TaskFactory taskFactory = new TaskFactory();
            using (FileStream fs = new FileStream("text.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, true))
            {
                byte[] array = new byte[1024];
                return taskFactory.FromAsync(fs.BeginRead, (IAsyncResult) => {
                    int bytes = fs.EndRead(IAsyncResult);
                    Console.WriteLine($"Bytes read - {bytes}");
                    return Encoding.UTF8.GetString(array);
                }, array, 0, array.Length, null);
            }
        }
    }
}
