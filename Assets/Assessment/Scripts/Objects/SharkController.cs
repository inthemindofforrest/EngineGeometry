using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public GameObject Target;

    public float YHeightOfObject = 0;

    [Header("Rotation")]
    public float RotationSpeed = 1;
    public float Radius = 1;
    public float AngleOffset;

    float Angle;

    void Start()
    {
        //Only need to call the animator once
        GetComponentInChildren<Animator>().Play("SharkSwim", 0, Time.deltaTime + AngleOffset);//Offsets the animations from each of the sharks;
    }

    void Update()
    {
        CircleTargetObject(gameObject, Target.transform.position, YHeightOfObject);
    }

    void CircleTargetObject(GameObject _Target, Vector3 _Center, float _YHeight)
    {
        Angle += RotationSpeed * Time.deltaTime;//Increase Angle of rotation
        Vector3 Offset = new Vector3(Mathf.Sin(Angle + AngleOffset), _YHeight, Mathf.Cos(Angle + AngleOffset)) * Radius;//Calculate Point on circle depending on the Angle
        _Target.transform.position = _Center + Offset;//Move the object to the position
        transform.LookAt(_Center + new Vector3(Mathf.Sin(Angle + .01f + AngleOffset), _YHeight, Mathf.Cos(Angle + .01f + AngleOffset)) * Radius);//Look at the object 
    }
}
