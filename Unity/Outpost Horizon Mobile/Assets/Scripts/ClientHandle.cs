using System;
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
        /*
        tempvec = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
        vector = new Vector3(-tempvec.x, tempvec.z, tempvec.y);
        vector = vector / 100;
        CraneHandler.instance.SetPlayerPosition(vector);
        */

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
            case "CraneError!":
                TheClaw.instance.CraneError();
                break;
        }
    }

    public static void RoomEntry( Packet _packet)
    {
        string msg = _packet.ReadString();
        //a lockpick completion would be awareLockPick, a complete challenge would be ch1:1
        switch (msg)
        {
            case string anotif when anotif.StartsWith("aware"):
                GameManagerScript.instance.notif = anotif;
                GameManagerScript.instance.NotifAnnounce();
                break;
            case string achlge when achlge.StartsWith("ch"):
                string[] chnum = achlge.Split(':', 2);
                ChallengeParser.instance.OnChallengeComplete(Int32.Parse(chnum[1]));
                break;
        }
    }

    public static void WorldGeometryPosition(Packet _packet)
    {
        int code = _packet.ReadInt();

		int itr = _packet.ReadInt();
        //TheClaw.instance.MoveClawToLocation(vector);
        for (int i = 0; i < itr; i++)
        {
            var posx = _packet.ReadInt();
            var posy = _packet.ReadInt();
            var posz = _packet.ReadInt();

            Vector3 pos = new Vector3(-posx, posz, posy) / 100000;
            Vector3 euler = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
            euler = new Vector3(euler.x, euler.z, euler.y);
            switch (code)
            {
                case 1: WorldBuilder.Instance.SpawnWorldFloorTile(i, pos, euler);
                    break;
                case 2: WorldBuilder.Instance.SpawnWall(i, pos, euler);
                    break;
                case 3: WorldBuilder.Instance.SpawnDoor(i, pos, euler);
                    break;
                case 4: WorldBuilder.Instance.SpawnElevator(i, pos, euler);
                    break;
                case 5: WorldBuilder.Instance.SpawnElevatorPort(i, pos, euler);
                    break;
                case 6: WorldBuilder.Instance.SpawnControlRoom(i, pos, euler);
                    break;
                case 7: WorldBuilder.Instance.SpawnCrate(i, pos, euler);
                    break;
                case 8: WorldBuilder.Instance.SpawnEngine(i, pos, euler);
                    break;
                case 9: WorldBuilder.Instance.SetCraneOrientation(i, pos, euler);
                    break;
                case 10: WorldBuilder.Instance.SpawnCraneControl(i, pos, euler);
                    break;

            }


        }

    }

    public static void PlayerTransformTracking(Packet _packet)
    {
        int length = _packet.ReadInt();

        Vector3 tempvec = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
        Vector3 vector = new Vector3(-tempvec.x, tempvec.z, tempvec.y);
        vector = vector / 100000;

        Vector3 tempvec2 = new Vector3(_packet.ReadInt(), _packet.ReadInt(), _packet.ReadInt());
        Vector3 vector2 = new Vector3(-tempvec.x, tempvec.z, tempvec.y);
        vector2 = vector2;
        PlayerTracker.Instance.SetOrientation(vector, vector2);
    }
}
