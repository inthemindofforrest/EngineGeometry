using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float>{ }

public class SliderThrottle : MonoBehaviour
{
    public FloatEvent SetToValue;//Function to munipulate

    #region HandleDate
    enum HANDLEPOSITION {CENTER, UP, DOWN}//All possible Handle Positions
    RectTransform HandleTransform;//Handle Transform
    RectTransform ParentTransform;//Parents Transform
    #endregion

    public bool AllowNegative = false;//Allow Value to go to -1 instead of clamping at 0
    public bool ReleaseReset = false;//Reset Joystick to Value of 0 instantly when let go

    [Range(-1,1)]//Clamp value in Inspector
    public float Value;//Value of the Throttle

    bool HandleGrabbed;//Whether the Handle is being grabbed
    [SerializeField]//Want to show in inspector without making it public
    HANDLEPOSITION ThrottlePositon;//The current Throttle location

    void Start()
    {
        //Parent needs to be first child
        ParentTransform = transform.GetChild(0).GetComponent<RectTransform>();
        //Handle needs to be first child in Parent
        HandleTransform = transform.GetChild(0).GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        //Update the throttle
        ThrottleUpdate();
    }

    void ThrottleUpdate()
    {
        //Ajusts the position of the Throttle
        AjustThrottle();
        //Ajusts the Value according to the Throttle
        AjustValue();
        //Set Parameter in the FloatEvent to Value
        if(HandleGrabbed)SetToValue.Invoke(Value);
        //Checks if the throttle needs to be reset
        ThrottleReset();
        //Clamp the Slider between -1/0 to 1
        ValueLimits((AllowNegative) ? -1 : 0, 1);
    }

    void ThrottleReset()
    {
        //If the handle is not being grabbed
        if(!HandleGrabbed)
        {
            //Re center the Position of the Handle
            ThrottlePositon = HANDLEPOSITION.CENTER;
        }
        //If ReleaseReset it Active and the handle hits the ceneter Position
        if (ReleaseReset && ThrottlePositon == HANDLEPOSITION.CENTER)
        {
            //Reset the value to Zero
            Value = 0;
        }
    }
    void ValueLimits(float _Min, float _Max)
    {
        //Clamp the Value between a min and max
        Value = Mathf.Clamp(Value, _Min, _Max);
    }
    void AjustThrottle()
    {
        //If the handle is being grabbed
        if(HandleGrabbed)
        {
            //Get the default position of the handle
            Vector3 DefaultPosition = new Vector3(HandleTransform.localPosition.x, 0, HandleTransform.localPosition.z);
            //Get the offset of the mouse compared to the parent
            float DifferenceInY = ParentTransform.position.y - Input.mousePosition.y;
            //Handle's position is now the Default position plus the offset
            HandleTransform.localPosition = DefaultPosition + new Vector3(0, ((DifferenceInY > 100) ? -100 : (DifferenceInY < -100) ? 100 : 0), 0);
            //Sets the Throttles positioning
            ThrottlePositon = (DifferenceInY > 100) ? HANDLEPOSITION.DOWN : (DifferenceInY < -100) ? HANDLEPOSITION.UP : HANDLEPOSITION.CENTER;
        }
        else
        {
            //Put handle back in the center 
            HandleTransform.localPosition = Vector3.zero;
        }
    }
    void AjustValue()
    {
        //Change the Value depending on the Handle position
        Value += ((ThrottlePositon == HANDLEPOSITION.UP) ? -1 : (ThrottlePositon == HANDLEPOSITION.DOWN) ? 1 : 0) * Time.deltaTime;
    }
    public void GrabbedHandle(bool _True)
    {
        //Set whether handle is being grabbed
        HandleGrabbed = _True;
    }
}
