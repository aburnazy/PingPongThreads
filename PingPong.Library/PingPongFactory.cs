namespace PingPong.Library
{
    public static class PingPongFactory
    {
        public static IPingPongRunner GetPingPongRunner(PingPongImplementation type)
        {
            IPingPongRunner runner = null;

            switch (type)
            {
                case PingPongImplementation.StandaloneThreads:
                    runner = new PingPongViaStandaloneThreads();
                    break;
                case PingPongImplementation.ThreadPoolBased:
                    runner = new PingPongViaThreadPool();
                    break;
            }

            return runner;
        }
    }

    public enum PingPongImplementation
    {
        StandaloneThreads, ThreadPoolBased
    }
}