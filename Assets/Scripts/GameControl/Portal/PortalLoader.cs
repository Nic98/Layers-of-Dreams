using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLoader : MonoBehaviour
{   
    public Vector3 portalPos;
    public GameObject portalTemplate;
    void Start(){
        GameEvents.current.onInstantiatePortal+=InstantiatePortal;
    }
    void InstantiatePortal(){
        // print("Portal Instantiated.");
        GameObject portal = Instantiate(portalTemplate);
        portal.transform.position=portalPos;
    }
    
}
