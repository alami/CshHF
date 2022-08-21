using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class F301ReviewTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> tasksList = new LinkedList<Task>();
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasksList;
        }
        protected override void QueueTask(Task task)
        {
            Console.WriteLine($"    [QueueTask] Задача #{task.Id} поставлена в очередь..");
            tasksList.AddLast(task);
            ThreadPool.QueueUserWorkItem(ExecuteTasks, null);
            //ExecuteTasks(null);
        }
        //вызывается методами ожидания Wait,WaitAll
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine($"    [TryExecuteTaskInline] Попытка выполнить задачу #{task.Id} синхр..");
            lock (tasksList)
            {
                tasksList.Remove(task);
            }
            return base.TryExecuteTask(task);
        }
        //вызывается при отмене выполнения задач
        protected override bool TryDequeue(Task task)
        {
            Console.WriteLine($"    [TryDequeue] Попытка удалить задачу #{task.Id} из очереди..");
            bool result = false;
            lock (tasksList)
            {
                result = tasksList.Remove(task);
            }
            return result;
        }
        private void ExecuteTasks(object _)
        {
            while (true)
            {
                //Thread.Sleep(2000);
                Task task = null;
                lock (tasksList)
                {
                    if (tasksList.Count == 0) break;
                    task = tasksList.First.Value;
                    tasksList.RemoveFirst();
                }
                if (task != null) break;
                base.TryExecuteTask(task);
            }
        }
    }
}
