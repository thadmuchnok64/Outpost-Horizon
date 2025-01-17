﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    internal class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                    Server.clients[i].tcp.SendData(_packet);
            }
        }

        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void TestMessage(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.test))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SendBrokenRodInfoToUnity(int _toClient, List<int> ints)
        {
            using (Packet _packet = new Packet((int)ServerPackets.breakrod))
            {
                _packet.Write(ints.Count);

                for (int i = 0; i < ints.Count; i++)
                {
                    _packet.Write(ints[i]);
                }

                SendTCPData(_toClient, _packet);
            }
        }

        public static void ClawControl(int _toClient, int control)
        {
            using (Packet _packet = new Packet((int)ServerPackets.clawControl))
            {
                _packet.Write(control);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void ClawPositionInfo(int _toClient, List<int> list)
        {
            using (Packet _packet = new Packet((int)ServerPackets.clawPosition))
            {
                foreach (int i in list)
                {
                    _packet.Write(i);
                }

                SendTCPData(_toClient, _packet);
            }
        }

        public static void VerifyUnityUserHasLoggedIn(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.UnityToUnrealLogin))
            {
                SendTCPData(_toClient, _packet);
            }
        }

        public static void SendRoomInfoToUnity(int _toClient, string code)
        {
            using (Packet _packet = new Packet((int)ServerPackets.roomEntry))
            {
                _packet.Write(code);
                SendTCPData(_toClient, _packet);
            }
        }



        public static void WorldGeometryPositionInfo(int _toClient, List<int> list)
        {
            using (Packet _packet = new Packet((int)ServerPackets.WorldGeometryPositionInfo))
            {
                foreach (int i in list)
                {
                    _packet.Write(i);
                }

                SendTCPData(_toClient, _packet);
            }

        }

        public static void DoorUnlock(int _toClient, int doorId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.DoorUnlock))
            {
                _packet.Write(doorId);

                SendTCPData(_toClient, _packet);
            }

        }

        public static void ElevatorSend(int _toClient, int doorId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.ElevatorSend))
            {
                _packet.Write(doorId);

                SendTCPData(_toClient, _packet);
            }

        }

        public static void PlayerTransformTracking(int _toClient, List<int> transform)
        {
            using (Packet _packet = new Packet((int)ServerPackets.PlayerTransformTrack))
            {
                foreach (int i in transform)
                {
                    _packet.Write(i);
                }

                SendTCPData(_toClient, _packet);
            }

        }

        public static void DoorAdminUnlock(int _toClient, int doorId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.RequestAdmin))
            {
                _packet.Write(doorId);

                SendTCPData(_toClient, _packet);
            }

        }

        public static void GrantAdmin(int _toClient, int doorId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.GrantAdmin))
            {
                _packet.Write(doorId);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void TrackElevator(int _toClient, List<int> list)
        {
            using (Packet _packet = new Packet((int)ServerPackets.TrackElevator))
            {
                foreach (int i in list)
                {
                    _packet.Write(i);
                }

                SendTCPData(_toClient, _packet);
            }
        }

        public static void WaypointMessage(int _toClient, int id,string mes)
        {
            using (Packet _packet = new Packet((int)ServerPackets.WaypointMessage))
            {
                _packet.Write(id);
                _packet.Write(mes);

                SendTCPData(_toClient, _packet);
            }
        }
    }
}
