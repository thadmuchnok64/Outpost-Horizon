using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    internal class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int,Client> clients = new Dictionary<int, Client>();
        private static TcpListener tcpListener;
        public static void Start(int _maxPlayer, int _port)
        {
            MaxPlayers = _maxPlayer;
            Port = _port;

            Console.WriteLine("Starting server....");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void TCPConnectCallBack(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient( _result );
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");
            for (int i = 0; i < MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }
            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server Full!");
        }

        private static void InitializeServerData()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                    clients.Add(i, new Client(i));
            }
        }
    }
}
