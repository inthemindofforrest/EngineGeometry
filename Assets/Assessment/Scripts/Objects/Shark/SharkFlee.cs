using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkFlee : MonoBehaviour
{
    [Header("Locations")]
    public GameObject ShipWreck;//Shipwreck GameObject
    public GameObject Boat;//Stranded Person GameObject

    [Header("Distance Objects")]
    public GameObject Helicopter;//Helicopter GameObject

    GameObject CurrentLocation;//Current Target Location
    SharkController SController;//Gets the current Shark controller
    Vector3 TargetPosition;//Target position to circle

    Animator Anim;

    public float SwimSpeed;//How fast to swim
    public float EncounterDistance;//How close to get to target befor circling

    void Start()
    {
        SController = GetComponent<SharkController>();
        Anim = GetComponentInChildren<Animator>();
        CurrentLocation = Boat;    
    }

    void Update()
    {
        //Check whether to flee or stay
        FleeUpdate();
    }

    void FleeUpdate()
    {
        //Determines the correct Destination depending on Distance of heli to Person
        CurrentLocation = DistanceChecks();
        //Move to the correct location
        MoveTowardsLocation();
    }

    GameObject DistanceChecks()
    {
        //Returns which location the shark needs to be moving towards
        return (Vector3.Distance(Helicopter.transform.position, Boat.transform.position) < EncounterDistance)? ShipWreck : Boat;
    }
    void MoveTowardsLocation()
    {
        //If the distance of the shark is further than Radius plus Little bit
        if(Vector3.Distance(transform.position, CurrentLocation.transform.position) > SController.Radius + .1f)
        {
            //Set the Target circle location to the new location
            SController.Target = CurrentLocation;
            //Disable the Circling mechanic
            SController.enabled = false;

            Vector3 Offset = new Vector3(Mathf.Sin(SController.AngleOffset), 0, Mathf.Cos(SController.AngleOffset) * SController.Radius);//Calculate Point on circle depending on the Angle
            TargetPosition = CurrentLocation.transform.position /*+ Offset*/;//Move the object to the position

            //Find direction for the shark to move to get to the correct position
            Vector3 TempTarget = (TargetPosition - transform.position).normalized;
            //Look at Target
            transform.LookAt(transform.position + TempTarget);
            //Move the shark to the correct Lerped position
            transform.position = transform.position + (TempTarget * SwimSpeed * Time.deltaTime);
            //Blend between the two animations
            Anim.SetFloat("SwimBlend", Anim.GetFloat("SwimBlend"), 1, 0.1f);
        }
        else
        {
            //Re enable the Circling Mechanic
            SController.enabled = true;
            //Blend between animations
            Anim.SetFloat("SwimBlend", Anim.GetFloat("SwimBlend"), 0, 0.1f);
        }
        //Update the Radius depending on the two locations
        SController.Radius = (CurrentLocation == Boat) ? 2.5f : 5;
    }
}
