using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingTexture : MonoBehaviour
{
    Renderer MyRenderer;

    void Start()
    {
        MyRenderer = GetComponent<Renderer>();
        MyRenderer.material.mainTextureScale = new Vector2(1,1);
        
    }

    
    void Update()
    {
        MyRenderer.material.mainTextureOffset += new Vector2(Mathf.Sin(Time.deltaTime) * .001f, Mathf.Cos(Time.deltaTime) * .001f);

    }
}
