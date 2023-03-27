using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWithin : AttrBuff
{   
    float change = 1f;
    float bleeding = 2f;
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        NeedCoolDown = true;
        secondaryType = SecondaryType.Attack;
        BuffCoolDownTime = 8f;

        target.gameObject.GetComponent<playerAttribute>().pickUpBuffItem(this);
    }
    public override void changeImmediate(playerAttribute targetAttr)
    {   
        // increase attack damage
        float originalDmg = targetAttr.attackDamage;
        targetAttr.setAttackDamage(originalDmg + change);
        // reduce health
        targetAttr.playerIsDamaged(bleeding);
    }
    public override void undoChange(playerAttribute targetAttr)
    {   
        float newDmg = targetAttr.attackDamage;
        targetAttr.setAttackDamage(newDmg - change);
    }
}
