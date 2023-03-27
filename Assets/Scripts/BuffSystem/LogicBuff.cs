using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBuff : Buff // parent class
{
    public override void SetBuffStat()
    {
        primaryType = PrimaryType.Logic;
        base.SetBuffStat();
    }
}