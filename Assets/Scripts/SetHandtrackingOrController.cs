using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//-----------------------------------------
//Sets the Hand Tracking or Controller in the main menu
//-----------------------------------------

public class SetHandtrackingOrController : MonoBehaviour
{

    [SerializeField]
    private GameObject ToggleGameobject;

    // Start is called before the first frame update
    void Start()
    {
        //Set the toggle button on the start of the scene
        if (PlayerPrefs.GetInt("IsHandtracking") == 1)
        {
            ToggleGameobject.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            ToggleGameobject.GetComponent<Toggle>().isOn = false; 
        }
    }

    public void ToggleActivated()
    {
        if (ToggleGameobject.GetComponent<Toggle>().isOn == false)
        {   
            PlayerPrefs.SetInt("IsHandtracking", 0);
        }
        else
        {  
            PlayerPrefs.SetInt("IsHandtracking", 1);
        }
    }




}
