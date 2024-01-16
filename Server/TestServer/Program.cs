using System;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            Server.Start(2, 80);
            Console.WriteLine("Thad says 'Hi'");
			Console.WriteLine("Pressing any key will terminate the server");
			Console.WriteLine("--------------------------------------------");

			Console.ReadKey();
        }
    }
}