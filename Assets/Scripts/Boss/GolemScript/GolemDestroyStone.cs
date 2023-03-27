using UnityEngine;

public class GolemDestroyStone : MonoBehaviour {
    public GameObject Boss;
    void Update() {
        if (Boss == null)
        {
            Destroy(this.gameObject);
        }
    }
}