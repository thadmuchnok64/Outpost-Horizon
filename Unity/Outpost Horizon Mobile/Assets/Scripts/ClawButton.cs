using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawButton : MenuTrigger
{
    public override void DoAction()
    {
        ChallengeParser.instance.TryCompleteChallenge(13);
        base.DoAction();
    }
}
