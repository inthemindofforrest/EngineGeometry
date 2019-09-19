using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingTexture : MonoBehaviour
{
    public float SlideMultiplier = 0.001f;

    Renderer MyRenderer;

    void Start()
    {
        MyRenderer = GetComponent<Renderer>();
        MyRenderer.material.mainTextureScale = new Vector2(1,1);
    }

    
    void Update()
    {
        //Moves the texture of the water
        MyRenderer.material.mainTextureOffset += new Vector2(Mathf.Sin(Time.deltaTime) * SlideMultiplier, Mathf.Cos(Time.deltaTime) * SlideMultiplier);
    }
}
