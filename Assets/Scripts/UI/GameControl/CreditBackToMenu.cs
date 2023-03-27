using UnityEngine;
using General;

public class CreditBackToMenu: MonoBehaviour
{   
    private CanvasGroup self;
    void Start() {
        self = this.GetComponent<CanvasGroup>();
    }
    void Update() {
        if (self.alpha == 1)
        {   
            GameEvents.current.BackToMainMenu();
        }
    }

}