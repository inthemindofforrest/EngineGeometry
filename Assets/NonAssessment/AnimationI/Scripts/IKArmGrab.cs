using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKArmGrab : MonoBehaviour
{
    Animator Anim;
    public bool ActiveIK;
    public GameObject RightHandObject;
    public GameObject LeftHandObject;
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnAnimatorIK()
    {
        if(Anim)
        {
            if(ActiveIK)
            {
                #region RightArm
                Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                Anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandObject.transform.position);
                Anim.SetIKRotation(AvatarIKGoal.RightHand, RightHandObject.transform.rotation);
                #endregion
                #region LeftArm
                Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                Anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandObject.transform.position);
                Anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandObject.transform.rotation);
                #endregion
            }
            else
            {
                Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }
}
