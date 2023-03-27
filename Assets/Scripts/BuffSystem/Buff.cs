using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour // grandparent class
{   
    public enum PrimaryType { Attr, Logic } //  what types of change does it make
    public enum SecondaryType {Stat, Move, Attack} // what component it impacts on
    public GameObject Icon = null; // for UI
    // if this bool = false, it will be a permanent change. 
    public bool NeedCoolDown;
    public float BuffCoolDownTime;
    public PrimaryType primaryType;
    public SecondaryType secondaryType;
    
    protected GameObject target;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){

            target = other.gameObject;
            SetBuffStat();
            
            Destroy(this.gameObject);
        }
    }
    public void setTarget(GameObject target) {
        this.target = target;
    }
    public virtual void SetBuffStat() {}
    public virtual void changeImmediate(playerAttribute targetAttr) {}
    public virtual void changeWhileCoolDown(playerAttribute targetAttr) {} // called every second
    public virtual void undoChange(playerAttribute targetAttr) {}
}
