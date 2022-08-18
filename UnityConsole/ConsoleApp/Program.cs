using ConsoleApp;
using System.Net.Security;

class Program
{
    public static IEnumerable<int> Countdown(int start, int end)
    {
        for (int i = start; i>=end; i--)
        {
            if (i == end+2) yield break;
            yield return i;
        }
    }
    static void Main(string[] argv)
    {
        Console.WriteLine("Test start!");
        //(new Fdelegate()).fdelegate();
        //(new FThread()).testthread();
        //(new FThreadPool()).testthreads();
        //(new FThreadPoolWorker()).testthreadPW();
        //(new FThreadPoolWorker2()).testthreadPW();
        (new F15AsyncProg()).F15AsyncProgTest();
    }
}
