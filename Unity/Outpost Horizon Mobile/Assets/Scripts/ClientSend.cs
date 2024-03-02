using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        TestClient.instance.tcp.SendData(_packet);
    }

	#region
    public static void WelcomeRecieved()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(TestClient.instance.myId);
            _packet.Write(TestClient.instance.username);

            SendTCPData(_packet);
        }
    }
    public static void DisconnectTCP()
    {
        using (Packet _packet = new Packet((int)ClientPackets.disconnect))
        {
            _packet.Write(TestClient.instance.myId);
            _packet.Write(TestClient.instance.username);


            SendTCPData(_packet);
        }
    }

    // Creates a packet, with the message in the text box;
    public static void SendTestMessage(string message)
    {
        using (Packet _packet = new Packet((int)ClientPackets.testRecieved))
        {
            _packet.Write(TestClient.instance.myId);
            _packet.Write(message);

            SendTCPData(_packet);
        }
    }
    public static void SendNumber(int sensitivity)
    {
        using (Packet _packet = new Packet((int)ClientPackets.clawControl))
        {
            _packet.Write(TestClient.instance.myId);
            _packet.Write(sensitivity);
            SendTCPData(_packet);
        }
    }
    #endregion
}
