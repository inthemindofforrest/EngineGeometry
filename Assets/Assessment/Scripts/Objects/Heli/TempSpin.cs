using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSpin : MonoBehaviour
{
    public float SpinSpeed = 1;

    void Update()
    {
        //Spins the Heli blades
        transform.Rotate(0, SpinSpeed * Time.deltaTime, 0);
    }
}
