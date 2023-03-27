using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : AttrBuff
{   
    public float change = 6f;
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        NeedCoolDown = true;
        secondaryType = SecondaryType.Move;
        BuffCoolDownTime = 10f;

        target.gameObject.GetComponent<playerAttribute>().pickUpBuffItem(this);
    }
    public override void changeImmediate(playerAttribute targetAttr)
    {   
        float originalSpeed = targetAttr.moveSpeed;
        targetAttr.setMoveSpeed(originalSpeed + change);
    }
    public override void undoChange(playerAttribute targetAttr)
    {   
        float newSpeed = targetAttr.moveSpeed;
        targetAttr.setMoveSpeed(newSpeed - change);
    }
}
