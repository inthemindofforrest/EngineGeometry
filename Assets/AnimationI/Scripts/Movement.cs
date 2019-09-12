using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;
    public GameObject CameraParent;

    Animator Anim;
    Vector3 MoveDirection;

    float TargetSpeed;
    float CurrentSpeed;
    
    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetFloat("Direction", 1);
        MoveDirection = transform.forward * 2;
    }
    void Update()
    {
        ChangeWalkingSpeed();
        ChangeDirection();
        Jumping();
        ActuallyMove();
    }

    void ActuallyMove()
    {
        transform.position += ((MoveDirection + transform.forward).normalized * Anim.GetFloat("Speed") * speed * Time.deltaTime);
    }
    void ChangeWalkingSpeed()
    {
        CurrentSpeed = Anim.GetFloat("Speed");
        if (Input.GetKey(KeyCode.LeftShift) && MovementKeysHeld()) TargetSpeed = 1;
        else if (MovementKeysHeld()) TargetSpeed = .3f;
        else TargetSpeed = 0;

        if (TargetSpeed > CurrentSpeed) CurrentSpeed += Time.deltaTime;
        else if (TargetSpeed < CurrentSpeed) CurrentSpeed -= Time.deltaTime;

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, 1);
        Anim.SetFloat("Speed", CurrentSpeed);
    }
    void ChangeDirection()
    {
        #region Direction
        Vector3 Direction = Vector3.zero;
        float DirectionValue = 0;
        Direction += new Vector3(0, 0, Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0);
        Direction += new Vector3(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0, 0, 0);

        DirectionValue = Direction.z;
        if (DirectionValue != 0) DirectionValue += Direction.x / 2;
        else DirectionValue = (Direction.x > 0) ? 2 : 0;

        float CurrentDirection = Anim.GetFloat("Direction");
        Anim.SetFloat("Direction", (DirectionValue > CurrentDirection) ? CurrentDirection + Time.deltaTime : CurrentDirection - Time.deltaTime);
        #endregion
        if (!MovementKeysHeld())
        {
            CurrentDirection = Anim.GetFloat("Direction");
            Anim.SetFloat("Direction", (CurrentDirection > 1) ? CurrentDirection -= Time.deltaTime : CurrentDirection += Time.deltaTime);
        }
        MoveDirection = Direction;
    }
    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurrentSpeed = TargetSpeed = 0;
            Anim.SetFloat("Speed", 0);
            Anim.SetTrigger("Jump");
        }
    }

    bool MovementKeysHeld()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
    }
}
