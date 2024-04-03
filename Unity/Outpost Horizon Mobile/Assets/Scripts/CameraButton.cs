using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : MonoBehaviour
{

    public Sprite buttonSprite;
    [HideInInspector] public ButtonRelay relay;

    private void Start()
    {
        CameraButtonManager.instance.AddButton(this,buttonSprite);
    }

    public virtual void DoAction()
    {
        throw new NotImplementedException($"No functionality programmed for {gameObject.name} button");
    }
}
