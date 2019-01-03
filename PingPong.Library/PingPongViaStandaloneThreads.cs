using System.Threading;

namespace PingPong.Library
{
    public class PingPongViaStandaloneThreads : BasePingPong
    {
        protected override void StartPingPong()
        {
            AutoResetEvent.Set();
            Thread pingThread = new Thread(PingPongWorker);
            Thread pongThread = new Thread(PingPongWorker);

            pingThread.Start("---> Ping");
            pongThread.Start("<--- Pong");

            pingThread.Join();
            pongThread.Join();
        }
    }

}
