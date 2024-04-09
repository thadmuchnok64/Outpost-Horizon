using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton : MenuTrigger
{
    public Sprite urgentImage;


    public void MakeUrgent()
    {
        relay.UrgentTime(true);
    }
    public void ClearUrgent()
    {
        relay.UrgentTime(false);
    }
}
