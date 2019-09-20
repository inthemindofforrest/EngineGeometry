using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlending : MonoBehaviour
{
    public GameObject Helicopter;//Helicopter Object
    HelicopterLadderController DeployedLadder;//The script with the ladder on it
    HeliExtraction Extraction;//The script for the Heli Extraction
    public GameObject PostProccessing;
    Lightning LightningEffect;

    public float Multiplier;//How much the Distance will be divided

    Animator Anim;
    void Start()
    {
        Anim = GetComponent<Animator>();
        DeployedLadder = Helicopter.GetComponent<HelicopterLadderController>();
        Extraction = Helicopter.GetComponent<HeliExtraction>();
        LightningEffect = PostProccessing.GetComponent<Lightning>();
    }

    void Update()
    {
        //Update the blending
        Blending();
    }

    void Blending()
    {
        //Get the distance from the heli to the character
        float CurrentBlend = Mathf.Clamp(1 - DistanceCalcXZ(gameObject, Helicopter) / Multiplier, 0, 1);

        //Check to see if heli is close enough
        if(CurrentBlend == 1)
        {
            //Add the ladders current frame
            CurrentBlend += DeployedLadder.CurrentFrame;
        }
        
        //Sets the blend variable to the Max value minus Distance from heli
        SetBlendVariable(CurrentBlend);

        //If the animation is set to grab the Heli
        if(CurrentBlend == 2)
        {
            HopOnHeli();
        }
    }

    void SetBlendVariable(float _Value)
    {
        //Sets the Blend Variable to a value
        Anim.SetFloat("Blend", _Value);
    }

    void HopOnHeli()
    {
        //Cause an insane Strike to hide Character from user
        LightningEffect.InsaneStrike();
        //Let's extract
        Extraction.TimeToLeave = true;
        //Hide survivor
        gameObject.SetActive(false);
    }

    float DistanceCalcXZ(GameObject _Obj1, GameObject _Obj2)
    {
        //Getting the positions of both objects
        Vector3 TempObj1 = _Obj1.transform.position;
        Vector3 TempObj2 = _Obj2.transform.position;

        //Setting the y values to the same value
        TempObj1.y = TempObj2.y = 0;

        //Gets the distance between two objects
        float TempDistance = Vector3.Distance(TempObj1, TempObj2);

        //Check to see if it is close enough
        return (TempDistance / Multiplier < 0.15f)? 0 : TempDistance;
    }

    float DistanceCalcY(GameObject _Obj1, GameObject _Obj2)
    {
        //Getting the positions of both objects
        Vector3 TempObj1 = _Obj1.transform.position;
        Vector3 TempObj2 = _Obj2.transform.position;

        //Setting the X and Z values to the same value
        TempObj1.x = TempObj2.x = TempObj1.z = TempObj2.z = 0;

        //Gets the distance between two objects
        float TempDistance = Vector3.Distance(TempObj1, TempObj2);

        //Check to see if it is close enough
        return (TempDistance / Multiplier < 0.15f) ? 0 : TempDistance;
    }
}
