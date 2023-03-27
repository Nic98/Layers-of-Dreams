using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    private GameObject UIcontrol;

    
    void Start()
    {
        UIcontrol=GameObject.Find("LevelUI");
    }

    private void Update()
    {   

        if(UIcontrol){
            if(!UIcontrol.GetComponent<UIControl>().ifPause()){
            CamControl();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        
            }else{
            Cursor.visible=true;
            Cursor.lockState = CursorLockMode.Confined;
            }
        }
        
        
    }
    

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -15, 30);

        transform.LookAt(Target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {   
            if(!Player.GetComponent<TimeStop>().stoping){
                Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
                Player.rotation = Quaternion.Euler(0, mouseX, 0);
            }
            
        }
        
    }
    

}
