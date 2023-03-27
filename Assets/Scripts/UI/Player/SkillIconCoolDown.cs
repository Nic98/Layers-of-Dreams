using UnityEngine;
using UnityEngine.UI;
public class SkillIconCoolDown : MonoBehaviour
{   
    private float duration;
    private float remainingTime = 0;
    private Image CoolDownIcon;
    void Start() {
        CoolDownIcon = this.GetComponent<Image>();
        CoolDownIcon.fillAmount = 0f;
    }
    public void setCoolDown(float duration) { this.duration = duration; remainingTime = duration; }
    void Update() {
        remainingTime -= Time.deltaTime;
        if (remainingTime > 0)
        {
            CoolDownIcon.fillAmount = calculateRatio();
        }
        else
        {
            CoolDownIcon.fillAmount = 0f;
        }
    }
    float calculateRatio() {
        return remainingTime / duration;
    }
}