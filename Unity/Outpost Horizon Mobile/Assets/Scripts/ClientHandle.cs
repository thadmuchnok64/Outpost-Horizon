using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message recieved from server: {_msg}");
        TestClient.instance.myId = _myId;
        ClientSend.WelcomeRecieved();
    }
}
