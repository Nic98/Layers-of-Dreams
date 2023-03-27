using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySampleAnimations : MonoBehaviour
{
    private Animator animator;
    private const string key_isWalk = "IsWalk";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    private const string key_isAttack = "IsAttack";

    void Start() 
    {
        
    }

    public void IdleToMove()
    {
        if (animator) {
            animator.SetBool(key_isWalk, true);
            
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isWalk, true);
        }
        
    }

    public void MoveToIdle()
    {   
        if (animator) {
            animator.SetBool(key_isWalk, false);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isWalk, false);
        }
    }
    public void AfterKnockBack(){
        if (animator) {
            animator.SetBool(key_isDamage, false);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, false);
        }
    }

    
    public void TakeDamage()
    {
        if (animator) {
            animator.SetBool(key_isWalk, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, true);
        }
    }

    public void AfterDamage()
    {
        if (animator) {
            animator.SetBool(key_isWalk, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, false);
        }
    }

    public void MeleeAttack()
    {   
        if (animator) {
            animator.SetBool(key_isAttack, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isAttack, true);
        }
    }

    public void AfterMeleeAttack()
    {   
        if (animator) {
            animator.SetBool(key_isAttack, false);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isAttack, false);
        }
    }

    public void PlayDead()
    {   
        if (animator) {
            animator.SetBool(key_isDead, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDead, true);
        }
    }
    public void AfterDead(){
        if (animator) {
            animator.SetBool(key_isDead, false);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDead, false);
        }
    }
    public void KnockBack(){
        if (animator) {
            animator.SetBool(key_isDamage, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, true);
        }
    }

}
