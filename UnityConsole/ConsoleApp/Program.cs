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
        //(new Fdelegate()).fdelegate();
        //----------------------------Async ch 1 
        //(new FThread()).testthread();
        //(new FThreadPool()).testthreads();
        //(new FThreadPoolWorker()).testthreadPW();
        //(new FThreadPoolWorker2()).testthreadPW();

        //(new F15AsyncProg()).F15AsyncProgTest();

        //----------------------------Async ch 2 
        //(new F21ThreadOutput()).test();  // 4 vars of Task running without Result
        //(new F22ReturnResults()).test();    // 3 vars ...wt Result
        //(new F23TaskOptions()).test();    // влиять на задачи чрз TASK CREATEION OPTIONS 
        //(new F24TaskStatus()).test();
        //(new F25TaskWait()).test();
        //(new F26Closure()).test();

        //(new F27ContinueWith()).test();
        //(new F28ContinueWith()).test();
        //(new F29ContinueWithChain()).test();

        //(new F29FactoryTask()).test();

        //(new F2AValueTask()).test();
        //(new F2BValueTask()).test();
        //(new F2CValueTask()).test(); 

        //(new F2DAsyncProg()).F15AsyncProgTest();

        //(new F301Program()).test();
        //(new F302StandartTaskScheduler()).test();
        //(new F305ProgramPriorityTS()).test();

        //F310NestedTasks.test();

        //F401Async.test();
        //F403AsyncReturn.test();

        //F701Exceptions.test();
        //F702Exceptions.test();
        //F703ExceptionsNested.test();
        //F704ExceptionsWithoutParent.test();
        //F705Exceptions.test();

        //F721CancellationToken.test();
        F722LinkedToken.test();
    }
}
