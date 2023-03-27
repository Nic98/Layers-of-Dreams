using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderEgg : MonoBehaviour
{   
    // DO NOT PUT START() IN THIS SCRIPT
    // IT WILL OVERRIDE THE START FUNCITON IN PARENT CLASS
    public GameObject SpiderBoss;
   
    void Start(){
        
    }
    void Update(){
        if (this.GetComponent<EnemyAttribute>().getHealth()==0){
            SpiderBoss.SetActive(true);
            this.GetComponent<EnemyAttribute>().health-=1;
            GetComponent<EggController>().Destroy();
        }
    }

    
}
