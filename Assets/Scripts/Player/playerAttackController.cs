using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class playerAttackController : MonoBehaviour
{   /*
      this script dictates how player attacks
      
      functions called in attribute update:
       - attack()

      functions called in playerDetectEnemy.cs
       - enemyNearby
       - setDistanceToPlayer()
    */
    public GameObject ProjectileTemplate;
    private HandyCoolDown AttackCoolDown;
    private bool EnemyIsNearby;
    private GameObject NearestEnemy;
    private bool hasAttacked;
    public AudioClip shootSound;

    void Start() {
        hasAttacked = false;
        AttackCoolDown = null;
        
        GameEvents.current.onEnemyNearbyEnter += enemyNearby;
        GameEvents.current.onEnemyDeathEnter += exitEnemyNearby;
    }

    public bool attack(float attackRange, float attackDamage,float AttackCoolDownTime)
    {   
        Vector3 playerPos = this.transform.position;

        if (Input.GetMouseButton(0) && AttackCoolDown == null) {
            
            hasAttacked = true; // player press A to attack
        
            // calculate direction vector for projectile
            Vector3 dir;

            if (EnemyIsNearby)
            {   
                GameEvents.current.PlayerDoAttack(NearestEnemy);
                Vector3 enemyPos = NearestEnemy.transform.position;
                dir = (enemyPos - (playerPos + Vector3.up)).normalized;
            }

            else { dir = calculateDir(); } // this is based on player input

            // instantiate projectile
            Vector3 bulletInsPos = this.gameObject.transform.position + dir + Vector3.up;
            GameObject bullet = Instantiate(ProjectileTemplate, bulletInsPos , Quaternion.identity);
            ProjectileManager bulletManage = bullet.gameObject.GetComponent<ProjectileManager>();
            bulletManage.setVelocity(dir);
            bulletManage.setDamage(attackDamage);
            
            // set cool down, control player attack rate
            AttackCoolDown = new HandyCoolDown(AttackCoolDownTime, "Player Attack Cool Down");
            
            if (shootSound != null) 
            {
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
            }
            
        } else {hasAttacked = false; } // player did not press A

        return hasAttacked;
    }

    void enemyNearby(GameObject enemy) {
        // function 
        NearestEnemy = enemy;
        if (NearestEnemy != null)
        { EnemyIsNearby = true; }
        else { EnemyIsNearby = false; }
    }

    void exitEnemyNearby() {
        // function 
        NearestEnemy = null;
        EnemyIsNearby = false;
    }

    Vector3 calculateDir() { return transform.forward; }

    void Update() {
        // UPDATE() IS FOR COOL DOWN ONLY!!!
        if (AttackCoolDown != null)
        {   
            bool done = AttackCoolDown.check();
            if (done) {
                AttackCoolDown = null;
            }
        }
    }
}