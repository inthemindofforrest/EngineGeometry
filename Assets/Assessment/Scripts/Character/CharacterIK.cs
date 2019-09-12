using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    Animator Anim;
    public bool CanIK = false;
    public GameObject RightHandTarget;
    public GameObject LeftHandTarget;



    void Start()
    {
        Anim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }


    void OnAnimatorIK()
    {
        if (CanIK)
        {
            Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            Anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.transform.position);
            Anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.transform.rotation);
        }
        else
        {
            Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
}
