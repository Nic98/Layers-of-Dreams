using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : AttrBuff
{   
    float bleedingRate = 0.5f;
    
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        secondaryType = SecondaryType.Attack;

        target.gameObject.GetComponent<playerAttribute>().applyItemBuffEffect(this);
    }
    public override void changeWhileCoolDown(playerAttribute targetAttr)
    {   
        targetAttr.playerIsDamaged(bleedingRate);
    }
}