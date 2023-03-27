using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterTheWeak : MonoBehaviour
{
    public float pushForce = 200.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy") 
        {
            print(other.name);
            Vector3 dir = calculateDir(other.gameObject);
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        }
    }

    private Vector3 calculateDir(GameObject enemy) 
    {
        Vector3 enemyPos = enemy.transform.position;
        Vector3 playerPos = this.transform.position;
        return (enemyPos - playerPos).normalized;
    }
}
