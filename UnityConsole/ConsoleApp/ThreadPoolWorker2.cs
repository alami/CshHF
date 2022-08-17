namespace ConsoleApp
{
    public class ThreadPoolWorker2<TResult>
    {
        private readonly Func<object, TResult> func;
        private TResult result;
        public ThreadPoolWorker2(Func<object, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }
        public bool Success { get; private set; } = false;
        public bool Completed { get; private set; } = false;
        public Exception Exception { get; private set; } = null;
        public TResult Result { 
            get
            {
                while (Completed == false) { Thread.Sleep(150); }
                return Success==true && Exception==null ? result : throw Exception;
            }
        }
        public void Start (object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
        }

        private void ThreadExecution(object? state)
        {
            try
            {
                result = func.Invoke(state);
                Success = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                Success = false;
            }
            finally
            {
                Completed = true;
            }
        }

        public void Wait()
        {
            while (Completed == false) { Thread.Sleep(150); }
            if (Exception != null) { throw Exception; }
        }
    }
} 