using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using General;


// https://www.youtube.com/watch?v=ZYeXmze5gxg
public class EnemyHealthBarEnhance : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject HealthBarCanvas;
    public Slider HealthBarInstant;
    public Slider HealthBarGradual;
    public float decreaseRate;
    public float DelayTime;
    public HandyCoolDown Delay;
    private EnemyAttribute enemyAttr;
    void Start() {
        enemyAttr = this.GetComponent<EnemyAttribute>();
        Delay = null;

        HealthBarInstant.value = 1;
        if (HealthBarGradual != null) { HealthBarGradual.value = 1; }
        
        /*
        foreach ( Transform child in HealthBarInstant.transform)
        { // https://answers.unity.com/questions/205391/how-to-get-list-of-child-game-objects.html
            if (child.name.CompareTo("Fill Area") == 0)
            {
                health = child.transform.GetComponentInChildren<Image>();
                health.color = healthColor; // this line does not work 
            }
        }*/

        decreaseRate /= 1000;
        if (mainCamera == null)
        {
            print("Ahh, please drag and drop camera on inspector please, for the EnemyHealthBar Script.");
        }
    }

    float calculateHealth() { return enemyAttr.getHealth() / enemyAttr.maxHealth; }

    void Update() {
        float health = enemyAttr.getHealth();

        if (health < enemyAttr.maxHealth)
        {
            HealthBarCanvas.SetActive(true);
        }
        else if (health <= 0)
        {
            HealthBarCanvas.SetActive(false);
        }

        float newValue = calculateHealth();
        
        HealthBarInstant.value = newValue;
        if (HealthBarGradual != null)
        {
            if (HealthBarGradual.value <= HealthBarInstant.value)
            {
                HealthBarGradual.value = HealthBarInstant.value;
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
        HealthBarCanvas.transform.LookAt(mainCamera.transform); // make the canvas look at camera
    }
}