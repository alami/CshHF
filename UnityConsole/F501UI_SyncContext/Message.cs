using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F501UI_SyncContext
{
    internal class Message
    {
        public SendOrPostCallback Callback { get; set; }
        public object State { get; set; }
        public Message(SendOrPostCallback callback, object state)
        {
            Callback = callback;
            State = state;
        }
    }
}
