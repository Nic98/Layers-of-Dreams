using System;
using UnityEngine;

namespace General
{
    public class HandyCoolDown
    {
        /* The purpose of this class is to create an timer object
        such that there is no need to use variables and for loops and stuff
        all over the place.
        Contains:
        - contructor: take a cool down time as parameter, set up a cooldown object
        - private variables:
                - remaining cool down
                - boolean to return when cool down is done
            - public function checkCoolDown() to return boolean
        */

        private float coolDown;
        private string itemName;
        private bool DONE;
        private bool debugPrint;

        // constructor
        public HandyCoolDown(float time, string name, bool printing = false)
        {
            coolDown = time;
            itemName = name;
            DONE = false;
            debugPrint = printing;
            
            if (debugPrint)
            {
                Debug.Log("Cool Down Initalised for" + itemName + " :" + coolDown.ToString() + " seconds");
            }
        }

        public float getRemainingTime() { return coolDown; }

        public bool check()
        {   
            this.coolDown -= Time.deltaTime;
            // Debug.Log(itemName + "Cooling Down..." + coolDown.ToString() + " seconds");

            if (coolDown <= 0)
            {   
                if (debugPrint) {
                    Debug.Log("Cool Down is done for " + itemName + "!");
                }
                DONE = true;
            }

            return DONE;
        }
    }
    
    /********************************************************************************/
    public class Buffer
    {
        private string buffName;
        public HandyCoolDown CoolDown;
        public HandyCoolDown OneSecond;
        // delegate is like javascript var XXX = (param) => {code}
        // where XXX is actually storing the function "definition"
        public delegate void ContinuousEffectFunction(playerAttribute targetAttr);
        public ContinuousEffectFunction ContinuousEffect;
        public delegate void UndoEffectFunction(playerAttribute targetAttr);
        public UndoEffectFunction UndoBuffEffect;

        public Buffer(string buffName, float CoolDownTime, ContinuousEffectFunction funcVar1, UndoEffectFunction funcVar2)
        {
            this.buffName = buffName;
            CoolDown = new HandyCoolDown(CoolDownTime, buffName);
            ContinuousEffect = funcVar1; // to be updated every second
            UndoBuffEffect = funcVar2; // to be run when buff expires
            resetOneSec();
        }
        public string getBuffName() { return buffName; }
        public void resetOneSec()
        { OneSecond = new HandyCoolDown(1f, "1sec"); }
    }
}
