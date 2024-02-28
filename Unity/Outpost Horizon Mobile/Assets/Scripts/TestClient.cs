using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Net.Http;

public class TestClient : MonoBehaviour
{
    public static TestClient instance;
    public static int dataBufferSize = 4096;
    public string ip = "127.0.0.1";
    public string username = "Unity";
    public string message;
    public int port = 80;
    public int myId = 0;
    public TCP tcp;

    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists!!!");
                Destroy(this);
            }
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    public void ConnectToServer()
    {
        InitializeClientData();
        tcp.Connect();
    }
    public void DisconnectFromServer()
    {
        tcp.Disconnect();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private Packet recievedData;
        private byte[] recieveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            recieveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }
        public void Disconnect()
        {
            stream.Close();
            socket.Close();
        }
        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();

            recievedData = new Packet();

            stream.BeginRead(recieveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    // Disconnect
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(recieveBuffer, _data, _byteLength);

                recievedData.Reset(HandleData(_data));

                stream.BeginRead(recieveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {

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
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
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
            return false;
        }

		public void SendData(Packet _packet)
		{
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(),null, null);
                }
            } 
            catch(Exception e)
            {
                Debug.Log($"Error sending data to server via TCP: {e}");
            }

		}
	}
        private void InitializeClientData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ServerPackets.welcome, ClientHandle.Welcome},
                {(int)ServerPackets.breakrod, ClientHandle.GetInfoOnBrokenRods},
                {(int)ServerPackets.cranePosition,ClientHandle.CranePosition},

            };
        Debug.Log("Initializing client data...");
        }
        public void SendMessageToServer()
        {
        packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ServerPackets.test, ClientHandle.Dataset }
            };
        Debug.Log("Sending Message To Server...");
        }


}
