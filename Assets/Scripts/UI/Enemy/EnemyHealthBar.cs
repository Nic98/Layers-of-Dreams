using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// https://www.youtube.com/watch?v=ZYeXmze5gxg
public class EnemyHealthBar : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject HealthBarCanvas;
    public Slider HealthBarInstant;
    public Slider HealthBarGradual;
    public float decreaseRate;
    private EnemyAttribute enemyAttr;
    private RectTransform HealthBarInstantRect;
    void Start() {
        enemyAttr = this.GetComponent<EnemyAttribute>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (HealthBarInstant != null) { HealthBarInstant.value = 1; }
        if (HealthBarGradual != null) { HealthBarGradual.value = 1; }

        decreaseRate /= 1000;
    }

    float calculateHealth() { return enemyAttr.getHealth() / enemyAttr.maxHealth; }

    void Update() {
        float health = enemyAttr.getHealth();

        if (health < enemyAttr.maxHealth)
        {
            HealthBarCanvas.gameObject.SetActive(true);
        }
        else if (health <= 0)
        {
            HealthBarCanvas.gameObject.SetActive(false);
        }

        float newValue = calculateHealth();
        
        if (HealthBarInstant != null) {HealthBarInstant.value = newValue;}
        if (HealthBarGradual != null)
        {
            if (HealthBarGradual.value < newValue)
            {
                HealthBarGradual.value = newValue;
            }
            else if (HealthBarGradual.value > newValue)
            {
                HealthBarGradual.value -= decreaseRate;
            }
        }
        HealthBarCanvas.transform.LookAt(mainCamera.transform); // make the canvas look at camera
    }
}