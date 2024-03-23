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
        Vector3 tempvec = new Vector3(_packet.ReadInt(),_packet.ReadInt(),_packet.ReadInt());
        Vector3 vector = new Vector3(-tempvec.x, tempvec.z, tempvec.y);
        vector = vector / 100000;
        Vector3 craneEuler = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
        craneEuler = new Vector3(craneEuler.x, craneEuler.z, craneEuler.y);
        CraneHandler.instance.SetCraneOrientation(vector, craneEuler);
        tempvec = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
        vector = new Vector3(-tempvec.x, tempvec.z, tempvec.y);
        vector = vector / 100;
        CraneHandler.instance.SetPlayerPosition(vector);

        int itr = _packet.ReadInt();
            //TheClaw.instance.MoveClawToLocation(vector);
            for (int i = 0; i < itr; i++)
            {
                int index = _packet.ReadInt();
                var posx = _packet.ReadInt();
                var posy = _packet.ReadInt();
                var posz = _packet.ReadInt();

                Vector3 pos = new Vector3(-posx, posz, posy)/100000;
                Vector3 euler = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
            euler = new Vector3(euler.x,euler.z, euler.y);
                CraneHandler.instance.SetOrientationOfShippingContainer(index, pos, euler);


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
            case string anotif when anotif.StartsWith("aware"):
                GameManagerScript.instance.notif = anotif;
                GameManagerScript.instance.NotifAnnounce();
                break;
        }
    }

    public static void RoomEntry( Packet _packet)
    {
        string msg = _packet.ReadString();

        switch( msg )
        {
            case "awareGenerator":
                // do stuff here
                break;
            case "awareLockpick":
                // do stuff here
                break;
            case "awareCrane":
                // do stuff here
                break;
            default:
                break;

        }
    }
}
