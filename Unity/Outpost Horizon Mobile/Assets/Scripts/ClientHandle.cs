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
    public static void Dataset(Packet _packet)
    {
        TestClient.instance.myId = 0;
        //ClientSend.TestRecieved();
    }

    public static void GetInfoOnBrokenRods(Packet _packet)
    {
        int count = _packet.ReadInt();
        List<int> ints = new List<int>();
        for(int i = 0; i < count; i++)  
            ints.Add(_packet.ReadInt());
        RodBehaviour.rods.Clear();
        RodBehaviour.rodstoinsert = "";
        for (int i = 0; i < count; i++)
            RodBehaviour.rods.Add(ints[i]);
        if(RodBehaviour.rods.Count > 0)
            RodBehaviour.instance.RodsTUpdate();
        else if (RodBehaviour.rods.Count == 0)
            RodBehaviour.instance.RodsComplete();

    }
}
