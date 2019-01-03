using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PingPong.Library
{
    public interface IPingPongRunner
    {
        void Stop(object sender, ElapsedEventArgs e);
        void Start();
    }

    public abstract class BasePingPong: IPingPongRunner
    {
        protected Int32 StopThreads = 0;
        protected Int32 TotalPingPongIterations = 0;

        protected readonly AutoResetEvent AutoResetEvent = new AutoResetEvent(false);

        public virtual void Stop(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"----------- Timer expired: stop the threads --------------");
            Volatile.Write(ref StopThreads, 1);
        }

        public virtual void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            StartPingPong();

            stopwatch.Stop();

            Console.WriteLine($@"
                    Total time      :   {stopwatch.ElapsedMilliseconds}ms               
                    Total iterations:   {TotalPingPongIterations}
            ");
        }

        protected virtual void PingPongWorker(Object o)
        {
            AutoResetEvent.WaitOne();

            while (Volatile.Read(ref StopThreads) == 0)
            {
                Console.WriteLine(o);

                Interlocked.Increment(ref TotalPingPongIterations);

                AutoResetEvent.Set();
                AutoResetEvent.WaitOne();
            }

            Console.WriteLine($"----------- Stopped {o} thread --------------");
            AutoResetEvent.Set();

        }

        protected abstract void StartPingPong();
    }

}
