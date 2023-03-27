using UnityEngine;

public class HealthSave : MonoBehaviour {
    public float maxHealth = 0f;
    private float health = 0f;

    public float getHealth()
    {   
        if ((health != 0) && ((maxHealth - health) > 2))
        {
            return health + 2; // add but not changing save
        }
        return maxHealth;
    }

    public float getMaxHealth()
    {   
        if (maxHealth == 0f)
        {
            maxHealth = 8f;
        }
        return maxHealth;
    }

    public void saveCurrentHealth(float update)
    {
        health = update;
    }
}