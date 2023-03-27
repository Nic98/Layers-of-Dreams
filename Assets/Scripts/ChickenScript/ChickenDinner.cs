using UnityEngine;
using General;

public class ChickenDinner : MonoBehaviour {
    private bool disable = true;
    public CanvasGroup self;
    public FadingInUIControl selfFadeIn;
    public FadingOutUIControl selfFadeOut;

    public float displayTime = 5f;
    private HandyCoolDown displayCountDown;
    void Start() {
        
    }

    void Update() {


        if (disable)
        {
            if (this.transform.childCount == 1)
            {
                // no more chickens! some moron killed them all!
                displayAchievement();
                disable = false;
            }
        }
        else
        {   
            if (displayCountDown == null)
            {
                displayCountDown = new HandyCoolDown(displayTime, "Display");
            }
            else
            {
                bool done = displayCountDown.check();
                if (done)
                {
                    if (selfFadeOut != null) 
                    {
                        selfFadeOut.disable = false;
                    }
                   
                }
            }
        }
    }

    void displayAchievement()
    {
        selfFadeIn.disable = false;
    }
}