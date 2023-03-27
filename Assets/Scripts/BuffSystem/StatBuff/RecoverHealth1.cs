using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealth1 : AttrBuff
{   
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        NeedCoolDown = false;
        secondaryType = SecondaryType.Stat;
        
        target.gameObject.GetComponent<playerAttribute>().pickUpBuffItem(this);
    }
    public override void changeImmediate(playerAttribute targetAttr)
    {   
        targetAttr.recoverHealth(1);
    }
}
