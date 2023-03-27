using UnityEngine;
using General;

public class playerRotateController : MonoBehaviour
{
    private float faceOrientation;

    // 8 orientations
    // if forward = facing North
    // orientations are N, NE, E, SE, S, SW, W, NW
    private float facingN;
    private float facingNE; // up + right
    private float facingE; // right
    private float facingSE; // right + down
    private float facingS; // down
    private float facingSW; // down + left
    private float facingW; // left
    private float facingNW; // left + up
    private float rotation;
    private bool FaceEnemyMode;
    private Transform target;
    private HandyCoolDown FaceEnemyModeCoolDown;
    private float FaceEnemyModeCoolDownTime;
    private Vector3 oriRotation;
    private float rotateSpeed = 5f;

    void Start()
    {
        defineOrientation();
        rotation = facingN; // default to face north at start
        FaceEnemyMode = false;
        FaceEnemyModeCoolDownTime = this.GetComponent<playerAttribute>().FaceEnemyModeCoolDownTime;
        
        GameEvents.current.onPlayerAttackEnter += enterFaceEnemyMode;
        // every time an enemy die, player get another enemy as target
        GameEvents.current.onEnemyDeathEnter += exitFaceEnemyMode;
    }

    void defineOrientation()
    {
        facingN = 0f;
        facingNE = 45f;
        facingE = 90f;
        facingSE = 135f;
        facingS = 180f;
        facingSW = 225f;
        facingW = 270f;
        facingNW = 315f;
    }

    float determineRotation()
    {

        faceOrientation = transform.eulerAngles.y;

        // strange case of pressing two together...
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
           if (Input.GetKey(KeyCode.DownArrow)) {
               return facingS - faceOrientation;
           }
            
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {   // if getting normal inputs up right left input(s)

            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {   // NE = up + right
                return facingNE - faceOrientation;
            }

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {   // SE = right + down
                return facingSE - faceOrientation;
            }

            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {   // SW = down + left
                return facingSW - faceOrientation;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {   // NW = left + up

                return facingNW - faceOrientation;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {   // E = right
                return facingE - faceOrientation;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {   // W = left
                return facingW - faceOrientation;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {   // S = down
                return facingS - faceOrientation;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            return facingN - faceOrientation;
        }

        // if no arrow key input
        return 0f; // no rotation
    }

    public void rotate() {

        if (FaceEnemyMode) {
            var lookPos = target.position - transform.position;
            lookPos.y = 0; // do not look up/down
            var rotation = Quaternion.LookRotation (lookPos);
            transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        }
        else
        {
            // set rotation based on arrow keys input
            rotation = determineRotation();
        }
       
        if (rotation != 0) 
        { 
            if (rotation > 180 || rotation < -180) {
                transform.Rotate(0, Time.deltaTime * rotateSpeed * -rotation, 0); 
            } else {
                transform.Rotate(0, Time.deltaTime * rotateSpeed * rotation, 0);
            }
        }
    }
        

    void enterFaceEnemyMode(GameObject enemy) {
        FaceEnemyMode = true;
        target = enemy.transform;
        rotation = 0f;
        FaceEnemyModeCoolDown = new HandyCoolDown(FaceEnemyModeCoolDownTime, "Player Rotate to Face Enemy");
    }

    // call if lose target/ no lock on any enemy?
    void exitFaceEnemyMode()
    {   
        target = null;
        FaceEnemyMode = false;
    }

    void Update() {
        // UPDATE() IS FOR COOL DOWN ONLY!!!

        if (FaceEnemyModeCoolDown != null)
        {   
            bool done = FaceEnemyModeCoolDown.check();
            if (done) {
                exitFaceEnemyMode();
                FaceEnemyModeCoolDown = null;
            }
        }
    }
    
}