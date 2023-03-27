using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamageManage : MonoBehaviour
{   
    public GameObject impactEffect;
    public AudioClip hitSound;

    public void damageEffect()
    {               
        if (hitSound != null) 
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        // this function is called when player is damaged
        if (impactEffect) 
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "EnemyDamage")
        {
            EnemyProjectile bullet = other.gameObject.GetComponent<EnemyProjectile>();
            float damage = bullet.getDamage();
            
            // do damage to player
            this.GetComponent<playerAttribute>().playerIsDamaged(damage);
            Destroy(other.gameObject);
        }
    }
}