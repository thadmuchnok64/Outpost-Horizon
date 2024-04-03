using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : CameraButton
{
    public int ID = 0;
    [HideInInspector] public bool adminRequested;
    public Sprite adminsprite;
    public override void DoAction()
    {
        if (!adminRequested)
            ClientSend.AttemptUnlockDoor(ID);
        else
        {
            GameManagerScript.instance.ShowMenuItem(1);
            GameManagerScript.instance.doorAdminCode = ID;
        }
    }

    public void AdminNeeded()
    {
        relay.GetComponent<Image>().sprite = adminsprite;
        adminRequested = true;
    }
}
