using UnityEngine;

public class GlobalOption : MonoBehaviour {
    void Awake ()
    {   
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalOption");
        if (objs.Length > 1)
        {
            Destroy(objs[1].gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}