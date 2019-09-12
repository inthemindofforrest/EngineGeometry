using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonOnClick : MonoBehaviour
{
    Button ThisButton;
    

    void Start()
    {
        ThisButton = GetComponent<Button>();
        ThisButton.onClick.AddListener(ButtonClicked);
    }

    
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
