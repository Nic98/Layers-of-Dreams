using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyAttackController : MonoBehaviour
{   
    /*
      parent class of enemy attacking logics
       - decides if enemy should attack or not
       - the actual attack is written in child class
      
      function called in attribute update:
       - attack()

      function to override in child class:
       - attackLogic()

      functions called in playerDetectEnemy.cs
       - setDistanceToPlayer()
    */
    private HandyCoolDown AttackCoolDown;
    private float AttackCoolDownTime;
    private float DistanceToPlayer;
    private GameObject player;
    private playerAttribute playerAttr;
    void Start() {
        player = null;
        DistanceToPlayer = 9999; // without initial value it is set to 0, otherwise may casue a bug
        
        AttackCoolDownTime = this.GetComponent<EnemyAttribute>().AttackCoolDownTime;
        AttackCoolDown = null; // this cool down affects enemy attack rate
    }
    public void setDistanceToPlayer(GameObject player, float distance) {
        // this function is called in PlayerDetectEnemy.cs
        // whenever player calculated some distances values
        // it passes values to enemy for update
        this.player = player;
        playerAttr = player.GetComponent<playerAttribute>();
        DistanceToPlayer = distance;
    }
    public bool playerInSearchRange(float searchRange)
    {   
        if (DistanceToPlayer <= searchRange)
        {   
            // print(this.gameObject.name+" has found player. (AttackController)");
            return true;
        }
        else
        {   
            // print(this.gameObject.name+" has lost sight of player. (AttackController)");
            return false;
        }
    }
    public bool attack(float damage, float attackRange)
    {   
        // stop attacking player if player is dead
        if (playerAttr.health <= 0) { return false; }
        
        if (DistanceToPlayer <= attackRange && AttackCoolDown == null)
        {   
            // print(this.gameObject.name+" Parent--Attack()");
            AttackCoolDown = new HandyCoolDown(AttackCoolDownTime, "Enemy Attack");
            attackLogic(damage, player);
            return true;
        }
        else {
            return false;
        }
    }
    public virtual void attackLogic(float damage, GameObject player)
    {
        // print(this.gameObject.name+" Parent--Do Attack Logic");
        // keyword: virtual and override
    }

    void Update() {
        // UPDATE() IS FOR COOL DOWN ONLY!!!
        // DO NOT PUT ANYTHING ELSE OTHER THAN COOL DOWN
        if (AttackCoolDown != null)
        {   
            bool done = AttackCoolDown.check();
            if (done) {
                AttackCoolDown = null;
            }
        }
    }
}