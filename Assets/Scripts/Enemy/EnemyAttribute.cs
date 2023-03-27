using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyAttribute : MonoBehaviour
{   
    /***********************************************************************/
    // public variables for configuration
    public float mapSize = 25.0f; // not sure if this is needed anymore?
    public float attackRange;
    public float searchRange;
    public float moveSpeed;
    public float attackDmg;
    public float DeathDepth = -5f;
    /***********************************************************************/ 
    public float AttackCoolDownTime;
    public float DeathAnimationCoolDownTime;
    private HandyCoolDown DeathCoolDown;
    public float DamageAnimationCoolDownTime;
    public float AttackAnimationCoolDownTime;
    /***********************************************************************/
    // health management
    public float maxHealth;
    public float health;
    public void enemyIsDamaged(float pushForce, float damage, Vector3 dir)
    {   // on enemy damage:
        // minus health, knock back, damage animation, health UI...
        // 1. minus health
        health -= damage;
        // print(this.gameObject.name+" is damaged. Remaining HP: "+health.ToString());
        // 2. knockback
        this.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        // 3. damage animation
        if (EnemyAnimation != null)
        {
            EnemyAnimation.KnockBack();
        }
    }
    void checkIfDead()
    {   
        bool isDead = false;

        // list death conditions here
        if (health <= 0) { isDead = true; }
        else if (transform.position.y < DeathDepth) { isDead = true; }


        if (isDead)
        {
            GameEvents.current.EnemyDeathEnter(); // reset player rotation target
            // print(this.gameObject.name + " is dead.");
            // originally destroy is placed here
            // but I think it is better just to put a cooldown for its animator
            DeathCoolDown = new HandyCoolDown(DeathAnimationCoolDownTime, "Enemy Death");
            if (EnemyAnimation != null)
            {
                EnemyAnimation.PlayDead();
            }
        }
    }
    // getHealth() is used by playerDetectEnemy (to determine it is dead)
    public float getHealth() { return health; } 
    /***********************************************************************/
    // all components
    private EnemyMoveController EnemyMove;
    private EnemyAttackController EnemyAttack;
    private EnemyAnimationController EnemyAnimation;
    private EnemyDeathController EnemyDeath;
    /***********************************************************************/
    void Start() {
        health = maxHealth;
        
        EnemyMove = this.GetComponent<EnemyMoveController>();
        EnemyAttack = this.GetComponent<EnemyAttackController>();
        EnemyAnimation = this.GetComponent<EnemyAnimationController>();
        EnemyDeath = this.GetComponent<EnemyDeathController>();
        DeathCoolDown = null;
        // animator: set MoveToIdle (enter idle state)
    }
    public void setDistanceToPlayer(GameObject player, float distance)
    {   
        if (EnemyAttack != null)
        { EnemyAttack.setDistanceToPlayer(player, distance); }

        if (EnemyMove != null)
        { EnemyMove.setDistanceToPlayer(player, distance); }
    }

    void Update() {
        
        // if death cool down is not initialised
        // and that the enemy object itself still exist
        // it means it is "alive" (health > 0)
        bool isAlive = (DeathCoolDown == null);

        if (isAlive)
        {
            checkIfDead();
            // check again, if it dies
            // (just want to be very sure it is not dead, avoiding null pointer exception)
            isAlive = (DeathCoolDown == null); 
        }
        if (!isAlive)
        {   // if death cool down is initiated
            // it's dead... just use cool down to wait for animation
            // then destroy itself
            
            bool done = DeathCoolDown.check();
            if (done)
            {   
                DeathCoolDown = null;
                // print(this.gameObject.name+" is destroyed.");
                EnemyDeath.destroySelf();
            }
        }

        // i know this is weird doing this condition again
        // i just want these codes to be more readable
        // i hope doing this can help making the logic/mechanics easy to understand
        bool moved = false, doAttack = false;
        if (isAlive)
        {   
            bool playerIsFound;
            if (EnemyAttack != null) // if attack component is added to enemy
            {   
                playerIsFound = EnemyAttack.playerInSearchRange(searchRange);
                if (playerIsFound)
                {  
                    EnemyMove.calculateDir();
                    EnemyMove.rotate(); // face playerEnemyMove.rotate(); // face player
                    
                    // print(this.gameObject.name+" has found player.");
                    doAttack = EnemyAttack.attack(attackDmg, attackRange);
                    // animation:
                    // if enemy did attack, and it has an animation component
                    if (doAttack && EnemyAnimation != null) { EnemyAnimation.Attack(); }

                    if (!doAttack)
                    {   
                        // print(this.gameObject.name+"is too far from player to attack.");
                        if (EnemyMove != null) // if enemy has move component
                        {   
                            if(EnemyAnimation != null){
                                EnemyAnimation.AfterAttack();
                            }
                            // print(this.gameObject.name+" will now move towards player.");
                            moved = EnemyMove.moveToPlayer(moveSpeed, attackRange);
                            
                            // animation
                            if (moved && EnemyAnimation != null)
                            {
                                EnemyAnimation.IdleToMove();
                            }
                        }
                    }
                }
                else // if enemy has lost sight of player
                {
                    if (EnemyAnimation != null) {
                        EnemyAnimation.MoveToIdle();
                    }
                }
            }
        }
    }
}
