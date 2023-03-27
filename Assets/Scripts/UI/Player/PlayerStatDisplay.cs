using UnityEngine;
using UnityEngine.UI;

/* Reference:
 * https://www.youtube.com/watch?v=_RIsfVOqTaE
 * https://answers.unity.com/questions/1254100/add-image-dynamically-in-runtime.html
 */

public class PlayerStatDisplay : MonoBehaviour
{   
    public GameObject PlayerTemplate;
    public Text playerDevDisplay;
    public float indentation; // distance from border of left top corner
    public float spacing; // spacing between health symbols
    public Texture2D symbolFull;
    public Texture2D symbolHalf;
    public Texture2D symbolEmpty;
    public bool HideDevDisplay;
    private playerAttribute playerAttr;
    private float maxHealth;
    private float health;
    private bool[] switches;
    private int switchCounter; // count of trues in the array
    void Start()
    {   
        if (PlayerTemplate == null)
        {
            Destroy(this.gameObject);
        }
        playerAttr = PlayerTemplate.GetComponent<playerAttribute>();

        if (getMaxHealthFromGlobalOption() == false)
        {
            maxHealth = playerAttr.getMaxHealth();
            health = playerAttr.health;
            if (maxHealth == 0f)
            {
                maxHealth = health;
            }
        }
        // multiply by 2 -> for half health?
        switches = new bool[(int)(2*maxHealth)];

        changeDisplay(health);
        playerDevDisplay.text = "";

        // for debug only, used to have dismatch, because player attr sets health on Start not Awake
        // print("UI: Maxhealth: " + maxHealth.ToString() + " | health: " + health.ToString());

        GameEvents.current.onPlayerChangeHealthEnter += changeDisplay;
    }

    bool getMaxHealthFromGlobalOption()
    {
        GameObject[] global = GameObject.FindGameObjectsWithTag("GlobalOption");

        if (global.Length > 0)
        {
            HealthSave save = global[0].GetComponent<HealthSave>();
            maxHealth = save.getMaxHealth();
            health = save.getHealth();
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {   
        health = playerAttr.health;
        if (!HideDevDisplay) { updateDevDisplay(); }
    }

    void updateDevDisplay()
    {       
        // for dev only
        playerDevDisplay.text = "";
        playerDevDisplay.text += "Health: " + health.ToString();
        
        playerDevDisplay.text += " | " + "Speed: " + playerAttr.moveSpeed.ToString();
        playerDevDisplay.text += " | " + "AtkDamage: " + playerAttr.attackDamage.ToString();
        playerDevDisplay.text += " | " + "AtkRange: " + playerAttr.attackRange.ToString();
        playerDevDisplay.text += " | " + "Jump: " + playerAttr.jumpForce.ToString();

        playerDevDisplay.text += " |";
    }

    // this is kind of updated on its own?
    void OnGUI()
    {   
        int i = 0;
        
        while ( (i+2) <= switches.Length) {
            if (switches[i] && switches[i+1]) // both true = full
            {   
                // GUI.DrawTexture (new Rect ((Screen.width * 0.5f) + indentation + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolFull);
                GUI.DrawTexture (new Rect ((Screen.width * i *spacing / 1000) + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolFull);
            }
            else if (!(switches[i] || switches[i+1])) // both false = empty
            {
                // GUI.DrawTexture (new Rect ((Screen.width * 0.5f) + indentation + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolEmpty);
                GUI.DrawTexture (new Rect ((Screen.width * i *spacing / 1000) + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolEmpty);
            }
            else { // 1 true 1 false = half
                // GUI.DrawTexture (new Rect ((Screen.width * 0.5f) + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolHalf);
                GUI.DrawTexture (new Rect ((Screen.width * i *spacing / 1000) + indentation, indentation, Screen.width * .04f, Screen.width * .04f), symbolHalf);
            }
            i += 2;
        }
    }

    private void changeDisplay() {
        health = playerAttr.health;
        changeDisplay(health);
    }
    private void changeDisplay(float health)
    {
        int healthIterate = (int)(health*2);
        
        for (int i = 0 ; i < switches.Length ; i++)
        {
            if ( i < healthIterate ) { switches[i] = true; }
            else { switches[i] = false; }
        }
    }
}
