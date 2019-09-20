using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeliController : MonoBehaviour
{
    public float HeliSpeed = 1;//Speed the Heli moves at
    public TextMeshProUGUI WarningText;//Text that displays a warning to to user

    HelicopterLadderController LadderController;

    void Start()
    {
        LadderController = GetComponent<HelicopterLadderController>();
        WarningText.color = new Color(255, 0, 0, 0);
    }

    void Update()
    {
        //Lerp the alpha of the text to be transparent
        WarningText.color = Color.Lerp(WarningText.color, new Color(255, 0, 0, 0), 2 * Time.deltaTime);
    }

    public void ControlHeli(Vector3 _Strength)
    {
        //Check for if ladder is up all the way
        if (LadderController.CurrentFrame == 0)
        {
            //Which direction to move
            _Strength = ((_Strength.x * transform.right) + (_Strength.y * transform.up) + (_Strength.z * transform.forward)).normalized * Time.deltaTime * -1;
            //Translate in the correct direction
            transform.Translate(_Strength * HeliSpeed);
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            //Clamp the heli within the boundaries
            transform.position = new Vector3(Mathf.Clamp(x, -15, 11), Mathf.Clamp(y, 1.5f, 3.5f), Mathf.Clamp(z, -14, 13));
        }
        else
        {
            //Change the text to display correctly
            WarningText.text = "WARNING: LADDER IS CURRENLY DEPLOYED";
            //Change the Alpha of the Warning
            WarningText.color = new Color(255, 0, 0, 1);
        }
    }

    public void Elevate(float _value)
    {
        //Gets the elevation level from the Throttle
        ControlHeli(new Vector3(0, _value, 0));
    }

}
