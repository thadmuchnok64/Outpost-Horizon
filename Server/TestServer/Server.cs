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
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;
        public static string usedClientName;
        //CHANGE 1 TO 0 ON RASPI!
        public static IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
        //public static IPAddress ip = IPAddress.Any;


        private static TcpListener tcpListener;
        public static void Start(int _maxPlayer, int _port)
        {
            MaxPlayers = _maxPlayer;
            Port = _port;
            Console.WriteLine("Starting server....");
            InitializeServerData();
           

			tcpListener = new TcpListener(ip, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

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

        public static void IncrementClients(int maxtime)
        {
            for(int i = 0; i < MaxPlayers; i++)
            {
                if (clients[i].tcp.socket != null)
                {
                    clients[i].timeSinceLastPing += Constants.MS_PER_TICK;
                    if (clients[i].timeSinceLastPing > maxtime)
                    {
                        clients[i].Disconnect(); //Disconnect if time out
                    }
                }
            }
        }

        private static void InitializeServerData()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                    clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ClientPackets.welcomeReceived,ServerHandle.WelcomeReceived},
                {(int)ClientPackets.testRecieved,ServerHandle.TestRecieved},
                {(int)ClientPackets.breakrod,ServerHandle.BreakRod},
                {(int)ClientPackets.clawControl,ServerHandle.ClawControl},
                {(int)ClientPackets.clawPosition,ServerHandle.ClawPostionInfo},
                {(int)ClientPackets.disconnect, ServerHandle.DisconnectFromTCP },
                {(int)ClientPackets.roomEntry, ServerHandle.RoomEntry },
                {(int)ClientPackets.maintainConnection,ServerHandle.MaintainConnection},
                {(int)ClientPackets.WorldGeometryPositionInfo,ServerHandle.WorldGeometryPositions },
                {(int)ClientPackets.DoorUnlock,ServerHandle.DoorUnlock },
                {(int)ClientPackets.PlayerTransformTrack,ServerHandle.PlayerTransformTracking },
                {(int)ClientPackets.ElevatorSend,ServerHandle.ElevatorSend },
                {(int)ClientPackets.RequestAdmin,ServerHandle.DoorAdminRequest },
                {(int)ClientPackets.GrantAdmin,ServerHandle.GrantAdmin },
                {(int)ClientPackets.TrackElevator,ServerHandle.ElevatorMove },
                {(int)ClientPackets.WaypointMessage,ServerHandle.WaypointMessage },

            };
            Console.WriteLine("Initialized packets");
        }
    }
}
