using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using General;


// https://www.youtube.com/watch?v=ZYeXmze5gxg
public class EnemyHealthBarMulti : MonoBehaviour
{
    public Slider HealthBarInstant1;
    public Slider HealthBarGradual1;
    public Slider HealthBarInstant2;
    public Slider HealthBarGradual2;
    public Slider HealthBarInstant3;
    public Slider HealthBarGradual3;
    public float decreaseRate;
    private int intervalSize = 0;
    public float DelayTime;
    public HandyCoolDown Delay;
    public GameObject Boss;
    private EnemyAttribute BossAttr;
    private Slider currSliderInstant;
    private Slider currSliderGradual;
    private int index = 0;
    private List<Slider> InstantBar;
    private List<Slider> GradualBar;
    private bool disable = true;

    void Awake()
    {
        if (Boss != null)
        {
            disable = false;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    void Start() {

        if (!disable)
        {   
            initialise();
        }

        HealthBarInstant1.value = 1;
        HealthBarInstant2.value = 1;
        HealthBarInstant3.value = 1;

        InstantBar = new List<Slider>();
        InstantBar.Add(HealthBarInstant1);
        InstantBar.Add(HealthBarInstant2);
        InstantBar.Add(HealthBarInstant3);
    
        HealthBarGradual1.value = 1;
        HealthBarGradual2.value = 1;
        HealthBarGradual3.value = 1;

        GradualBar = new List<Slider>();
        GradualBar.Add(HealthBarGradual1);
        GradualBar.Add(HealthBarGradual2);
        GradualBar.Add(HealthBarGradual3);

        decreaseRate /= 1000;
        Delay = null;
    }

    void initialise()
    {   
        BossAttr = Boss.GetComponent<EnemyAttribute>();
        intervalSize = (int) (BossAttr.maxHealth / 3);
    }

    float calculateHealth() {
        float result = BossAttr.getHealth() % intervalSize;
        if (result == 0) {
            if (BossAttr.getHealth() == 0) { return 0; }
            return 1;
        }
        else { return result / intervalSize; }
    }
    

    void Update() {

        if (!disable)
        {
            float health = BossAttr.getHealth();
            float newValue;

            if (health >= 0)
            {   
                float maxHealth = BossAttr.maxHealth;
                float bound = maxHealth - intervalSize*(index+1);

                if (health <= bound)
                {   currSliderInstant.gameObject.SetActive(false); index++; }

                newValue = calculateHealth();

                if (index < InstantBar.Count)
                {   
                    currSliderInstant = InstantBar[index];
                    currSliderGradual = GradualBar[index];
                    updateInstantBar(currSliderInstant, newValue);
                    updateGradualBar(currSliderGradual, newValue);
                } 
            }
            else { newValue = 0; }

            if (index != 0)
            { updateGradualBar(GradualBar[index-1], 0); }
        }
    }

    void updateInstantBar(Slider HealthBarInstant, float newValue)
    {
        HealthBarInstant.value = newValue;
    }

    void updateGradualBar(Slider HealthBarGradual, float newValue)
    {   
        if ( HealthBarGradual.value == 0 )
        {
            HealthBarGradual.gameObject.SetActive(false);
            return;
        }
        if (HealthBarGradual.value < newValue)
        {
            HealthBarGradual.value = newValue;
            Delay = null;
        }
        else if (HealthBarGradual.value > newValue)
        {   
            if (Delay == null)
            {
                Delay = new HandyCoolDown(DelayTime, "HealthBarDelay");
            }else
            {
                if (Delay.check()) { HealthBarGradual.value -= decreaseRate; }
            }
        }
    }
}