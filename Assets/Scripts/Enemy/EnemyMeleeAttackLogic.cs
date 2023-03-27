using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyMeleeAttackLogic : EnemyAttackController
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public override void attackLogic(float damage, GameObject player)
    {   
        // base.attackLogic(damage, player);
        // print(this.gameObject.name+" attacked (Child--Melee)");

        // because melee attack basically has no logic...
        // sooo... simply trigger player damaged event

        // do damage to player
        player.GetComponent<playerAttribute>().playerIsDamaged(damage);
    }
}