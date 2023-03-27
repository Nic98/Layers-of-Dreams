using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class SpiderBoss : EnemyAttackController
{
    public float jumpForce = 2000f;
    public float maxHeight;
    private GameObject player;
    private HandyCoolDown SkillCoolDown;
    private float SkillCoolDownTime = 10f;

    private Component enemyAttribute;
    void Start() 
    {
        player = GameObject.Find("Hero");
        enemyAttribute = this.GetComponent<EnemyAttribute>();
    }
    void Update() 
    {
        if (transform.position.y > maxHeight) 
        {
            Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = newPos;
        }
        if (SkillCoolDown == null)
        {
            SkillCoolDown = new HandyCoolDown(SkillCoolDownTime, "Enemy Attack");
            SpiderBossSkill();
        }
        if (SkillCoolDown != null)
        {   
            bool done = SkillCoolDown.check();
            if (done) 
            {
                SkillCoolDown = null;
            }
        }

    }

    void SpiderBossSkill() 
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    
}
