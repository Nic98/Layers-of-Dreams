using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectileManager : MonoBehaviour {
    public float speed;
    private Vector3 velocity;
    private float damage;
    public float pushForce = 200.0f;
    //public GameObject LightningEffect;
    void Update() 
    {
        this.transform.Translate(speed/10 * velocity);
    }
    
    // setters (set when shooing)
    public void setVelocity(Vector3 dir) { velocity = dir; }
    public void setDamage(float attackDamage) { damage = attackDamage; }
    public void setPushForce(float pushForce){ this.pushForce = pushForce; }
    // getters (for on collision enter to calculate damage)
    public Vector3 getVelocity() { return velocity; }
    public float getDamage() { return damage; }
    public float getPushForce() { return pushForce; }

}