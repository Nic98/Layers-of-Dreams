using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using General;

public class PlayerBuffDisplay : MonoBehaviour
{  
    public Text playerDevDisplay;
    public bool HideDevDisplay;
    private HandyCoolDown OneSecond;
    private List<Buffer> BuffList;
    public GameObject PlayerTemplate;
    public int IconSpacing;
    public int horizontalIndent;
    public int verticalIndent;
    private playerAttribute playerAttr;
    private float h_unit;
    private float v_unit;
    private int lastIconCount = 0;
    void Start() {
        if (PlayerTemplate == null)
        {
            Destroy(this.gameObject);
        }
        if (!HideDevDisplay) { playerDevDisplay.text = "BuffList: \n"; } else { playerDevDisplay.text = ""; }
        playerAttr = PlayerTemplate.GetComponent<playerAttribute>();
        resetOneSec();
        BuffList = null;
        h_unit = Screen.width*0.01f;
        v_unit = Screen.height*0.01f;

        GameEvents.current.onPlayerNewBuffEnter += addBuffIcon;
    }
    void Update() {
        BuffList = playerAttr.getBuffList();

        if (BuffList.Count != transform.childCount) { /* print("Oops Catch Error: BuffDisplay.cs"); */ }
        if (transform.childCount != lastIconCount) {
            rearrange();
            lastIconCount = transform.childCount;
        }

        if (!HideDevDisplay) {
            bool oneSec = OneSecond.check();
            if (oneSec){
                resetOneSec();
                playerDevDisplay.text = "BuffList: \n";
                if (BuffList != null) { updateBuffDisplay(); }
            }
        }
    }
    void rearrange() {
        int indent = 0;
        foreach ( Transform child in this.transform )
        {   // https://answers.unity.com/questions/205391/how-to-get-list-of-child-game-objects.html
            child.position = this.transform.position;
            shiftIcon(child, indent);
            indent += 1;
        }
    }
    void updateBuffDisplay() // dev only
    {   
        for (int i = 0; i < BuffList.Count ; i++)
        {   
            Buffer buffer = BuffList[i];
            string name = buffer.getBuffName();
            float remainingTime = buffer.CoolDown.getRemainingTime();
            // dev only
            playerDevDisplay.text += "" + name + " : " + ((int)remainingTime).ToString() + "\n";
        }
    }
    void addBuffIcon(string name, float remainingTime, GameObject icon) {
        if (icon != null)
        {   
            int indent = this.transform.childCount;
            
            GameObject buffIcon = Instantiate(icon);
            IconCoolDown iconCoolDown = buffIcon.GetComponentInChildren<IconCoolDown>();
            buffIcon.transform.SetParent(this.transform, false);
            iconCoolDown.setDuration(remainingTime);
            iconCoolDown.buffName = name;
            shiftIcon(buffIcon.transform, indent);
        }

        if (!HideDevDisplay)
        {
            playerDevDisplay.text += "" + name + " : " + ((int)remainingTime).ToString() + "\n";
        }
    }
    void shiftIcon(Transform icon, int indent)
    {
        icon.position += Vector3.right*((horizontalIndent)+((IconSpacing*indent)))*h_unit;
        icon.position += Vector3.down*verticalIndent*v_unit;
    }
    void resetOneSec()
    { OneSecond = new HandyCoolDown(1f, "1sec"); }
}