using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : CameraButton
{
    public int ID = 0;
    public override void DoAction()
    {
        ClientSend.SendElevator(ID);
    }
}
