using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : MonoBehaviour
{

    private void Start()
    {
        CameraButtonManager.instance.AddButton(this);
    }

    public virtual void DoAction()
    {
        throw new NotImplementedException($"No functionality programmed for {gameObject.name} button");
    }
}
