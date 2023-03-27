using UnityEngine;

public class SpiderBossMeleeAttack : EnemyMeleeAttackLogic
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public override void attackLogic(float damage, GameObject player)
    {   
        // do damage to player
        base.attackLogic(damage, player);
        
        playerAttribute playerAttr = player.GetComponent<playerAttribute>();
        
        // apply poison buff - continuously decrease health
        Buff PoisonedDebuff = this.GetComponent<Poisoned>();
        PoisonedDebuff.setTarget(player);
        PoisonedDebuff.SetBuffStat();

        // apply sticky buff - slow down player
        Buff StickyDebuff = this.GetComponent<StickySlowDown>();
        StickyDebuff.setTarget(player);
        StickyDebuff.SetBuffStat();
    }
}