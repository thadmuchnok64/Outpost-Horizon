using System;
using System.Threading;

namespace TestServer
{
    class Program
    {

        private static bool isRunning = false;
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning= true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Server.Start(2, 80);
            Console.WriteLine("Thad says hi");
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
                }
            }
        }
    }
}