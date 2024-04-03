using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrigger : CameraButton
{
    public int menuIndex = 0;
    public override void DoAction()
    {
        GameManagerScript.instance.ShowMenuItem(menuIndex);
    }
}
