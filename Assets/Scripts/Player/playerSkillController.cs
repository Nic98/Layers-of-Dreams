using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class playerSkillController : MonoBehaviour
{   /*
      this script keeps track of all player skills information
      including cool down times, key bindings, and UI update handling
      (but not necessarily the skill logics)

      suggestion:
        write the logics in separate scripts, or, get other components from player attributes
      
      functions called in attribute update:
       - useSkill() - check input, do skill logic, start cool down, update UI
    */
    /****************************************************************************/
    public enum PlayerSkill { // used for UI updates
        Attack, // this is only for UI update only, dont do logic/anything with this Attack
        SkillOne, // not implemented
        SkillTwo, // not implemented
        SkillThree, // not implemented
        ScatterTheWeak // instantiate a PS, and gives a knockback effect for enemies within a specific range
    };
    /******************************** All Skills ********************************/
    public float SkillOneCoolDownTime;
    private HandyCoolDown SkillOneCoolDown;
    /******************************************/
    public float SkillTwoCoolDownTime;
    private HandyCoolDown SkillTwoCoolDown;
    /******************************************/
    public float SkillThreeCoolDownTime;
    private HandyCoolDown SkillThreeCoolDown;
    /******************************************/
    public GameObject ScatterTheWeak;
    public float ScatterTheWeakCoolDownTime;
    private HandyCoolDown ScatterTheWeakCoolDown;
    /****************************************************************************/
    // all components from player attribute
    // private playerAnimationController PlayerAnimation = null;
    // private playerMoveController PlayerMove = null;
    // private playerAttackController PlayerAttack = null;
    //private playerBuffManage PlayerBuff = null;
    /****************************************************************************/
    void Start() {
        SkillOneCoolDown = null;
        SkillTwoCoolDown = null;
        SkillThreeCoolDown = null;
        ScatterTheWeakCoolDown = null;

        // PlayerAnimation = this.GetComponent<playerAnimationController>();
        // PlayerMove = this.GetComponent<playerMoveController>();
        // PlayerAttack = this.GetComponent<playerAttackController>();
        // PlayerBuff = this.GetComponent<playerBuffManage>();
    }

    public void useSkill() // Basically Key Bindings
    {
        if (Input.GetKeyDown(KeyCode.R)) { UseScatterTheWeak(); }

        // else if (Input.GetKeyDown(KeyCode.Alpha1)) { UseSkillOne(); }
        // not implemented, keycode taken by item

        // else if (Input.GetKeyDown(KeyCode.Alpha2)) { UseSkillTwo(); }
        // not implemented, keycode taken by item

        // else if (Input.GetKeyDown(KeyCode.Alpha3)) { UseSkillThree(); }
        // not implemented, keycode taken by item
        
    }
    
    /**************************************************************************/
    // All Skill Relevant Informations

    void UseScatterTheWeak()
    {   
        if (ScatterTheWeakCoolDown == null)
        {
            // skill logic
            GameObject skill = Instantiate(ScatterTheWeak);
            skill.transform.position = this.transform.position + Vector3.up;

            // skill UI update
            GameEvents.current.PlayerUseSkill(PlayerSkill.ScatterTheWeak, ScatterTheWeakCoolDownTime);
            // initiate cool down
            ScatterTheWeakCoolDown = new HandyCoolDown(ScatterTheWeakCoolDownTime, "ScatterTheWeakSkill");
        }
    }

    void UseSkillOne()
    {   
        if (SkillOneCoolDown == null)
        {   
            // logic?
            GameEvents.current.PlayerUseSkill(PlayerSkill.SkillOne, SkillOneCoolDownTime);
            SkillOneCoolDown = new HandyCoolDown(SkillOneCoolDownTime, "SkillOne");
        }
    }

    void UseSkillTwo()
    {   
        if (SkillTwoCoolDown == null)
        {   
            // logic?
            GameEvents.current.PlayerUseSkill(PlayerSkill.SkillTwo, SkillTwoCoolDownTime);
            SkillTwoCoolDown = new HandyCoolDown(SkillTwoCoolDownTime, "SkillTwo");
        }
    }

    void UseSkillThree()
    {   
        if (SkillThreeCoolDown == null)
        {   
            // logic?
            GameEvents.current.PlayerUseSkill(PlayerSkill.SkillThree, SkillThreeCoolDownTime);
            SkillThreeCoolDown = new HandyCoolDown(SkillThreeCoolDownTime, "SkillThree");
        }
    }

    /**************************************************************************/
    // Boring Part: do all cool downs
    void Update() {
        if (ScatterTheWeakCoolDown != null)
        {
            bool done = ScatterTheWeakCoolDown.check();
            if (done) { ScatterTheWeakCoolDown = null;}
        }

        if (SkillOneCoolDown != null)
        {
            bool done = SkillOneCoolDown.check();
            if (done) { SkillOneCoolDown = null; }
        }

        if (SkillTwoCoolDown != null)
        {
            bool done = SkillTwoCoolDown.check();
            if (done) { SkillTwoCoolDown = null; }
        }

        if (SkillThreeCoolDown != null)
        {
            bool done = SkillThreeCoolDown.check();
            if (done) { SkillThreeCoolDown = null; }
        }
    }
}