using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDimming : MonoBehaviour
{
    public float IntensityMaximum = 1;
    public float TransitionSpeed = 1;
    public bool State = false;


    Light LightComponent;

    void Start()
    {
        LightComponent = GetComponent<Light>();
    }

    
    void Update()
    {
        if(State)
        {
            LightComponent.intensity += TransitionSpeed * Time.deltaTime;
        }
        else
        {
            LightComponent.intensity -= TransitionSpeed * Time.deltaTime;
        }
        LightComponent.intensity = Mathf.Clamp(LightComponent.intensity, 0, IntensityMaximum);
    }
}
