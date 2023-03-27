public class StickySlowDown : AttrBuff
{   
    public float change;
    
    public override void SetBuffStat()
    {
        base.SetBuffStat(); // setting stuff

        secondaryType = SecondaryType.Move;

        target.gameObject.GetComponent<playerAttribute>().applyItemBuffEffect(this);
    }
    public override void changeImmediate(playerAttribute targetAttr)
    {   
        float originalSpeed = targetAttr.moveSpeed;
        targetAttr.setMoveSpeed(originalSpeed - change);
    }
    public override void undoChange(playerAttribute targetAttr)
    {   
        float newSpeed = targetAttr.moveSpeed;
        targetAttr.setMoveSpeed(newSpeed + change);
    }
}
