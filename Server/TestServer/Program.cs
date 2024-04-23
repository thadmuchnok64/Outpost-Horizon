using System;
using System.Net;
using System.Threading;

namespace TestServer
{
    class Program
    {

        private static bool isRunning = false;
        static void Main(string[] args)
        {
            Console.Title = "Outpost Horizon Terminal";
            isRunning= true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Server.Start(2, 9005);
            //Server.Start(2, 80);
            ClearConsole();

		}

        static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine($"Server started on port {Server.Port}.");
            Console.WriteLine($"Server IP: {Server.ip}");
            Console.WriteLine("Thad and jj say hi");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Welcome to Outpost Horizon,");
            Console.WriteLine("Bringing you a Better Future, Tomorrow");
            Console.WriteLine("------------------------------------------------------");
        }

		private static void MainThread()
        {
                Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;
            while (isRunning)
            {
                while(_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);
                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}