using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterAttack : AttrBuff
{   
    float change = -0.2f;
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        NeedCoolDown = true;
        secondaryType = SecondaryType.Attack;
        BuffCoolDownTime = 6f;

        target.gameObject.GetComponent<playerAttribute>().pickUpBuffItem(this);
    }
    public override void changeImmediate(playerAttribute targetAttr)
    {   
        float original = targetAttr.AttackCoolDownTime;
        
        targetAttr.AttackCoolDownTime = original + change;
    }
    public override void undoChange(playerAttribute targetAttr)
    {   
        float current = targetAttr.AttackCoolDownTime;
        targetAttr.AttackCoolDownTime = current - change;
    }
}
