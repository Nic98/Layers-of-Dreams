using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrBuff : Buff // parent class
{
    public override void SetBuffStat()
    {
        primaryType = PrimaryType.Attr;
        base.SetBuffStat();
    }
}
