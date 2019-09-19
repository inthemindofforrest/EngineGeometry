using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterLadderController : MonoBehaviour
{
    Animator LadderAnimator;//Animator for Ladder
    [SerializeField]//Want to see in Inspector
    float CurrentFrame = 0;//Current Animation frame 

    private void Start()
    {
        //Ladder needs to be in the second location
        //Animation needs to be first child of ladder
        LadderAnimator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        //Sets the Animator to the correct Current Frame
        LadderAnimator.Play("Deploy", 0, CurrentFrame);
    }

    public void DeployingLadder(float _Value)
    {
        //Gets the ladder Deployment from the Throttle
        CurrentFrame = Mathf.Clamp(_Value, 0, 1);
    }
}
