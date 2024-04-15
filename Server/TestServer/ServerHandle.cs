using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            if (_username == Server.usedClientName)
            {
                Server.clients.Remove(_fromClient);
            }
            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}!\n" +
                $"this user is using {_username}");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }

            Server.clients[_fromClient].username = _username;

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            if (_username == "Unreal")
            {
                try
                {
                    if (Server.clients[_clientToSendTo].username == "Unity")
                    {
                        ServerSend.VerifyUnityUserHasLoggedIn(_fromClient);
                    }
                }
                catch { }
            }
            else if (_username == "Unity")
            {
                try
                {
                    if (Server.clients[_clientToSendTo].username == "Unreal")
                    {
                        ServerSend.VerifyUnityUserHasLoggedIn(_clientToSendTo);
                    }
                }
                catch { }
            }

        }

        public static void TestRecieved(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _message = _packet.ReadString();
            Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} says {_message}");
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;
            ServerSend.TestMessage(_clientToSendTo, _message);
        }

        public static void BreakRod(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int intsToRead = _packet.ReadInt();
            List<int> list = new List<int>();
            for (int i = 0; i < intsToRead; i++)
            {
                list.Add(_packet.ReadInt());
            }
            //Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} says {_message}");
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            if (list.Count == 0)
            {
                Console.WriteLine($"Unreal (ID: {_fromClient}) reports to Unity (ID: {_clientToSendTo}) that no rods are broken!");
            }
            else
            {
                string message = $"Unreal (ID: {_fromClient}) reports to Unity (ID: {_clientToSendTo}) that rods ";
                foreach (int i in list)
                {
                    message = message + i + ", ";
                }
                message = message + "are broken!";
                Console.WriteLine(message);

            }

            ServerSend.SendBrokenRodInfoToUnity(_clientToSendTo, list);
        }
        public static void ClawControl(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int control = _packet.ReadInt();
            //Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} says {_message}");
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.ClawControl(_clientToSendTo, control);
        }

        public static void ClawPostionInfo(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int count = _packet.ReadInt();

            List<int> list = new List<int>();

            list.Add(_packet.ReadInt());
            list.Add(_packet.ReadInt());
            list.Add(_packet.ReadInt());

            list.Add(_packet.ReadInt());
            list.Add(_packet.ReadInt());
            list.Add(_packet.ReadInt());

            int i = _packet.ReadInt();
            list.Add(i);

            for (int x = 0; x < i; x++)
            {
                list.Add(_packet.ReadInt());

                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());

                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
            }

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.ClawPositionInfo(_clientToSendTo, list);
        }
        public static void DisconnectFromTCP(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} has been disconnected.");
            Server.clients[_clientIdCheck].username = "Pizza"; // Server would still recognize unity user if this isn't set to something else
            Server.clients[_clientIdCheck].tcp.socket = null;
        }

        public static void RoomEntry(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string code = _packet.ReadString();
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.SendRoomInfoToUnity(_clientToSendTo, code);
        }

        public static void MaintainConnection(int _fromClient, Packet _packet)
        {
            Server.clients[_fromClient].timeSinceLastPing = 0;
        }

        public static void WorldGeometryPositions(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int length = _packet.ReadInt();
            int code = _packet.ReadInt();
            List<int> list = new List<int>();

            list.Add(code);

            // How many bits of geometry there are
            int i = _packet.ReadInt();
            list.Add(i);

            for (int x = 0; x < i; x++)
            {
                //position
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
                //rotation
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
                list.Add(_packet.ReadInt());
            }

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.WorldGeometryPositionInfo(_clientToSendTo, list);
        }



        public static void DoorUnlock(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int code = _packet.ReadInt();


            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.DoorUnlock(_clientToSendTo, code);
        }

        public static void ElevatorSend(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int code = _packet.ReadInt();


            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.ElevatorSend(_clientToSendTo, code);
        }

        public static void PlayerTransformTracking(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();

            List<int> list = new List<int>();
            //length
            list.Add((int)_packet.ReadInt());
            //player pos
            list.Add((int)_packet.ReadInt());
            list.Add((int)_packet.ReadInt());
            list.Add((int)_packet.ReadInt());
            //player rot
            list.Add((int)_packet.ReadInt());
            list.Add((int)_packet.ReadInt());
            list.Add((int)_packet.ReadInt());


            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.PlayerTransformTracking(_clientToSendTo, list);
        }


        public static void DoorAdminRequest(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int size = _packet.ReadInt();
            int code = _packet.ReadInt();

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.DoorAdminUnlock(_clientToSendTo, code);
        }
        public static void GrantAdmin(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int code = _packet.ReadInt();

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.GrantAdmin(_clientToSendTo, code);
        }

        public static void ElevatorMove(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int length = _packet.ReadInt();
            List<int> list = new List<int>();
            //length
            list.Add((int)_packet.ReadInt());
            //Elevator y axis
            list.Add((int)_packet.ReadInt());

            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.TrackElevator(_clientToSendTo, list);
        }

        public static void WaypointMessage(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int id = _packet.ReadInt();
            string message = _packet.ReadString();
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.WaypointMessage(_clientToSendTo, id,message);
        }
    }
}
