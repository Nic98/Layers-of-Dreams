using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryBlessing : AttrBuff
{   
    float recovery = 0.5f;
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        NeedCoolDown = true;
        secondaryType = SecondaryType.Stat;
        BuffCoolDownTime = 6f;

        target.gameObject.GetComponent<playerAttribute>().pickUpBuffItem(this);
    }
    public override void changeWhileCoolDown(playerAttribute targetAttr)
    {   
        targetAttr.recoverHealth(recovery);
    }
}
