using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterWaterEffect : MonoBehaviour
{
    public GameObject WaterParticleEffect;


    
    void Update()
    {
        WaterParticleEffect.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
