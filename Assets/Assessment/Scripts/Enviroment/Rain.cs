using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameObject Ground;
    public float ImpactAmount = .7f;
    public float WaterHeightLimit = .4f;
    public float WaterIntensity = .4f;
    MeshFilter Rend;
    Vector3[] Verts;
    Vector3[] VertsLocation;

    void Start()
    {
        Rend = Ground.GetComponent<MeshFilter>();
        Verts = Rend.mesh.vertices;
        VertsLocation = new Vector3[Verts.Length];
        VertInitLocation();
    }

    
    void Update()
    {
        Controller();

        //InitialImpact();

        SetAllVerts();
    }

    void VertInitLocation()
    {
        for (int i = 0; i < VertsLocation.Length; i++)
            VertsLocation[i] = Verts[i];
    }

    void SetAllVerts()
    {
        List<Vector3> Temp = new List<Vector3>();
        for (int i = 0; i < Verts.Length; i++)
            Temp.Add(Verts[i]);

        Rend.mesh.SetVertices(Temp);
    }

    void InitialImpact()
    {
        int Closest = int.MaxValue;
        float ClosestDis = float.MaxValue;
        for (int i = 0; i < Rend.mesh.vertexCount; i++)
        {
            //Verts[i].y = DefaultWaterLevel;
            float TempDistance = Vector3.Distance(Verts[i], transform.position);
            if (TempDistance < ClosestDis && TempDistance < 1.5f)
            {
                ClosestDis = Vector3.Distance(Verts[i], transform.position);
                Closest = i;
                Verts[Closest].y = ImpactAmount;
            }
        }
    }

    void Controller()
    {
        for (int i = 0; i < Verts.Length; i++)
        {
            if (Vector3.Distance(VertsLocation[i], Verts[i]) < 0.1f)
            {
                VertsLocation[i].y = Random.Range(0, WaterHeightLimit);
            }
            Vector2 OldXAndZ = new Vector2(Verts[i].x, Verts[i].z);
            Verts[i] = Vector3.Slerp(Verts[i], new Vector3(OldXAndZ.x, VertsLocation[i].y, OldXAndZ.y), WaterIntensity * Time.deltaTime);
            Verts[i].x = OldXAndZ.x;
            Verts[i].z = OldXAndZ.y;
        }
    }
}
