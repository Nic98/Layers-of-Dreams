using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private const string key_isWalk = "IsWalk";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    private const string key_isAttack = "IsAttack";

    private float DamageAnimationCoolDownTime;
    private HandyCoolDown DamageCoolDown;
    private float AttackAnimationCoolDownTime;
    private HandyCoolDown AttackCoolDown;

    void Start() 
    {   
        animator = this.GetComponent<Animator>();
        if (this.GetComponent<EnemyAttribute>())
        {
            DamageAnimationCoolDownTime = this.GetComponent<EnemyAttribute>().DamageAnimationCoolDownTime;
            DamageCoolDown = null;
            AttackAnimationCoolDownTime = this.GetComponent<EnemyAttribute>().AttackAnimationCoolDownTime;
            AttackCoolDown = null;
        }


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
        animator.SetBool(key_isWalk, true);
        /* if (animator) {
            animator.SetBool(key_isWalk, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, false);
        }*/ 
    }

    public void Attack()
    {   
        AttackCoolDown = new HandyCoolDown(AttackAnimationCoolDownTime, this.gameObject.name+" Attack Animation");
        if (animator) {
            animator.SetBool(key_isAttack, true);
            animator.SetBool(key_isWalk, false);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isAttack, true);
            animator.SetBool(key_isWalk, false);
        }
    }

    public void AfterAttack()
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
        //animator.SetBool(key_isDead, true);
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
        DamageCoolDown = new HandyCoolDown(DamageAnimationCoolDownTime, this.gameObject.name+" Damage Animation");
        if (animator) {
            animator.SetBool(key_isDamage, true);
        } else {
            animator = this.GetComponent<Animator>();
            animator.SetBool(key_isDamage, true);
        }
        
    }

    public void AfterKnockBack()
    {
        animator.SetBool(key_isDamage, false);
    }

    void Update() {
        if (DamageCoolDown != null)
        {
            bool done = DamageCoolDown.check();
            if (done) {
                AfterKnockBack();
                DamageCoolDown = null;
            }
        }

        if (AttackCoolDown != null)
        {
            bool done = AttackCoolDown.check();
            if (done) {
                AfterAttack();
                AttackCoolDown = null;
            }
        }
    }

}
