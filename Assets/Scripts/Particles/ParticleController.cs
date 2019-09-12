using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct ParticleAtts
{
    Transform Direction;
}

[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
    #region Public 
    [Header("Particle Attributes")]
    public float Speed = 1;
    public bool AlwaysFlame = false;
    #endregion
    #region Positional Data
    Vector3 MousePositionWorldView = Vector3.zero;
    float PositionOffset = 0.0f;
    Camera MainCamera;
    #endregion
    #region Particle System Data
    ParticleSystem PS;
    ParticleSystem.Particle[] Particles;
    ParticleAtts[] ParticleAttributes;
    #endregion

    private void Start()
    {
        PS = GetComponent<ParticleSystem>();
        MainCamera = Camera.main;
    }
    private void Update()
    {
        EmissionController();
        MoveParticlesToMouse();
        CheckCollision();
    }
    void EmissionController()
    {
        var Temp = PS.emission;
        Temp.enabled = (Input.GetMouseButton(0) || AlwaysFlame);
    }
    void GetAllParticles()
    {
        Particles = new ParticleSystem.Particle[PS.particleCount];
        PS.GetParticles(Particles);
    }
    void CreateParticleOffset()
    {
        //Changes the PositionOffset depending on ScrollWheel
        PositionOffset += (Input.GetAxis("Mouse ScrollWheel"));
    }
    void GetMousePosition()
    {
        //Sets the target position to be the y position of the mouse position
        if (Input.GetMouseButton(0) || AlwaysFlame)
            MousePositionWorldView = MainCamera.ScreenToWorldPoint(Input.mousePosition + MainCamera.gameObject.transform.position).y * new Vector3(0, 1, 0);
        CreateParticleOffset();

        //Wind of X, Pushes all particles with wind force depending on where the mouse is on the X coordinate of the screen
         Vector3 WindOfX = MainCamera.ScreenToWorldPoint(Input.mousePosition + MainCamera.gameObject.transform.position).x * new Vector3(1, 0, 0);

        //Adds the offset, and makes the flame always move up
        MousePositionWorldView = new Vector3(WindOfX.x, Mathf.Abs(MousePositionWorldView.y), PositionOffset);
    }
    void CheckCollision()
    {
        var PSColision = PS.collision;
        PSColision.enabled = !(Input.GetKey(KeyCode.Space)); 
    }
    void MoveParticlesToMouse()
    {
        GetAllParticles();
        GetMousePosition();

        for (int i = 0; i < Particles.Length; i++)
            Particles[i].position += MousePositionWorldView.normalized * Time.deltaTime * Speed;

        PS.SetParticles(Particles);
    }
}
