using System;
using System.Threading;

namespace PingPong.Library
{
    public class PingPongViaThreadPool : BasePingPong
    {
        private CountdownEvent _finished;

        protected override void PingPongWorker(Object o)
        {
            try
            {
                base.PingPongWorker(o);
            }
            finally
            {
                _finished.Signal();
            }
        }

        protected override void StartPingPong()
        {

            using (_finished = new CountdownEvent(2))
            {
                ThreadPool.QueueUserWorkItem(PingPongWorker, "- - - >   PING");
                ThreadPool.QueueUserWorkItem(PingPongWorker, "< - - -   PONG");

                AutoResetEvent.Set();
                _finished.Wait();
            }
        }

    }
}