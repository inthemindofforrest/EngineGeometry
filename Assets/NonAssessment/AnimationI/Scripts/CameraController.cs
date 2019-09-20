using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 5;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetKey(KeyCode.Q)?1:Input.GetKey(KeyCode.E)?-1:0) * Time.deltaTime * rotateSpeed);
    }
}
