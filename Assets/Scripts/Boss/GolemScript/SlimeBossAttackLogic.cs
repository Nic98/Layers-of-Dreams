using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class SlimeBossAttackLogic : EnemyAttackController
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public GameObject ProjectileTemplate;

    public override void attackLogic(float damage, GameObject player)
    {   
        Vector3 dir;
        Vector3 playerPos = player.transform.position;
        dir = (playerPos + Vector3.up - this.transform.position).normalized;
        Vector3 dir1= Quaternion.Euler(0,60,0) * dir;
        Vector3 dir2= Quaternion.Euler(0,-60,0) * dir;
        GameObject slim = Instantiate(ProjectileTemplate, this.gameObject.transform.position +  2 * Vector3.up + 3 * dir, Quaternion.identity);
        GameObject slim1 = Instantiate(ProjectileTemplate, this.gameObject.transform.position + 2 *Vector3.up + 3 * dir1, Quaternion.identity);
        GameObject slim2 = Instantiate(ProjectileTemplate, this.gameObject.transform.position + 2 *Vector3.up + 3 * dir2, Quaternion.identity);
        
        // do event trigger on collision, not here
        // print(this.gameObject.name+" attacked (Child--Ranged)");
    }
}