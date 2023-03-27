using UnityEngine;

public class PlayerSkillCoolDownController : MonoBehaviour
{   
    public GameObject NormalAttack;
    public GameObject Skill1;
    public GameObject Skill2;
    public GameObject Skill3;
    public GameObject ScatterTheWeak;
    void Start() {
        GameEvents.current.onPlayerUseSkillEnter += coolDownSkill;
    }

    void coolDownSkill(playerSkillController.PlayerSkill skill, float CoolDownTime)
    {   
        switch(skill)
        {
            case playerSkillController.PlayerSkill.Attack:
                NormalAttack.GetComponentInChildren<SkillIconCoolDown>().setCoolDown(CoolDownTime);
                break;
            case playerSkillController.PlayerSkill.SkillOne:
                Skill1.GetComponentInChildren<SkillIconCoolDown>().setCoolDown(CoolDownTime);
                break;
            case playerSkillController.PlayerSkill.SkillTwo:
                Skill2.GetComponentInChildren<SkillIconCoolDown>().setCoolDown(CoolDownTime);
                break;
            case playerSkillController.PlayerSkill.SkillThree:
                Skill3.GetComponentInChildren<SkillIconCoolDown>().setCoolDown(CoolDownTime);
                break;
            case playerSkillController.PlayerSkill.ScatterTheWeak:
                ScatterTheWeak.GetComponentInChildren<SkillIconCoolDown>().setCoolDown(CoolDownTime);
                break;
        }
    }
    
}