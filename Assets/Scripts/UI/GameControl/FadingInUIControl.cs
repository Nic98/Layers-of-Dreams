using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingInUIControl: MonoBehaviour
{   
    public float fadeInSpeed;
    public bool disable = true;
    private CanvasGroup UIGroup;
    void Start() {
        UIGroup = this.GetComponent<CanvasGroup>();
    }
    
    void Update() {
        if (disable) { return; }
        if (UIGroup.alpha < 1)
        {
            UIGroup.alpha += Time.deltaTime * fadeInSpeed;
        }
        if (UIGroup.alpha == 1)
        {
            Destroy(this);
        }
    }
}