using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUI : MonoBehaviour
{

    [HideInInspector] public int ID = -1;
    public string message;
    public void SetMessage(string _message)
    {
        message = _message;
        CameraButtonManager.instance.AddWaypoint(this, message);
    }

}
