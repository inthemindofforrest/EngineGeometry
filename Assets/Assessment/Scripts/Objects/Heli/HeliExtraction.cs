using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliExtraction : MonoBehaviour
{
    public GameObject UIDisplay;//All UI elements
    public GameObject SecondCamera;//Second camera in the scene
    //public GameObject HeliCharacter;
    GameObject MainCamera;//Camera attached to the Heli

    public bool TimeToLeave = false;//Whether the Heli will leave or not
    [SerializeField]
    float Speed = 1;//How fast the heli will fly away
    [SerializeField]
    float DelayTime = 2;//Timer for how long the heli will stay

    HelicopterLadderController LadderController;

    void Start()
    {
        MainCamera = Camera.main.gameObject;
        LadderController = GetComponent<HelicopterLadderController>();
    }
    
    void Update()
    {
        print("FPS Counted in the HeliExtraction.cs");
        print("FPS: " + Mathf.Ceil(1.0f / Time.deltaTime));//1/(1/60) = 60
        //Hide all the objects if the heli is suppose to leave
        HideAll(!TimeToLeave);
        //If the Heli is suppose to leave and the timer is below zero
        if(TimeToLeave && DelayTime <= 0)
        {
            //Heli flys away
            transform.Translate((-transform.forward - transform.right).normalized * Time.deltaTime * Speed);
            //Camera follows the heli as it leaves
            MainCamera.transform.LookAt(transform.position);
            //Camera is no longer parented to the heli
            MainCamera.transform.parent = transform.parent;
        }
        else if(TimeToLeave)
        {
            //Count down the timer
            DelayTime -= Time.deltaTime;
            //Raise the ladder
            LadderController.CurrentFrame -= Time.deltaTime / 2;
        }
    }

    public void HideAll(bool _ShowHide)
    {
        UIDisplay.SetActive(_ShowHide);
        SecondCamera.gameObject.SetActive(_ShowHide);
    }
}
