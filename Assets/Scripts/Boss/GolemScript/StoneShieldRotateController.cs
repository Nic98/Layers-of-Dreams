using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShieldRotateController : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.RotateAround(this.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
