using UnityEngine;
using General;

public class CreditPageControl: MonoBehaviour
{
    public CanvasGroup former;
    public float displayTime = 3f;
    private HandyCoolDown displayCountDown;
    private CanvasGroup self;
    private FadingInUIControl selfFadeIn;
    private FadingOutUIControl selfFadeOut;
    void Start() {
        displayCountDown = null;
        self = this.GetComponent<CanvasGroup>();
        selfFadeIn = this.GetComponent<FadingInUIControl>();
        selfFadeOut = this.GetComponent<FadingOutUIControl>();
    }
    void Update() {
        if (former == null)
        {
            selfFadeIn.disable = false;
        }
        else if (former.alpha == 0)
        {
            selfFadeIn.disable = false;
        }

        if (self.alpha == 1)
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

        if (self.alpha == 0)
        {
            Destroy(this.gameObject);
        }
    }
}