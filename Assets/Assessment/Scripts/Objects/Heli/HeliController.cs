using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour
{
    public float HeliSpeed = 1;//Speed the Heli moves at

    public void ControlHeli(Vector3 _Strength)
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

    public void Elevate(float _value)
    {
        //Gets the elevation level from the Throttle
        ControlHeli(new Vector3(0, _value, 0));
    }

}
