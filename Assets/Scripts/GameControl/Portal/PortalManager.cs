using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalManager : MonoBehaviour
{   
   
    private bool portalIsOpen = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {   
            if (portalIsOpen)
            {
                // print("Loading Next Level...");
                GameEvents.current.TriggerPortal();
            }
        }
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "Hero") 
        {
            portalIsOpen = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        portalIsOpen = false;
    }
}
