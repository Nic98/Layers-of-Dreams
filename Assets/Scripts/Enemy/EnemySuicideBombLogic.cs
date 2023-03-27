using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemySuicideBombLogic : EnemyAttackController
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public GameObject createOnDestroy;
    public AudioClip hitSound;
    
    public override void attackLogic(float damage, GameObject player)
    {   
        // print(this.gameObject.name+" attacked (Child--Suicide)");
        
        // do damage to player
        player.GetComponent<playerAttribute>().playerIsDamaged(damage);
        // kill itself
        this.GetComponent<EnemyAttribute>().enemyIsDamaged(0, 100 , Vector3.zero);
        if (createOnDestroy != null) 
        {
            GameObject obj = Instantiate(this.createOnDestroy);
            obj.transform.position = this.transform.position;
        }
        
        if (hitSound != null) 
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
    }
}