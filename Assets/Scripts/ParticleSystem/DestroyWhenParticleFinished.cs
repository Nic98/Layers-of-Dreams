using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenParticleFinished : MonoBehaviour
{
    public ParticleSystem targetParticleSystem;

    void Update()
    {
        if (!this.targetParticleSystem.IsAlive())
        {
            Destroy(targetParticleSystem.gameObject);
        }
    }
    
}
