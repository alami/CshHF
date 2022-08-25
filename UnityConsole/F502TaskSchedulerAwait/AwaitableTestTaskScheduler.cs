namespace F50TaskSchedulerAwait
{
    internal class AwaitableTestTaskScheduler : TaskScheduler
    {
        private int counter = 0;
        string[] names = { "1ЙПОТОК", "2ЙПОТОК", "3ЙПОТОК", "4ЙПОТОК" };
        protected override void QueueTask(Task task)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"QueueTask сработал для задачи - {task.Id}");
            Console.ResetColor();
            new Thread(_=> base.TryExecuteTask(task)) 
                    { IsBackground=true, Name = names[counter++] }
                .Start();
            //ThreadPool.QueueUserWorkItem(_=> base.TryExecuteTask(task));
        }
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"TryExecuteTaskInline сработал для задачи - {task.Id}");
            Console.ResetColor();
            return base.TryExecuteTask(task);
        }
        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            return null;
        }
    }
}