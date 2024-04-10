using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : CameraButton
{
    public int ID = 0;
    [SerializeField] int taskCompletion = -1;
    [HideInInspector] public bool adminRequested;
    [SerializeField] MeshRenderer doormesh;
    [SerializeField] Material doorOpenMat;
    public Sprite altSprite;
    public Sprite adminsprite;
    public override void DoAction()
    {
        if (!adminRequested)
        {
            ClientSend.AttemptUnlockDoor(ID);
            ChallengeParser.instance.TryCompleteChallenge(taskCompletion);
        }
        else
        {
            GameManagerScript.instance.ShowMenuItem(1);
            GameManagerScript.instance.doorAdminCode = ID;
        }
    }

    public void UnlockedButton()
    {
        relay.GetComponent<Image>().sprite = altSprite;
        Material[] mats = { doorOpenMat };
        doormesh.SetMaterials(mats.ToList());
    }

    public void AdminNeeded()
    {
        relay.GetComponent<Image>().sprite = adminsprite;
        adminRequested = true;
    }
}
