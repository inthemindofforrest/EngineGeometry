/*
 Joysticks - Main Steering for the Helicopter
 Function - Takes in a function from the Helicopter and gives it a direction to move
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Creating a Vector3 Event to accept a Vector3 as a parameter
[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }

public class Joysticks : MonoBehaviour
{
    public Vector3Event JoystickDirection;//The function that will be effected by the joystick

    #region RectTransforms
    RectTransform HandleTransform;//The RectTransform of the Child Handle
    RectTransform ParentTransform;//The RectTransform of the Parent of Handle
    #endregion

    bool HandleGrabbed;//Whether the user is grabbing the Joystick

    Vector3 Strength = Vector3.zero;//The Direction the joystick is currently facing

    void Start()
    {
        //Parent needs to be First Child
        ParentTransform = transform.GetChild(0).GetComponent<RectTransform>();
        //Handle needs to be Parents First Child
        HandleTransform = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        HandleTransform.localPosition = Vector3.zero;
    }
    
    void Update()
    {
        //Update the Joystick
        JoystickUpdate();
    }

    void JoystickUpdate()
    {
        //Moves the joystick to correct position when grabbed
        MoveJoystick();
        //Set Parameter in the Vector3Event to Strength
        if(HandleGrabbed)JoystickDirection.Invoke(Strength);
    }

    public void JoystickReset()
    {
        //Resets all the parameters for the Joystick
        HandleGrabbed = false;
        HandleTransform.localPosition = Vector3.zero;
        Strength = Vector3.zero;
    }

    public void JoystickGrabbed(bool _Grab)
    {
        //Sets the grabbed state
        HandleGrabbed = _Grab;
    }

    void MoveJoystick()
    {
        //If Handle is being grabbed
        if (HandleGrabbed)
        {
            //Get offset distance of the Mouse and ParentTransform
            Vector2 Distance = (Input.mousePosition - ParentTransform.position);
            //Set the X and Y Offsets to snap to locations
            float XOffset = (Distance.x < -50) ? -100 : (Distance.x > 50) ? 100 : 0;
            float YOffset = (Distance.y < -50) ? -100 : (Distance.y > 50) ? 100 : 0;

            //Set the transform of the Handle to the correct offsets
            HandleTransform.localPosition = new Vector3(XOffset, YOffset, 0);

            //Set the Strength
            Strength.x = Mathf.Clamp(HandleTransform.localPosition.x/100, -1, 1);
            Strength.z = Mathf.Clamp(HandleTransform.localPosition.y/100, -1, 1);
        }
    }

}
