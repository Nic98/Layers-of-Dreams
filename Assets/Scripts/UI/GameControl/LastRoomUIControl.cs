using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoomUIControl : RoomUIControl
{
    public GameObject CreditUI;
    public override void Update() {
        base.Update();
        if (GameIsPaused == false &&Input.GetKeyDown(KeyCode.T))
        {
            PlayCredit();
        }
    }
    public void PlayCredit()
    {
        Instantiate(CreditUI);
        Destroy(this.gameObject);
    }
}