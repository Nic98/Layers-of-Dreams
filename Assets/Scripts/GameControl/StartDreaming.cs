using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDreaming : MonoBehaviour
{
    public bool IfLastRoom;
    private bool dreamed = false;
    void Update()
    {   
        if (IfLastRoom) { return; }
        if (Input.GetKeyDown(KeyCode.T) && !dreamed)
        {   
            dreamed=true;
            Time.timeScale=1f;
            GameEvents.current.TriggerPortal();
        }
    }
}
