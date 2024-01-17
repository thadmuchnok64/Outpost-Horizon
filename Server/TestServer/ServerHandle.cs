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
		}
	}
}
