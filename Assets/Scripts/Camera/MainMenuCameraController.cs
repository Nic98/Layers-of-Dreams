using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    public Transform target;

    void Update()
    {   
        if (target == null) { return; }
        transform.LookAt(target);
        Cursor.lockState = CursorLockMode.Confined;
    }

}