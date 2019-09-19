using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkParent:MonoBehaviour
{
    float RotationSpeed = 1;//Speed at which to rotate
    public float Angle;//Angle of Rotation
    private void Update()
    {
        //This is created in order to keep all the sharks in sink where they should be within their Circles
        Angle += RotationSpeed * Time.deltaTime;//Increase Angle of rotation
    }

}

public class SharkController : MonoBehaviour
{
    public GameObject Target;//Target Object to rotate around

    public float YHeightOfObject = 0;//The Target Y position

    [Header("Rotation")]
    public float Radius = 1;//Radius at which to rotate
    public float AngleOffset;//Offset of the time in the position

    static SharkParent SharkParentParam;//Gets the Parents Parameters for Synced Rotation 

    void Start()
    {
        //Adds a SharkParent component once to the parent
        if (transform.parent.GetComponent<SharkParent>() == null) SharkParentParam = transform.parent.gameObject.AddComponent<SharkParent>();

        //Only need to call the animator once
        GetComponentInChildren<Animator>().Play("SharkSwim", 0, Time.deltaTime + AngleOffset);//Offsets the animations from each of the sharks;
    }

    void Update()
    {
        //Circles the Target
        CircleTargetObject(gameObject, Target.transform.position, YHeightOfObject);
    }

    void CircleTargetObject(GameObject _Target, Vector3 _Center, float _YHeight)
    {
        //Creates an offset of where it needs to be on the circle 
        Vector3 Offset = new Vector3(Mathf.Sin(SharkParentParam.Angle + AngleOffset), _YHeight, Mathf.Cos(SharkParentParam.Angle + AngleOffset)) * Radius;//Calculate Point on circle depending on the Angle
        //Finds the location of where the point is located after adding the origin
        _Target.transform.position = _Center + Offset;//Move the object to the position
        //Look in the direction of movement
        transform.LookAt(_Center + new Vector3(Mathf.Sin(SharkParentParam.Angle + .01f + AngleOffset), _YHeight, Mathf.Cos(SharkParentParam.Angle + .01f + AngleOffset)) * Radius);//Look at the object 
    }
}
