using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float speed;
    public bool stoping;
    public GameObject impactEffect;
    public Component UIcontrol;
    public CameraShake cameraShake;
    void Start()
    {
        stoping=false;
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        UIcontrol = GameObject.Find("LevelUI").GetComponent<UIControl>();
    }
    void Update()
    {
        if (!stoping && !UIControl.GameIsPaused){
            Time.timeScale=1.0f;
        }
        

    }
    public void StopTime()
    {
        stoping=true;
        Time.timeScale=0.0f;
        // Instantiate(impactEffect, transform.position, Quaternion.identity);
        StartCoroutine(cameraShake.Shake(0.05f, 1f));
        //Time.timeScale = ChangeTime;
        StartCoroutine(StartTimeAgain(0.15f));
        
    }

    IEnumerator StartTimeAgain(float amt) 
    {
        
        yield return new WaitForSecondsRealtime(amt);
        stoping = false;
    }
}
