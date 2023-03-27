using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetectEnemy : MonoBehaviour
{   
    private GameObject NearestEnemy;
    private List<GameObject> EnemyList;
    private bool portalSpawned = false;
    private float attackRange;
    void Start() {
        attackRange = this.GetComponent<playerAttribute>().attackRange;
        NearestEnemy = null;

        EnemyList = new List<GameObject>();
        StartCoroutine("EnemyDetector");
    }
    IEnumerator EnemyDetector()
    {
        while (true)
        {
            yield return null;
            FindScenceEnemy();
            GameEvents.current.FindAllEnemy(EnemyList.Count);
            if(EnemyList.Count<=0) {
                if (portalSpawned == false)
                {
                    portalSpawned=true;
                    GameEvents.current.InstantiatePortal();
                }
            }
            FindNearestEnemy();
            GameEvents.current.EnemyNearbyMode(NearestEnemy);
        }
    }

    private void FindScenceEnemy()
    {	
		GameObject [] EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyList.Clear();
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            // if the enemy is alive
            if (EnemyArray[i].GetComponent<EnemyAttribute>().getHealth() > 0)
            {
                EnemyList.Add(EnemyArray[i]);
            }
        }
    }

    public void FindNearestEnemy()
    {
        NearestEnemy = null;
 
        float nearestDistance = attackRange;
        if (EnemyList != null && EnemyList.Count >=1)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {   
                float EnemyAndMeDistance = CalculationOfDistance(EnemyList[i].transform.position);
                if (EnemyAndMeDistance < nearestDistance)
                {
                    nearestDistance = EnemyAndMeDistance;
                    NearestEnemy = EnemyList[i];
                }
                // update distance between player to each enemy
                EnemyList[i].GetComponent<EnemyAttribute>().setDistanceToPlayer(this.gameObject, EnemyAndMeDistance);
            }
            /*
            if (NearestEnemy!=null)
            {
                if (CalculationOfDistance(NearestEnemy.transform.position) <= attackRange)
                {
                    //print(NearstEnemy.name);
                }
            } */
        }
    }

    private float CalculationOfDistance(Vector3 a)
    {
        float EnemyAndMeDistance = Vector3.Distance(this.transform.position, a);
        return EnemyAndMeDistance;
    }
}