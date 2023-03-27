using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveController : MonoBehaviour
{
    private bool charMoved;
    private bool onGround;
    private bool hasJumped;

    void Start()
    {
        //plane = GameObject.Find("Plane").transform;
        charMoved = false; // default false...
        onGround = true;
        hasJumped = false;
    }
	void Update ()
    {
       
    }

    public bool move(float Speed)
    {
       if (Input.GetKey(KeyCode.W)
           || Input.GetKey(KeyCode.S)
           || Input.GetKey(KeyCode.A)
           || Input.GetKey(KeyCode.D))
        {
            Vector3 playerMovement = new Vector3(0f, 0f, 0f) ;
        
            if(Input.GetKey(KeyCode.W)){
                playerMovement += new Vector3(0f, 0f, 1f);
            }
            if(Input.GetKey(KeyCode.S)){
                playerMovement += new Vector3(0f, 0f, -1f);
            }
            if(Input.GetKey(KeyCode.D)){
                playerMovement += new Vector3(1f, 0f, 0f);
            }
            if(Input.GetKey(KeyCode.A)){
                playerMovement += new Vector3(-1f, 0f, 0f);
            }

            transform.Translate(playerMovement.normalized * Speed * Time.deltaTime);
            return true;
        }else{
            return false;
        }
    }
    public bool jump(float jumpForce) {
        if (Input.GetKeyDown (KeyCode.Space) && onGround) {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            hasJumped = true;
        } else { hasJumped = false; }
        return hasJumped;
    }
    void OnCollisionEnter(Collision other)
    {   
        // change to if the collider is an object that can be "interactive?"
        // if (other.gameObject.tag == "SceneObject")
        if (other.gameObject.tag != "EnemyDamage" && other.gameObject.tag != "Damage") { GameEvents.current.PlayerHitGround(); 
            changeOnGround();
        }
    }

    void changeOnGround() { onGround = true; }
}
