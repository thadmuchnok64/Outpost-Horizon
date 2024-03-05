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
        GameManagerScript.instance.RodActivation();
        int count = _packet.ReadInt();
        List<int> ints = new List<int>();
        for (int i = 0; i < count; i++)
            ints.Add(_packet.ReadInt());
        RodBehaviour.rods.Clear();
        RodBehaviour.rodstoinsert = "";
        for (int i = 0; i < count; i++)
            RodBehaviour.rods.Add(ints[i]);
        if (RodBehaviour.rods.Count > 0)
            RodBehaviour.instance.RodsTUpdate();
        else if (RodBehaviour.rods.Count == 0)
            RodBehaviour.instance.RodsComplete();
    }

    public static void CranePosition(Packet _packet)
    {
        Vector2 vector = new Vector2(_packet.ReadInt(),_packet.ReadInt());
        vector = vector / 1000;
        try
        {
            TheClaw.instance.MoveClawToLocation(vector);
        }
        catch
        {
            // Claw probably isn't awake
        }
    }
    public static void ReadMessage(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Message recieved from server: {_msg}");
        switch ( _msg )
        {
            case "RodOnFloor!":
                RodBehaviour.instance.ShowError();
                break;
            
        }
    }
}
