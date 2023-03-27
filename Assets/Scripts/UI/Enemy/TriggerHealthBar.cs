using UnityEngine;

public class TriggerHealthBar : MonoBehaviour {
    public GameObject HealthBarToTrigger;
    public GameObject BGM;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            HealthBarToTrigger.SetActive(true);
            if(BGM) {
                BGM.SetActive(true);
            }
            
            Destroy(this.gameObject);
        }
    }
}