using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    void Awake ()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(objs[0].gameObject);
            //Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
 
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(objs[0].gameObject);
            //Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
        if (SceneManager.GetActiveScene().name == "SceneName")
        {
            Destroy(this.gameObject);
        }
    }
}
