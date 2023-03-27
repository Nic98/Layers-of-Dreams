using UnityEngine;

public class ChickenTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Destroy(this.gameObject);
        }
    }
}