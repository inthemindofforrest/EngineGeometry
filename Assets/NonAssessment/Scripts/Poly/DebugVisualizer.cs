using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVisualizer : MonoBehaviour
{
    MeshFilter Filter;

    void Start()
    {
        Filter = GetComponent<MeshFilter>();
    }

    private void OnDrawGizmos()
    {
        if (Filter && Filter.mesh != null)
        {
            var Verticies = Filter.mesh.vertices;
            for (int i = 0; i < Verticies.Length; i++)
            {
                //Gizmos.DrawSphere(transform.TransformPoint(Verticies[i]), .1f);
                Gizmos.DrawSphere(transform.TransformPoint(Verticies[i]), .05f);
            }
        }
    }
}
