using System.Collections.Generic;
using UnityEngine;
using General;

public class playerBuffManage : MonoBehaviour
{   
    /*
      this script manages the buffs
      if no buff, no bug
      if buff, but no script attached, also no bug > < !!
      
      functions called in attribute (on trigger with buff item):
       - applyBuff()

      functions called in attribute update:
       - checkAllBuffs()

      for every udpate:
       - go through list of buffs
       - check their cooldown
       - if 1 second passed (1f), do its continuous effect
       - if the buff expires, it runs undo buff effect
       - and then at the end, clear any expired buff(s)
    */
    private playerAttribute playerAttr;
    private List<Buffer> BuffList;
    void Start() {
        playerAttr = this.GetComponent<playerAttribute>();
        BuffList = new List<Buffer>();
    }
    public List<Buffer> getBuffList() { return BuffList; }
    public void applyBuff(Buff buff)
    {   
        switch(buff.primaryType)
        {
            case Buff.PrimaryType.Attr:
                applyAttrBuff(buff);
                break;
            case Buff.PrimaryType.Logic:
                //applyLogicBuff(buff);
                print("Change Logic Buff Not Implemented Yet! ");
                break;
        }
    }
    void applyAttrBuff(Buff buff)
    {   
        bool ifNewBuff = true; // init value
        
        if (buff.NeedCoolDown) // check if buff effect has a duration
        {   
            ifNewBuff = addNewBuff(buff); // will check our buff list
        }
        
        if (ifNewBuff) // does not exist in list, or it is one-time buff
        {
            // print("Adding Buff -- " + buff.GetType().Name);
            buff.changeImmediate(playerAttr); // JUST DO IT! MAKE YOUR DREAMS COME TRUE!
        }
    }
    bool addNewBuff(Buff buff)
    {   
        string buffName = buff.GetType().Name;
        float CoolDownTime = buff.BuffCoolDownTime;

        // https://docs.microsoft.com/en-us/dotnet/api/system.string.compareto?view=netcore-3.1
        // use this lambda find to see if buff already exists in buff list
        Buffer buffer = BuffList.Find(obuff => (obuff.getBuffName().CompareTo(buffName) == 0));
        
        if (buffer == null)
        {   // if not found, add it to list
            buffer  = new Buffer(buffName, CoolDownTime, buff.changeWhileCoolDown, buff.undoChange);
            BuffList.Add(buffer);
            // change UI
            GameEvents.current.PlayerNewBuff(buffName, CoolDownTime, buff.Icon);
            return true;
        }
        else
        {   // if found, renew its cooldown
            buffer.CoolDown = new HandyCoolDown(CoolDownTime, buffName);
            // change UI
            GameEvents.current.InterruptBuff(buffName, CoolDownTime);
            return false;
        }
    }
    public void checkAllBuffs()
    {
        // do all buff and cooldown first (uhh count dowm?)
        for (int i = 0; i < BuffList.Count ; i++)
        {
            Buffer buffer = BuffList[i];

            bool done = buffer.CoolDown.check();
            if (done) {
                buffer.UndoBuffEffect.Invoke(playerAttr);
                buffer.ContinuousEffect.Invoke(playerAttr); // invoke for last time
                // print("Removing Buff -- " + buffer.getBuffName());
                buffer.CoolDown = null;
            }

            bool oneSec = buffer.OneSecond.check();
            if (oneSec)
            {
                buffer.resetOneSec();
                buffer.ContinuousEffect.Invoke(playerAttr);
            }

        }
        //https://stackoverflow.com/a/3069663/14397665 List.RemoveAll(lambda)
        // clean up the list, remove any buff that is already done
        BuffList.RemoveAll(buffer => buffer.CoolDown == null);
    }
}
