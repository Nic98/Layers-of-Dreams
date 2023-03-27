using UnityEngine;
using UnityEngine.UI;

public class EnemyCountUI : MonoBehaviour
{
    public Text TextDisplay;

    void Start() {
        TextDisplay.text = "Count:\n0";
        GameEvents.current.onFindAllEnemyEnter += changeCountDisplay;
        GameEvents.current.onPlayerDeathEnter += destroySelf;   
    }
    void changeCountDisplay(int count)
    {
        TextDisplay.text = "Count:\n" + count.ToString();
    }
    void destroySelf(){
        Destroy(this.gameObject);
    }
}