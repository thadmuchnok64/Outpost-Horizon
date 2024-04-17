using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TestServer
{
     class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public TCP tcp;
        public string username;
        public int timeSinceLastPing = 0;

        public Client(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private Packet recievedData;
            private byte[] recieveBuffer;
            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();
                recievedData = new Packet();
                recieveBuffer = new byte[dataBufferSize];

                stream.BeginRead(recieveBuffer, 0, dataBufferSize, RecieveCallback, null);

                ServerSend.Welcome(id, "Welcome to the server!");

            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(),0,_packet.Length(),null,null);
                    }
                }
                catch
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP: ");
                }
            }

            private void RecieveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        Server.clients[id].Disconnect();
                        return;
                    }
                    byte[] _data = new byte[_byteLength];
                    Array.Copy(recieveBuffer, _data, _byteLength);
                    stream.BeginRead(recieveBuffer,0,dataBufferSize,RecieveCallback,null);

                    recievedData.Reset(HandleData(_data));
                } catch (Exception e)
                {
                    Console.WriteLine($"Error receiving TCP data: {e}");
                    Server.clients[id].Disconnect();
                }
            }

			private bool HandleData(byte[] _data)
			{
				int _packetLength = 0;

				recievedData.SetBytes(_data);
				if (recievedData.UnreadLength() >= 4)
				{
					_packetLength = recievedData.ReadInt();
					if (_packetLength <= 0)
					{
						return true;
					}
				}
				while (_packetLength > 0 && _packetLength <= recievedData.UnreadLength())
				{
					byte[] _packetBytes = recievedData.ReadBytes(_packetLength);
					ThreadManager.ExecuteOnMainThread(() =>
					{
						using (Packet _packet = new Packet(_packetBytes))
						{
                            try
                            {
                                int _packetId = _packet.ReadInt();
                                Server.packetHandlers[_packetId](id, _packet);
                            }
                            catch (Exception e)
                            {
                                if(e is KeyNotFoundException)
                                {
                                    Console.WriteLine("Packet loss???");
                                } else
                                {
                                    Console.WriteLine($"{e.GetType()}");

                                }
                            }
                            //Console.WriteLine(_packetId);
						}
					});
					_packetLength = 0;
					if (recievedData.UnreadLength() >= 4)
					{
						_packetLength = recievedData.ReadInt();
						if (_packetLength <= 0)
						{
							return true;
						}
					}

				}
				if (_packetLength <= 1)
				{
					return true;
				}
				return true;
			}

            public void Disconnect()
            {
               
                socket.Close();
                stream = null;
                recievedData = null;
                recieveBuffer = null;
                socket = null;
            }

		}
        public void Disconnect()
        {
            Console.WriteLine($"{username} Player {tcp.socket.Client.RemoteEndPoint} has been disconnected.");
            tcp.Disconnect();
        }
    }
}
