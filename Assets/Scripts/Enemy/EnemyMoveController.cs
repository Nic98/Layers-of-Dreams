using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class EnemyMoveController : MonoBehaviour
{   
    /*
      if player is nearby (within search range)
      enemy should move towards player, such that player is within its attack range

      two functions are called in attribute update:
      - moveToPlayer()
      - rotate()
    */
    private float DistanceToPlayer;
    private GameObject player;
    private Vector3 dir;

    void Start() {
        player = null;
        DistanceToPlayer = 9999; // without initial value it is set to 0, otherwise may casue a bug
    }
    public void setDistanceToPlayer(GameObject player, float distance) {
        // this function is called in PlayerDetectEnemy.cs
        // whenever player calculated some distances values
        // it passes values to enemy for update
        this.player = player;
        DistanceToPlayer = distance;
    }

    public void calculateDir(){
        // print(this.gameObject.name+" is moving towards player.");
            // calculate player direction
            Vector3 enemyPos = transform.position;
            Vector3 playerPos = player.transform.position;
            dir = (playerPos - enemyPos).normalized;
            dir.y=0;
    }
    public bool moveToPlayer(float moveSpeed, float attackRange) {

        if (DistanceToPlayer >= attackRange)
        {
            // move towards player
            transform.position += (dir * moveSpeed * Time.deltaTime);
            
            return true;
        }
        else { return false; }
    }

    public void rotate()
    {   
        float rotation = determineRotation();
        transform.Rotate(0, rotation, 0);
    }

    float determineRotation()
    {   
        float faceOrientation = transform.eulerAngles.y;
        if(dir.x >0 && dir.z>0){
            
            return Mathf.Atan(dir.x/dir.z)* Mathf.Rad2Deg - faceOrientation;
        }
        else if(-1<dir.x && dir.x < 0 && dir.z>0 && dir.z<1){
            return 270f + Mathf.Atan(dir.z/-dir.x)* Mathf.Rad2Deg - faceOrientation;
        }else if(dir.x <1 && dir.x > 0 && dir.z<0 && dir.z>-1){
            return 90f + Mathf.Atan(-dir.z/dir.x)* Mathf.Rad2Deg - faceOrientation;
        }else if(dir.x > -1 && dir.x < 0 && dir.z<0 && dir.z>-1){
            return 180f + Mathf.Atan(-dir.x/-dir.z)* Mathf.Rad2Deg - faceOrientation;
        }else if(dir.x == 0.0f && dir.z == 1.0f){
            return  - faceOrientation;
        }else if(dir.x == 0.0f && dir.z == -1.0f){
            return 180f - faceOrientation;
        }else if(dir.x == 1.0f && dir.z == 0.0f){
            return 90f - faceOrientation;
        }else{
            return 270f - faceOrientation;
        }
    }
}
