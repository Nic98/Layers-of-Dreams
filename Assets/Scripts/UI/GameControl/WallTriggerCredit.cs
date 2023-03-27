using UnityEngine;

public class WallTriggerCredit : MonoBehaviour {
    public LastRoomUIControl contorlScript;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            contorlScript.ResumeGame();
            contorlScript.PlayCredit();
        }
    }
}