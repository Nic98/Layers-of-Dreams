using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShieldMoveController : MonoBehaviour
{
    private GameObject stoneBoss;
    void Start()
    {
        stoneBoss = GameObject.Find("Polyart_Golem");
    }
    void Update()
    {
        if (stoneBoss == null) 
        {
            Destroy(this.gameObject);
        } else {
            transform.position = stoneBoss.transform.position + Vector3.up * 1.8f;
        }
    }
}
