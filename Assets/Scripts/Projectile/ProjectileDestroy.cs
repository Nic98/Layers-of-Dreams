using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    public float destroyRange = 20.0f ;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Hero"); 
    }
    void Update() 
    {
        Vector3 playerPos = player.transform.position;
        float distanceX = Mathf.Abs(transform.position.x - playerPos.x);
        float distanceZ = Mathf.Abs(transform.position.z - playerPos.z);
        if (distanceX > destroyRange ||
            distanceZ > destroyRange) 
            {
                Destroy(this.gameObject);
            }
    }
}
