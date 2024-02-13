using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHandtrackingOrController : MonoBehaviour
{

    [SerializeField]
    private GameObject ToggleGameobject;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("IsHandtracking") == 1)
        {
            ToggleGameobject.GetComponent<Toggle>().isOn = true;
            Debug.Log("is true");
        }
        else
        {
            ToggleGameobject.GetComponent<Toggle>().isOn = false;
            Debug.Log("is false");
        }

        Debug.Log(ToggleGameobject.GetComponent<Toggle>().isOn);
    }

    public void ToggleActivated()
    {
        Debug.Log(ToggleGameobject.GetComponent<Toggle>().isOn);
        if (ToggleGameobject.GetComponent<Toggle>().isOn == false)
        {
            Debug.Log("Set false");
            PlayerPrefs.SetInt("IsHandtracking", 0);
            //ToggleGameobject.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            Debug.Log("Set true");
            PlayerPrefs.SetInt("IsHandtracking", 1);
            
            //ToggleGameobject.GetComponent<Toggle>().isOn = true;
        }
    }




}
