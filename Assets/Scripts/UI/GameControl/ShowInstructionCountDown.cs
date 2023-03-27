using UnityEngine;
using General;

public class ShowInstructionCountDown : MonoBehaviour {
    public float ShowInstructionTime = 2f;
    private HandyCoolDown CountDown;
    private UIControl UIControl;
    void Start() {
        CountDown = new HandyCoolDown(ShowInstructionTime, "Show Instruction Count Down");
        UIControl = this.GetComponent<UIControl>();
    }
    void Update() {
        if (CountDown != null)
        {
            bool done = CountDown.check();
            if (done)
            {
                CountDown = null;
                UIControl.ShowInstruction();
            }
        }
    }
}