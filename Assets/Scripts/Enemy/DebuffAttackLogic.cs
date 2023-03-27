using UnityEngine;

public class DebuffAttackLogic : EnemyMeleeAttackLogic
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public override void attackLogic(float damage, GameObject player)
    {   
        // do damage
        base.attackLogic(damage, player);

        playerAttribute playerAttr = player.GetComponent<playerAttribute>();
        
        // apply poison buff - continuously decrease health
        Buff Debuff = this.GetComponent<Buff>();
        Debuff.setTarget(player);
        Debuff.SetBuffStat();
    }
}