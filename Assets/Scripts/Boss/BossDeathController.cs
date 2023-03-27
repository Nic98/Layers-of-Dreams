using UnityEngine;
using General;

public class BossDeathController : EnemyDeathController {
    public Material explosionMat;
    private float CountDownTime = 1.5f;
    private HandyCoolDown CountDown = null;
    private bool DoSth = false;
    public override void destroySelf()
    {   
        CountDown = new HandyCoolDown(CountDownTime, "Golem Boss Death: Counting Down");
        playShader();
    }

    void Update() {
        if (CountDown != null)
        {
            bool done = CountDown.check();
            if (done)
            {
                CountDown = null;
                DoSth = true;
            }
        }
        if (DoSth)
        {
            Destroy(this.gameObject);
        }
    }

    void playShader() 
    {
        SkinnedMeshRenderer renderers = this.GetComponentInChildren<SkinnedMeshRenderer>();      
        this.explosionMat.SetFloat("_StartTime", Time.timeSinceLevelLoad);
        renderers.material = this.explosionMat;
    }
}