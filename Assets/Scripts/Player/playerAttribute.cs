using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class playerAttribute : MonoBehaviour
{
    /****************************************************************************/
    // public variable for configuration
    public bool DieOnNoHealth;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float health;
    public float jumpForce;
    public float StartPosX = 0f;
    public float StartPosY = 0f;
    public float StartPosZ = 0f;
    public float DeathDepth = -5f;
    public float FallFromEdgeDamage = 2f;
    /****************************************************************************/
    public float AttackCoolDownTime;
    public float FaceEnemyModeCoolDownTime;
    public float DamageAnimationCoolDownTime;
    /****************************************************************************/
    // some setters
    public void setAttackRange(float range) { attackRange = range; }
    public void setAttackDamage(float damage) { attackDamage = damage; }
    public void setJumpForce(float jump)
    { jumpForce = jump; }
    public void setMoveSpeed(float speed) { moveSpeed = speed; }
    /****************************************************************************/
    // health management
    private bool isAlive;
    private float maxHealth;
    private HealthSave HealthSave;
    public bool getPlayerStatus() { return isAlive; }
    public float getMaxHealth() { return maxHealth; }
    public void recoverHealth(float recover)
    {   
        // print("Recovered" + recover.ToString());
        if ( (health + recover) > maxHealth) { health = maxHealth; }
        else { health += recover; }
        GameEvents.current.PlayerChangeHealth(); // damage is also handled in UI
    }
    bool checkIfDead()
    {   
        bool isDead = false;
        
        // list death conditions
        if (health <= 0 & DieOnNoHealth) { isDead = true; }

        if (isDead)
        {
            GameEvents.current.PlayerDeathEnter();
        }
        return !isDead;
    }
    public void playerIsDamaged(float damage)
    {   // on enemy damage:
        // minus health, knock back, damage animation...
        // 1. minus health
        if (damage > health) { health = 0; }
        else 
        { 
            //gameObject.GetComponent<TimeStop>().StopTime();
            health -= damage;
            PlayerDamageManage.damageEffect();
        }
        // print("Player is damaged. Remaining HP: "+health.ToString());
        // 2. damage animation
        if ( PlayerAnimation != null) { PlayerAnimation.TakeDamage(); }
        // 3. health UI
        GameEvents.current.PlayerChangeHealth(); // damage is also handled in UI
    }
    bool getHealthSaveFromGlobalOption()
    {
        GameObject[] global = GameObject.FindGameObjectsWithTag("GlobalOption");

        if (global.Length > 0)
        {
            HealthSave = global[0].GetComponent<HealthSave>();
            maxHealth = HealthSave.getMaxHealth();
            health = HealthSave.getHealth();
            
            return true;
        }
        return false;
    }
    void saveHealthToGlobalOption()
    {
        if (HealthSave != null)
        {
            HealthSave.saveCurrentHealth(health);
        }
    }
    /****************************************************************************/
    // all components
    private playerAnimationController PlayerAnimation;
    private playerMoveController PlayerMove;
    private playerRotateController PlayerRotate;
    private playerAttackController PlayerAttack;
    private playerDamageManage PlayerDamageManage;
    private playerSkillController PlayerSkillControl;
    /****************************************************************************/
    private playerBuffManage PlayerBuff;
    // on trigger (pick up buff item)
    public void applyItemBuffEffect(Buff buff) {
        if (PlayerBuff != null) { PlayerBuff.applyBuff(buff); }
    }
    public List<Buffer> getBuffList() {
        // getting buff list from buff manage so that UI has access to it
        if (PlayerBuff != null) { return PlayerBuff.getBuffList(); }
        return null;
    }
    /****************************************************************************/
    private playerInventory Inventory;
    public void pickUpBuffItem(Buff buff) {
        if (Inventory != null) { Inventory.addToInventory(buff); }
    }
    /****************************************************************************/
    void Start() {
        // initialise player location
        transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
        PlayerAnimation = this.GetComponent<playerAnimationController>();
        PlayerMove = this.GetComponent<playerMoveController>();
        PlayerRotate = this.GetComponent<playerRotateController>();
        PlayerAttack = this.GetComponent<playerAttackController>();
        PlayerDamageManage = this.GetComponent<playerDamageManage>();
        PlayerBuff = this.GetComponent<playerBuffManage>();
        PlayerSkillControl = this.GetComponent<playerSkillController>();
        Inventory = this.GetComponent<playerInventory>();

        if (getHealthSaveFromGlobalOption() == false)
        {   
            maxHealth = health;
        }

        if (HealthSave != null)
        {
            GameEvents.current.onPortalTriggeredEnter += saveHealthToGlobalOption;
        }
        
        isAlive = true;
        
        // for debug only, used to have dismatch with UI, because player attr sets health on Start not Awake
        // print("PLAYER: Maxhealth: " + maxHealth.ToString() + " | health: " + health.ToString());
    }
    
    void Update() {
        if (isAlive)
        {
            isAlive = checkIfDead(); // check if player is dead (death mechanics not written yet)
        } // double check make sure alive?

        if (isAlive)
        {   
            if (PlayerBuff != null) { PlayerBuff.checkAllBuffs(); }
            // this is for UI...

            bool moved, jumped, attacked; // booleans for animator

            // if player has move component, check movement input first
            if (PlayerMove != null)
            {   
                // check input arrow keys and space bar
                moved = PlayerMove.move(moveSpeed);
                jumped = PlayerMove.jump(jumpForce);
            }
            else { moved = false; jumped = false; }
            
            // if player has rotate component, do rotation, based on input, or enemy
            //if (PlayerRotate != null) { PlayerRotate.rotate(); } 

            // if player has attack component, check attack input
            if (PlayerAttack != null)
            { attacked = PlayerAttack.attack(attackRange, attackDamage, AttackCoolDownTime); }
            else { attacked = false; }

            // if player has animation, do animation, based on what action player has done
            if (PlayerAnimation != null)
            {    
                if (moved) { PlayerAnimation.IdleToMove(); }
                else { PlayerAnimation.MoveToIdle(); }

                if (jumped) { PlayerAnimation.ToJump(); }
            } // no attack animation yet...

            // if player falls from the edge out of the plane/map
            if (transform.position.y < DeathDepth)
            {   // do damage and then reset
                // playerIsDamaged(FallFromEdgeDamage);
                // transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
                // 3 nov - change to reload
                if(isAlive){
                    GameEvents.current.PlayerDeathEnter();
                    isAlive=false;
                }
            }

            if (attacked)
            {
                GameEvents.current.PlayerUseSkill(playerSkillController.PlayerSkill.Attack, AttackCoolDownTime);
            }

            if (PlayerSkillControl != null)
            {
                PlayerSkillControl.useSkill();
            }
           
        }
    }
}
