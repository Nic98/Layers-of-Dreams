using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{   
    
    public virtual void destroySelf() 
    {
        Destroy(this.gameObject);
    }
}
