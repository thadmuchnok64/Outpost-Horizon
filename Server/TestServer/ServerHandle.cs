﻿using System;
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

			Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}!\n" +
				$"this user is using {_username}");
			if (_fromClient != _clientIdCheck)
			{
				Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
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
			ServerSend.TestMessage( _clientToSendTo, _message );
		}

		public static void BreakRod(int _fromClient, Packet _packet)
		{
            int _clientIdCheck = _packet.ReadInt();
            int intsToRead = _packet.ReadInt();
			List<int> list = new List<int>();
			for(int i = 0; i < intsToRead; i++)
			{
				list.Add(_packet.ReadInt());
			}
            //Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} says {_message}");
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

			if(list.Count == 0)
			{
				Console.WriteLine($"Unreal (ID: {_fromClient}) reports to Unity (ID: {_clientToSendTo}) that no rods are broken!");
			}
			else
			{
				string message = $"Unreal (ID: {_fromClient}) reports to Unity (ID: {_clientToSendTo}) that rods ";
				foreach(int i in list)
				{
					message = message + i + ", ";
				}
				message = message + "are broken!";
                Console.WriteLine(message);

            }

            ServerSend.SendBrokenRodInfoToUnity(_clientToSendTo, list);
        }
        public static void Numparse(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            int floatToRead = _packet.ReadInt();
            List<float> list = new List<float>();
            for (int i = 0; i < floatToRead; i++)
            {
                list.Add(_packet.ReadFloat());
            }
            //Console.WriteLine($" Player {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} says {_message}");
            int _clientToSendTo = 1;
            if (_clientIdCheck == 1)
                _clientToSendTo = 0;

            ServerSend.ReadNum(_clientToSendTo, list);
        }

    }
}
