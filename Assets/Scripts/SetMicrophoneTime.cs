using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//-----------------------------------------
//Sets the Microphone time in the main menu
//-----------------------------------------

public class SetMicrophoneTime : MonoBehaviour
{

    [SerializeField]
    private GameObject ToggleGameobject;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the active choice in the dropdown
        if(this.name == "Dropdown")
        {
            if (PlayerPrefs.GetInt("MicRecLength", 0) == 0 || PlayerPrefs.GetInt("MicRecLength") == 30)
            {
                PlayerPrefs.SetInt("MicRecLength", 30);
                this.gameObject.GetComponent<TMP_Dropdown>().value = 0;
            }
            else if (PlayerPrefs.GetInt("MicRecLength") == 60)
            {
                this.gameObject.GetComponent<TMP_Dropdown>().value = 1;
            }
            else
            {
                this.gameObject.GetComponent<TMP_Dropdown>().value = 2;
            }
        }


        if(this.name != "Dropdown")
        {
            if (PlayerPrefs.GetInt("ShowTimer", 0) == 0)
            {
                PlayerPrefs.SetInt("ShowTimer", 0);
                ToggleGameobject.GetComponent<Toggle>().isOn = false;
            }
            else
            {
                PlayerPrefs.SetInt("ShowTimer", 1);
                ToggleGameobject.GetComponent<Toggle>().isOn = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMicrophoneLength()
    {
        switch (this.GetComponent<TMP_Dropdown>().value)
        {
            case 0:
                PlayerPrefs.SetInt("MicRecLength", 30);
                break;

            case 1:
                PlayerPrefs.SetInt("MicRecLength", 60);
                break;
            case 2:
                PlayerPrefs.SetInt("MicRecLength", 120);
                break;

        }
    }

    public void ShowTimer()
    {
        if(PlayerPrefs.GetInt("ShowTimer") == 0)
        {
            PlayerPrefs.SetInt("ShowTimer", 1);
            ToggleGameobject.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("ShowTimer", 0);
            ToggleGameobject.GetComponent<Toggle>().isOn = false;
        }
    }

}
