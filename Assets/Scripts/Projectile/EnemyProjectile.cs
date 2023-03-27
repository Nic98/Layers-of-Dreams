using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private Vector3 velocity;
    private float damage = 1.0f;
    public float pushForce = 0.0f;
    public void setVelocity(Vector3 dir) { velocity = dir; }
    public void setDamage(float attackDamage) { damage = attackDamage; }
    public Vector3 getVelocity() { return velocity; }
    public float getDamage() { return damage; }
    public float getPushForce() { return pushForce; }
    void Update() 
    {
        this.transform.Translate(speed/10 * velocity);
    }
}
