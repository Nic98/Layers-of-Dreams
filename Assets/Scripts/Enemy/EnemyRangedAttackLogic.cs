using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyRangedAttackLogic : EnemyAttackController
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public GameObject ProjectileTemplate;
    public override void attackLogic(float damage, GameObject player)
    {   
        Vector3 dir;
        Vector3 playerPos = player.transform.position;
        dir = (playerPos + Vector3.up - this.transform.position).normalized;

        GameObject fireball = Instantiate(ProjectileTemplate, this.gameObject.transform.position + Vector3.up + 2 * dir, Quaternion.identity);
        EnemyProjectile FireballManager = fireball.GetComponent<EnemyProjectile>();
        FireballManager.setVelocity(dir);
        FireballManager.setDamage(damage);
        
        // do event trigger on collision, not here
        // print(this.gameObject.name+" attacked (Child--Ranged)");
    }
}