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
    public static void TestRecieved()
    {
        using (Packet _packet = new Packet((int)ClientPackets.testRecieved))
        {
            _packet.Write(TestClient.instance.myId);
            _packet.Write(TestClient.instance.message);

            SendTCPData(_packet);
        }
    }
    #endregion
}
