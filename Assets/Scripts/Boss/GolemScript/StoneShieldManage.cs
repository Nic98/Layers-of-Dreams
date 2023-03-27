using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class StoneShieldManage : MonoBehaviour
{   
    public GameObject StoneShieldTemplate;
    public float ShieldRegenerateCoolDownTime;
    private HandyCoolDown ShieldRegeneCoolDown;
    private bool haveShield = true;
    void Start() {
        ShieldRegeneCoolDown = null;
    }
    public void shieldDestroyed() {
        haveShield = false;
        ShieldRegeneCoolDown = new HandyCoolDown(ShieldRegenerateCoolDownTime, "Regenerate Golem Shield");
    }
    void generateShield()
    {
        GameObject Shield = Instantiate(StoneShieldTemplate);
        Shield.transform.SetParent(this.transform);
    }
    void Update()
    {   
        if (haveShield == false)
        {
            bool done = ShieldRegeneCoolDown.check();
            if (done)
            {
                ShieldRegeneCoolDown = null;
                generateShield();
                haveShield = true;
            }
        }
    }
}
