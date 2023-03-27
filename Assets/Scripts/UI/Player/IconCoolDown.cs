using UnityEngine;
using UnityEngine.UI;

public class IconCoolDown : MonoBehaviour
{   
    public string buffName;
    private Image CoolDownIcon;
    private float duration;
    private float remainingTime;
    void Start() {
        CoolDownIcon = this.GetComponent<Image>();
        CoolDownIcon.fillAmount = 0f;

        GameEvents.current.onBuffInterrupedEnter += updateCoolDown;
    }
    public void setDuration(float duration) { this.duration = duration; remainingTime = duration;}
    void Update() {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
        CoolDownIcon.fillAmount = calculateRatio();
    }
    
    void updateCoolDown(string name, float time)
    {   
        if (buffName.CompareTo(name) == 0)
        {
            remainingTime = time;
            CoolDownIcon.fillAmount = calculateRatio();
        }
    }
    
    float calculateRatio() {
        return (duration - remainingTime) / duration;
    }
}
