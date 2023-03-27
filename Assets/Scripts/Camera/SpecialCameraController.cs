using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCameraController : MonoBehaviour
{   float camSens = 300.0f; //How sensitive it with mouse
    private float speed = 2f;
    private Vector3 rotation;
    private Vector3 thisRot;
    public float clampAngleX;
    public float minclampAngleY;
    public float maxclampAngleY;
    public float rotX; // rotation around the right/x axis
    public float rotY; // rotation around the up/y axis
    public Vector3 initialPos;
    public bool moveAble;
    void Start() 
    {
        this.transform.position = initialPos;
    }

    void Update() 
    {
        if (this.GetComponent<Rigidbody>()) 
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * camSens * Time.deltaTime;
        rotX += mouseY * camSens * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        rotX = Mathf.Clamp(rotX, -clampAngleX, clampAngleX);

        if (moveAble)
        {
            Vector3 pV = GetBaseInput();
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, initialPos.y, initialPos.y);
            transform.position = pos;
            pV = pV * Time.deltaTime * speed;
            transform.Translate(pV);

        } else {
            rotY = Mathf.Clamp(rotY, -minclampAngleY, maxclampAngleY);
        }
    }
    private Vector3 GetBaseInput() 
    { 
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
    public void CursorUnlock(){
        Cursor.lockState = CursorLockMode.Confined;
    }
}
