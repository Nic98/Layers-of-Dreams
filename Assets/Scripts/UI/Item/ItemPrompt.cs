using UnityEngine;

public class ItemPrompt : MonoBehaviour {
    public float PromptDistance = 7f;
    public GameObject prompt;
    private Camera mainCamera;
    private GameObject Player;
    void Start() {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Player = GameObject.Find("Hero");
        if (Player == null || mainCamera == null)
        {   
            print("ItemPrompt: Something is not found.");
            Destroy(this);
        }
        prompt.SetActive(false);
    }

    void Update() {
        if (CalculationOfDistance(Player.transform.position) < PromptDistance)
        {
            prompt.SetActive(true);
            // prompt.transform.LookAt(mainCamera.transform); // this is somehow inverted...
            prompt.transform.rotation = mainCamera.transform.rotation;
        }
        else
        {
            prompt.SetActive(false);
        }
    }

    // from calculation function from enemy detection
    private float CalculationOfDistance(Vector3 playerPos)
    {
        float distance = Vector3.Distance(this.transform.position, playerPos);
        return distance;
    }
}