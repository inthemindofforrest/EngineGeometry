using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFade : MonoBehaviour
{
    Renderer MyRenderer;
    public bool State;

    void Start()
    {
        MyRenderer = GetComponent<Renderer>();
    }

    
    void Update()
    {
        if (State)
        {
            MyRenderer.material.color += new Color(0,0,0,Time.deltaTime * .01f);
        }
        else
        {
            MyRenderer.material.color -= new Color(0,0,0,Time.deltaTime * .01f);
        }
        MyRenderer.material.color = new Color(MyRenderer.material.color.r, MyRenderer.material.color.g, MyRenderer.material.color.b, Mathf.Clamp(MyRenderer.material.color.a, 0, 255));
    }
}
