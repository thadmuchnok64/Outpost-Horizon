using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton : MenuTrigger
{
    public Sprite urgentImage;

    public override void DoAction()
    {
        base.DoAction();
        ChallengeParser.instance.TryCompleteChallenge(5);
        RodBehaviour.instance.RodsTUpdate();
    }
    public void MakeUrgent()
    {
        relay.UrgentTime(true);
    }
    public void ClearUrgent()
    {
        relay.UrgentTime(false);
    }
}
