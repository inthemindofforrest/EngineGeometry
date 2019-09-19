using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterWaterEffect : MonoBehaviour
{
    public GameObject WaterParticleEffect;//Particle Effect Object

    ParticleSystem MistSystem;//Mist Particle System
    float MistMaxParticles;//Maximum particles the Mist should be
    ParticleSystem WaterSystem;//Water Spray Particle System
    float WaterMaxParticles;//Maximum Particles the Water Spray should be

    void Start()
    {
        //Get Particle Systems
        MistSystem = WaterParticleEffect.GetComponent<ParticleSystem>();
        WaterSystem = WaterParticleEffect.transform.GetChild(0).GetComponent<ParticleSystem>();

        //Get Max Particles
        //I know it is Obsolete, but I don't know why it would be, it is exactally what I need!
#pragma warning disable CS0618 // Type or member is obsolete
        MistMaxParticles = MistSystem.emissionRate;
        WaterMaxParticles = WaterSystem.emissionRate;
#pragma warning disable CS0618 // Type or member is obsolete
    }

    void Update()
    {
        //Positions the water and mist effect to be under the Helicopter
        WaterParticleEffect.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

#pragma warning disable CS0618 // Type or member is obsolete
        MistSystem.emissionRate = MistMaxParticles - ((Vector3.Distance(transform.position, WaterParticleEffect.transform.position) * 250));
        WaterSystem.emissionRate = WaterMaxParticles - ((Vector3.Distance(transform.position, WaterParticleEffect.transform.position) * 6));
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
