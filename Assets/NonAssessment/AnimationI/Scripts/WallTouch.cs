using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTouch : MonoBehaviour
{
    RaycastHit hit;
    Animator Anim;

    public bool ActiveIK;

    GameObject TempHandHolder;
    GameObject TempRightHand;
    GameObject TempLeftHand;

    void Start()
    {
        Anim = GetComponent<Animator>();
        CreateFakeHands();
    }

    
    void Update()
    {
        
    }

    private void OnAnimatorIK()
    {
        if (Anim && Physics.Raycast(Anim.GetIKPosition(AvatarIKGoal.RightHand), transform.right, out hit, .3f))
        {
            if (ActiveIK)
            {
                #region RightArm
                Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                Anim.SetIKPosition(AvatarIKGoal.RightHand, hit.point + new Vector3(0, .5f, 0));

                //Region needed to get the correct rotation for the players hand
                #region TempGameObject
                TempRightHand.transform.position = Anim.GetIKPosition(AvatarIKGoal.RightHand);
                TempRightHand.transform.LookAt(hit.transform.position + Vector3.Cross(hit.point - TempRightHand.transform.position, hit.transform.right));
                #endregion

                Anim.SetIKRotation(AvatarIKGoal.RightHand, TempRightHand.transform.rotation);
                #endregion
            }
            else
            {
                Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }

        if (Anim && Physics.Raycast(Anim.GetIKPosition(AvatarIKGoal.LeftHand), -transform.right, out hit, .3f))
        {
            if (ActiveIK)
            {
                #region LeftArm
                Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                Anim.SetIKPosition(AvatarIKGoal.LeftHand, hit.point + new Vector3(0, .5f, 0));

                //Region needed to get the correct rotation for the players hand
                #region TempGameObject
                TempLeftHand.transform.position = Anim.GetIKPosition(AvatarIKGoal.LeftHand);
                TempLeftHand.transform.LookAt(hit.transform.position + Vector3.Cross(hit.point - TempLeftHand.transform.position, hit.transform.right));
                #endregion

                Anim.SetIKRotation(AvatarIKGoal.LeftHand, TempLeftHand.transform.rotation);
                #endregion
            }
            else
            {
                Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }


    void CreateFakeHands()
    {
        TempHandHolder = new GameObject("FakeHand");
        TempRightHand = new GameObject("TempRightHand");
        TempRightHand.transform.SetParent(TempHandHolder.transform);
        TempLeftHand = new GameObject("TempLeftHand");
        TempLeftHand.transform.SetParent(TempHandHolder.transform);
    }
}
