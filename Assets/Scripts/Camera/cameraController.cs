using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float y_distance;
    public float z_distance;

    public float pitch;
    public float smooth= 5.0f;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {   
        this.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        transform.Rotate(-pitch, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0.0f, y_distance, -z_distance), Time.deltaTime * smooth);
        Cursor.lockState = CursorLockMode.Confined;
    }
}