using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveController : MonoBehaviour
{
    public float ImpactAmount = .7f;
    public float WaterHeightLimit = .4f;
    public float WaterBreakingPoint = .3f;
    public float WaterIntensity = .4f;
    MeshFilter Rend;
    Vector3[] Verts;
    Vector3[] VertsLocation;

    void Start()
    {
        Rend = GetComponent<MeshFilter>();
        Verts = Rend.mesh.vertices;
        VertsLocation = new Vector3[Verts.Length];
        VertInitLocation();
    }

    
    void Update()
    {
        //Creation of the waves
        WaterController();

        //Impact points on the waters surface
        //InitialImpact();

        //Applies all the verticies in a mesh to the Temp coords
        SetAllVerts();
    }

    void VertInitLocation()
    {
        //Gets all the verticies in the mesh currently
        for (int i = 0; i < VertsLocation.Length; i++)
            VertsLocation[i] = Verts[i];
    }

    void SetAllVerts()
    {
        //Converts Array into a List
        List<Vector3> Temp = new List<Vector3>();
        for (int i = 0; i < Verts.Length; i++)
            Temp.Add(Verts[i]);

        //SetVertices takes in a List, while getting them gives an array
        Rend.mesh.SetVertices(Temp);
    }

    void InitialImpact()
    {
        int Closest = int.MaxValue;//Inits closest point with max value
        float ClosestDis = float.MaxValue;//Inits ClosestDis with a max value
        //Itterates through all points of a mesh
        for (int i = 0; i < Rend.mesh.vertexCount; i++)
        {
            //Verts[i].y = DefaultWaterLevel;
            //Gets the distance from object to the Verticy on a mesh
            float TempDistance = Vector3.Distance(Verts[i], transform.position);
            //Checks if the distance is closer than the previous closer distance
            //And if the distance is in range
            if (TempDistance < ClosestDis && TempDistance < 1.5f)
            {
                //Set new closest distance
                ClosestDis = Vector3.Distance(Verts[i], transform.position);
                //Set new Closest Vert
                Closest = i;
                //Set the Y value of the closest vert to the impact amount
                //!!!!Why do I do this here???? This makes more sense to do after the loop?????!!!!!
                Verts[Closest].y = ImpactAmount;
            }
        }
    }

    void WaterController()
    {
        //Itterate through all the verts in an object
        for (int i = 0; i < Verts.Length; i++)
        {
            //If the object is close enough to the Target Point
            if (Vector3.Distance(VertsLocation[i], Verts[i]) < 0.1f)
            {
                //Set a new Target Point
                VertsLocation[i].y = Random.Range(0, WaterHeightLimit);
            }
            //Hold onto the current X and Z coord
            Vector2 OldXAndZ = new Vector2(Verts[i].x, Verts[i].z);
            //Slerp the Verticy to the Target Point
            Verts[i] = Vector3.Slerp(Verts[i], new Vector3(OldXAndZ.x, VertsLocation[i].y, OldXAndZ.y), WaterIntensity * Time.deltaTime);
            //Reset the X and Z coordinate of the Vert as it should have not changed.
            Verts[i].x = OldXAndZ.x;
            Verts[i].z = OldXAndZ.y;


            //TODO:: CHange the Vertex color to have a more white tint to the water
            //if(VertsLocation.y > WaterBreakingPoint)GetComponent<Renderer>().material.
        }
    }
}
