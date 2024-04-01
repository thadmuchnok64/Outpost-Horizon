using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRelay : MonoBehaviour
{
    public CameraButton button;

    public void DoAction()
    {
        if(button == null)
        {
            Debug.Log("Unassigned camera button!");
            Destroy(this);
        }
        button.DoAction();
    }
}
