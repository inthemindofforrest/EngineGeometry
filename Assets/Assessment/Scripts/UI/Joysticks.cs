using UnityEngine;
using UnityEngine.UI; 

public class Joysticks : MonoBehaviour
{
    public GameObject Heli;
    public float HeliSpeed = 1;

    RectTransform StickTransform;
    bool SelectedCheck = false;
    bool MouseHeld = false;

    bool ElevationButtonHeld = false;
    bool Raise = false;

    Vector3 Strength = Vector3.zero;
    void Start()
    {
        StickTransform = GetComponent<RectTransform>();
        StickTransform.localPosition = Vector3.zero;
    }

    
    void Update()
    {
        MoveJoystick();
        ElevateHeli();
    }

    public void JoystickReset()
    {
        StickTransform.localPosition = Vector3.zero;
        MouseHeld = false;
    }

    public void JoystickSelected()
    {
        if(SelectedCheck)
            MouseHeld = true;
    }

    public void SetTrigger(bool _setter)
    {
        SelectedCheck = _setter;
    }

    void ControlHeli(Vector3 _Strength)
    {
        //(All * -1 as the Heli is backwards)
        _Strength = ((_Strength.x * Heli.transform.right) + (_Strength.y * Heli.transform.up) + (_Strength.z * Heli.transform.forward)).normalized * Time.deltaTime * -1;
        Heli.transform.Translate(_Strength * HeliSpeed);
        float x = Heli.transform.position.x;
        float y = Heli.transform.position.y;
        float z = Heli.transform.position.z;
        Heli.transform.position = new Vector3(Mathf.Clamp(x, -4, 4), Mathf.Clamp(y, 1.5f, 3.5f), Mathf.Clamp(z, -5, 4));
    }

    void MoveJoystick()
    {
        if (MouseHeld)
        {
            //Gets the point in which the mouse is in the Rect
            Vector2 OuttedPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(StickTransform, Input.mousePosition, Camera.main, out OuttedPoint);
            OuttedPoint = Rect.NormalizedToPoint(StickTransform.rect, OuttedPoint);
            
            StickTransform.position = StickTransform.position + (Input.mousePosition - StickTransform.position)/* - new Vector3(OuttedPoint.x, OuttedPoint.y, 0)*/;

            Strength.x = Mathf.Clamp(StickTransform.localPosition.x/100, -1, 1);
            Strength.z = Mathf.Clamp(StickTransform.localPosition.y/100, -1, 1);
            ControlHeli(Strength);

        }
    }

    public void RaiseLower(bool _Raise)
    {
        ElevationButtonHeld = true;
        Raise = _Raise;
    }
    public void ResetElevation()
    {
        ElevationButtonHeld = false;
    }
    public void ElevateHeli()
    {
        if(ElevationButtonHeld)
            ControlHeli(new Vector3(0, (Raise) ? -1 : 1, 0));
    }
}
