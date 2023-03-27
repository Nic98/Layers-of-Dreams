using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject InGameUI;
    public GameObject PauseMenuUI;
    public GameObject InstructionMenu;
    public GameObject DeathUIPrompt;

    void Start() {
        if (InGameUI != null)
        {
            InGameUI.SetActive(true);
        }
        GameEvents.current.onPlayerDeathEnter += destroySelf;
        GameEvents.current.onPortalTriggeredEnter += disableSelf;
    }

    void destroySelf()
    {   
        Instantiate(DeathUIPrompt);
        if(this.gameObject!=null){
            Destroy(this.gameObject);
        }
    }

    void disableSelf()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            InstructionMenu.SetActive(false);
        }
    }

    public void ResumeGame(){
        PauseMenuUI.SetActive(false);
        InstructionMenu.SetActive(false);
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        if (InGameUI != null)
        {
            InGameUI.SetActive(true);
        }
    }

    void PauseGame(){
        PauseMenuUI.SetActive(true);
        InstructionMenu.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;

        if (InGameUI != null)
        {
            InGameUI.SetActive(false);
        }
    }

    public void ShowInstruction() {
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        PauseMenuUI.SetActive(false);

        InstructionMenu.SetActive(true);
        if (InGameUI != null)
        {
            InGameUI.SetActive(false);
        }
    }
    public void HideInstruction() {
        PauseMenuUI.SetActive(true);
        InstructionMenu.SetActive(false);
        if (InGameUI != null)
        {
            InGameUI.SetActive(false);
        }
    }
    public bool ifPause(){
        return GameIsPaused;
    }
}
