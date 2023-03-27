using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShieldDestroyContoller : MonoBehaviour
{
    void Update() {
        if (this.transform.childCount == 0)
        {   
            this.GetComponentInParent<StoneShieldManage>().shieldDestroyed();
            Destroy(this.gameObject);
        }
    }
}
