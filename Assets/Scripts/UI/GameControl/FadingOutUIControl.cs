using UnityEngine;
using General;

public class FadingOutUIControl: MonoBehaviour
{   
    public float fadeOutSpeed;
    public bool disable = true;
    private CanvasGroup UIGroup;
    void Start() {
        UIGroup = this.GetComponent<CanvasGroup>();
    }
    void Update() {
        if (disable) { return; }
        if (UIGroup.alpha > 0)
        {
            UIGroup.alpha -= Time.deltaTime * fadeOutSpeed;
        }
        if (UIGroup.alpha == 0)
        {
            Destroy(this);
        }
    }
}