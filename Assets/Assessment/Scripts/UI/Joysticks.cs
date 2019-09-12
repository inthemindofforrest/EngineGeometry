using UnityEngine;
using UnityEngine.UI; 

public class Joysticks : MonoBehaviour
{
    public GameObject Heli;
    public float HeliAltSpeed = 1;
    public float HeliMovSpeed = 1;

    RectTransform StickTransform;
    bool SelectedCheck = false;
    bool MouseHeld = false;

    public Vector3 Strength = Vector3.zero;
    void Start()
    {
        StickTransform = GetComponent<RectTransform>();
        StickTransform.localPosition = Vector3.zero;
    }

    
    void Update()
    {
        MoveJoystick();
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
        Heli.transform.Translate(Strength.x * Heli.transform.forward * Time.deltaTime * -1);
        Heli.transform.Translate(Strength.y * Heli.transform.up * Time.deltaTime * -1);
        Heli.transform.Translate(Strength.z * Heli.transform.right * Time.deltaTime * -1);

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

            StickTransform.position = Input.mousePosition + new Vector3(OuttedPoint.x, OuttedPoint.y, 0);

            Strength.x = Mathf.Clamp(StickTransform.localPosition.x, -1, 1);
            Strength.z = Mathf.Clamp(StickTransform.localPosition.y, -1, 1);
            ControlHeli(Strength);

        }
    }

    public void RaiseLower(bool _Raise)
    {
        ControlHeli(new Vector3(0, (_Raise) ? 1 : -1, 0));
    }
}
