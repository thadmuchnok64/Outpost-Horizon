using System;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            Server.Start(2, 80);
            Console.WriteLine("Hello there");
            Console.ReadKey();
        }
    }
}