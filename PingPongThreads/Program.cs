using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using PingPong.Library;
using Timer = System.Threading.Timer;

namespace PingPongThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintWelcome();
            String commandStr;
            do
            {
                Console.WriteLine(@"
                    Please enter one of following commands:
                    
                    0 - Ping-pong implemented via standalone threads
                    1 - Ping-pong implemented via thread pool

                    exit - exit the program
                ");

                commandStr = Console.ReadLine();
                
                IPingPongRunner pingPong = null;
                PingPongImplementation type;

                if ( Enum.TryParse(commandStr, true, out type) )
                {
                    pingPong = PingPongFactory.GetPingPongRunner(type);
                }
                
                if (pingPong != null)
                {
                    var timer = new System.Timers.Timer { Interval = 5000, AutoReset = false, Enabled = true };
                    timer.Elapsed += (sender, e) => pingPong.Stop(sender, e);

                    pingPong.Start();
                }
            } while ( !"exit".Equals(commandStr, StringComparison.InvariantCultureIgnoreCase) );
        }

        public static void PrintWelcome()
        {
            Console.WriteLine(@"
                  _____  _____ __   _  ______       _____   _____  __   _  ______
                 |_____]   |   | \  | |  ____      |_____] |     | | \  | |  ____
                 |       __|__ |  \_| |_____|      |       |_____| |  \_| |_____|
                                                                                 
            ");
        }
    }
}
