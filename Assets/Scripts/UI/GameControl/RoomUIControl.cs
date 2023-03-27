using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class RoomUIControl : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject InstructionMenu;
    public GameObject Dialogue;
    public static bool ShowingDialogue = false;
    public float ShowDialogueTime;
    private HandyCoolDown ShowDialogueCoolDown;
    private bool DoNotShow = false;

    public GameObject mainCamera;
    void Start() {
        ShowDialogueCoolDown = new HandyCoolDown(ShowDialogueTime, "Dialogue Cool Down");
        GameEvents.current.onPortalTriggeredEnter += destroySelf;
        PauseMenuUI.SetActive(false);
        InstructionMenu.SetActive(false);
        Dialogue.SetActive(false);
    }

    void destroySelf()
    {   
        DoNotShow = true;
        Destroy(this.gameObject);
    }
    public virtual void Update()
    {   
        if (ShowDialogueCoolDown != null)
        {
            bool done = ShowDialogueCoolDown.check();
            if (done && !DoNotShow)
            {
                ShowDialogueCoolDown = null;
                ShowDialogue();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Tab)){
            if (ShowingDialogue){
                HideDialogue();
            } else {   
                ShowDialogue();
            }
        }
    }

    public void ResumeGame(){
        PauseMenuUI.SetActive(false);
        InstructionMenu.SetActive(false);
        Dialogue.SetActive(false);
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        if(mainCamera){
            mainCamera.GetComponent<SpecialCameraController>().enabled=true;
        }
        
    }

    void PauseGame(){
        PauseMenuUI.SetActive(true);
        InstructionMenu.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;

        if(mainCamera){
            mainCamera.GetComponent<SpecialCameraController>().CursorUnlock();
            mainCamera.GetComponent<SpecialCameraController>().enabled=false;
        }
        
    }

    public void ShowInstruction() {
        PauseMenuUI.SetActive(false);
        InstructionMenu.SetActive(true);
    }
    public void HideInstruction() {
        PauseMenuUI.SetActive(true);
        InstructionMenu.SetActive(false);
    }

    public void ShowDialogue()
    {
        ShowingDialogue = true;
        PauseGame();
        PauseMenuUI.SetActive(false);
        Dialogue.SetActive(true);
    }

    public void HideDialogue()
    {
        ResumeGame();
        ShowingDialogue = false;
    }
}
