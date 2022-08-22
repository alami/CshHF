using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class F308SynchronizationContextTaskScheduler : TaskScheduler
    {
        private readonly SynchronizationContext  synchronizationContext;
        public F308SynchronizationContextTaskScheduler()
            :this(SynchronizationContext.Current) {}
        public F308SynchronizationContextTaskScheduler
            (SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
        protected override void QueueTask(Task task)
        {
            synchronizationContext.Post(_ => base.TryExecuteTask(task), null);
        }
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return synchronizationContext == SynchronizationContext.Current
                && base.TryExecuteTask(task);
        }
    }
}
