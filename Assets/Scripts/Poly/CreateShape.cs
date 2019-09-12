using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CreateShape : MonoBehaviour
{
    Vector3 Position;
    Vector3 Size;

    Mesh CustomMesh;

    bool CanCreateQuad;
    bool CanCreatePentagon;
    public bool CanCreateCube;
    public bool CanCreateNGon;

    [Header("NGon Attributes")]
    public int Sides;

    MeshRenderer MyRenderer;
    [Header("Materials")]
    public Color MaterialColor;
    public Material NewMaterial;

    void Start()
    {
        Size = transform.localScale;
        Position = transform.position - (Size / 2);

        if (CanCreateQuad) CreateQuad();
        else if (CanCreatePentagon) CreatePentagon();
        else if (CanCreateCube) CreateCube();
        else if (CanCreateNGon) CreateNGon();
        MyRenderer = GetComponent<MeshRenderer>();
        SetupMaterial();
    }

    void CreateQuad()
    {
        Mesh TempMesh = new Mesh();
        TempMesh.name = "Quad";

        #region Verticies
        var Verticies = new Vector3[4];

        Verticies[0] = new Vector3(0, 0, 0);
        Verticies[1] = new Vector3(0, 1, 0);
        Verticies[2] = new Vector3(1, 0, 0);

        Verticies[3] = new Vector3(1, 1, 0);

        TempMesh.vertices = Verticies;
        #endregion

        #region Indecies
        var Indecies = new int[6];

        Indecies[0] = 0;
        Indecies[1] = 1;
        Indecies[2] = 2;

        Indecies[3] = 2;
        Indecies[4] = 1;
        Indecies[5] = 3;

        TempMesh.triangles = Indecies;
        #endregion

        #region Normals
        var Normals = new Vector3[4];
        for (int i = 0; i < Normals.Length; i++) Normals[i] = -Vector3.forward;
        TempMesh.normals = Normals;
        #endregion

        #region UVs

        var UVs = new Vector2[4];

        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(0, 1);
        UVs[2] = new Vector2(1, 0);
        UVs[3] = new Vector2(1, 1);

        TempMesh.uv = UVs;
        #endregion

        GetComponent<MeshFilter>().mesh = TempMesh;
        CustomMesh = TempMesh;
    }

    void CreatePentagon()
    {
        Mesh TempMesh = new Mesh();
        TempMesh.name = "Pentagon";

        #region Verticies
        var Verticies = new Vector3[5];

        Verticies[0] = new Vector3(2.5f, 0, 0);
        Verticies[1] = new Vector3(.5f, 0, 0);
        Verticies[2] = new Vector3(0, 1, 0);
        Verticies[3] = new Vector3(1.5f, 1.5f, 0);
        Verticies[4] = new Vector3(3, 1, 0);

        TempMesh.vertices = Verticies;
        #endregion

        #region Indecies
        var Indecies = new int[9];

        Indecies[0] = 0;
        Indecies[1] = 1;
        Indecies[2] = 2;

        Indecies[3] = 0;
        Indecies[4] = 2;
        Indecies[5] = 3;

        Indecies[6] = 0;
        Indecies[7] = 3;
        Indecies[8] = 4;

        TempMesh.triangles = Indecies;
        #endregion

        #region Normals
        var Normals = new Vector3[5];
        for (int i = 0; i < Normals.Length; i++) Normals[i] = -Vector3.forward;
        TempMesh.normals = Normals;
        #endregion

        GetComponent<MeshFilter>().mesh = TempMesh;
        CustomMesh = TempMesh;
    }

    void CreateCube()
    {
        Mesh TempMesh = new Mesh();
        TempMesh.name = "Quad";

        #region Verticies
        var Verticies = new Vector3[24];

        #region Front
        Verticies[0] = new Vector3(Position.x, Position.y, Position.z);
        Verticies[1] = new Vector3(Position.x, Position.y + Size.y, Position.z);
        Verticies[2] = new Vector3(Position.x + Size.x, Position.y, Position.z);
        Verticies[3] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);
        #endregion
        #region Back
        Verticies[4] = new Vector3(Position.x, Position.y, Position.z + Size.z);
        Verticies[5] = new Vector3(Position.x, Position.y + Size.y, Position.z + Size.z);
        Verticies[6] = new Vector3(Position.x + Size.x, Position.y, Position.z + Size.z);
        Verticies[7] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z + Size.z);
        #endregion
        #region Right
        Verticies[8] = new Vector3(Position.x + Size.x, Position.y, Position.z);//2
        Verticies[9] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);//3
        Verticies[10] = new Vector3(Position.x + Size.x, Position.y, Position.z + Size.z);//6
        Verticies[11] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z + Size.z);//7
        #endregion
        #region Left
        Verticies[12] = new Vector3(Position.x, Position.y, Position.z + Size.z);//4
        Verticies[13] = new Vector3(Position.x, Position.y + Size.y, Position.z + Size.z);//5
        Verticies[14] = new Vector3(Position.x, Position.y, Position.z);//0
        Verticies[15] = new Vector3(Position.x, Position.y + Size.y, Position.z);//1
        #endregion
        #region Top
        Verticies[16] = new Vector3(Position.x, Position.y + Size.y, Position.z);//1
        Verticies[17] = new Vector3(Position.x, Position.y + Size.y, Position.z + Size.z);//5
        Verticies[18] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);//3
        Verticies[19] = new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z + Size.z);//7
        #endregion
        #region Bottom
        Verticies[20] = new Vector3(Position.x, Position.y, Position.z);//0
        Verticies[21] = new Vector3(Position.x + Size.x, Position.y, Position.z);//2
        Verticies[22] = new Vector3(Position.x, Position.y, Position.z + Size.z);//4
        Verticies[23] = new Vector3(Position.x + Size.x, Position.y, Position.z + Size.z);//6
        #endregion

        TempMesh.vertices = Verticies;
        #endregion

        #region Indecies
        var Indecies = new int[36];

        #region Front
        Indecies[0] = 0;
        Indecies[1] = 1;
        Indecies[2] = 2;

        Indecies[3] = 2;
        Indecies[4] = 1;
        Indecies[5] = 3;
        #endregion
        #region Back
        Indecies[6] = 6;
        Indecies[7] = 7;
        Indecies[8] = 4;

        Indecies[9] = 4;
        Indecies[10] = 7;
        Indecies[11] = 5;
        #endregion
        #region Right
        Indecies[12] = 10;
        Indecies[13] = 8;
        Indecies[14] = 9;

        Indecies[15] = 10;
        Indecies[16] = 9;
        Indecies[17] = 11;
        #endregion
        #region Left
        Indecies[18] = 13;
        Indecies[19] = 15;
        Indecies[20] = 14;

        Indecies[21] = 13;
        Indecies[22] = 14;
        Indecies[23] = 12;
        #endregion
        #region Top
        Indecies[24] = 16;
        Indecies[25] = 17;
        Indecies[26] = 19;

        Indecies[27] = 16;
        Indecies[28] = 19;
        Indecies[29] = 18;
        #endregion
        #region Bottom
        Indecies[30] = 20;
        Indecies[31] = 21;
        Indecies[32] = 23;

        Indecies[33] = 20;
        Indecies[34] = 23;
        Indecies[35] = 22;
        #endregion



        TempMesh.triangles = Indecies;
        #endregion

        #region Normals
        var Normals = new Vector3[24];
        #region Front
        Normals[0] = -transform.forward;
        Normals[1] = -transform.forward;
        Normals[2] = -transform.forward;
        Normals[3] = -transform.forward;
        #endregion
        #region Back
        Normals[4] = transform.forward;
        Normals[5] = transform.forward;
        Normals[6] = transform.forward;
        Normals[7] = transform.forward;
        #endregion
        #region Right
        Normals[8] = transform.right;
        Normals[9] = transform.right;
        Normals[10] = transform.right;
        Normals[11] = transform.right;
        #endregion
        #region Left
        Normals[12] = -transform.right;
        Normals[13] = -transform.right;
        Normals[14] = -transform.right;
        Normals[15] = -transform.right;
        #endregion
        #region Top
        Normals[16] = transform.up;
        Normals[17] = transform.up;
        Normals[18] = transform.up;
        Normals[19] = transform.up;
        #endregion
        #region Bottom
        Normals[20] = -transform.up;
        Normals[21] = -transform.up;
        Normals[22] = -transform.up;
        Normals[23] = -transform.up;
        #endregion
        TempMesh.normals = Normals;
        #endregion

        #region UVs

        var UVs = new Vector2[24];

        #region Front
        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(0, 1);
        UVs[2] = new Vector2(1, 0);
        UVs[3] = new Vector2(1, 1);
        #endregion
        #region Back
        UVs[4] = new Vector2(1, 0);
        UVs[5] = new Vector2(1, 1);
        UVs[6] = new Vector2(0, 0);
        UVs[7] = new Vector2(0, 1);
        #endregion
        #region Right
        UVs[8] = new Vector2(0, 0);
        UVs[9] = new Vector2(0, 1);
        UVs[10] = new Vector2(1, 0);
        UVs[11] = new Vector2(1, 1);
        #endregion
        #region Left
        UVs[12] = new Vector2(0, 0);
        UVs[13] = new Vector2(0, 1);
        UVs[14] = new Vector2(1, 0);
        UVs[15] = new Vector2(1, 1);
        #endregion
        #region Top
        UVs[16] = new Vector2(0, 0);
        UVs[17] = new Vector2(0, 1);
        UVs[18] = new Vector2(1, 0);
        UVs[19] = new Vector2(1, 1);
        #endregion
        #region Bottom
        UVs[20] = new Vector2(1, 0);
        UVs[21] = new Vector2(0, 0);
        UVs[22] = new Vector2(1, 1);
        UVs[23] = new Vector2(0, 1);
        #endregion
        TempMesh.uv = UVs;
        #endregion


        GetComponent<MeshFilter>().mesh = TempMesh;
        CustomMesh = TempMesh;
    }

    void CreateNGon()
    {
        Mesh TempMesh = new Mesh();
        TempMesh.name = "NGon";

        #region Verticies
        var Verticies = new Vector3[4];

        Verticies[0] = new Vector3(0, 0, 0);
        Verticies[1] = new Vector3(0, 1, 0);
        Verticies[2] = new Vector3(1, 0, 0);

        Verticies[3] = new Vector3(1, 1, 0);

        TempMesh.vertices = Verticies;
        #endregion

        #region Indecies
        var Indecies = new int[6];

        Indecies[0] = 0;
        Indecies[1] = 1;
        Indecies[2] = 2;

        Indecies[3] = 2;
        Indecies[4] = 1;
        Indecies[5] = 3;

        TempMesh.triangles = Indecies;
        #endregion

        #region Normals
        var Normals = new Vector3[4];
        for (int i = 0; i < Normals.Length; i++) Normals[i] = -Vector3.forward;
        TempMesh.normals = Normals;
        #endregion

        #region UVs

        var UVs = new Vector2[4];

        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(0, 1);
        UVs[2] = new Vector2(1, 0);
        UVs[3] = new Vector2(1, 1);

        TempMesh.uv = UVs;
        #endregion

        GetComponent<MeshFilter>().mesh = TempMesh;
        CustomMesh = TempMesh;
    }

    void SetupMaterial()
    {
        MyRenderer.material.color = MaterialColor;
        MyRenderer.material = NewMaterial;
    }

    private void OnDestroy()
    {
        if(CustomMesh != null)
        {
            Destroy(CustomMesh);
        }
    }
}

