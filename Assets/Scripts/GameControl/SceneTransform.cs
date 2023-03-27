using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using General;

public class SceneTransform : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public float DeathAnimationCoolDownTime;
    private HandyCoolDown DeathAnimationCoolDown;
    void Start() {
        GameEvents.current.onPortalTriggeredEnter += LoadNextLevel;
        GameEvents.current.onPlayerDeathEnter += LoadAgainCountDown;
        GameEvents.current.onBackToMainMenuTriggerEnter += LoadMainMenu;
        DeathAnimationCoolDown = null;
    }
    public void LoadNextLevel()
    {   
        if(GameObject.Find("Main Camera")){
            GameObject.Find("Main Camera").GetComponent<TransitionShaderEffect>().Dreaming();
        }
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {   
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        //yield return null;
    }
    public void QuitGame(){
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
         #endif
    }
    public void LoadAgainCountDown()
    {
        DeathAnimationCoolDown = new HandyCoolDown(DeathAnimationCoolDownTime, "Player Death Animation Cool Down");
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
    void Update() {
        if (DeathAnimationCoolDown != null)
        {
            bool done = DeathAnimationCoolDown.check();
            if (done)
            {   
                // player is dead, and count down is done, so reload the current level
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
                DeathAnimationCoolDown = null;
            }
        }
    }
}
