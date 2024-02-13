using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandtrackingOrControllerOnRig : MonoBehaviour
{


    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightController;
    [SerializeField]
    private GameObject leftController;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("IsHandtracking") == 1)
        {
            rightHand.gameObject.SetActive(true);
            leftHand.gameObject.SetActive(true);
            rightController.gameObject.SetActive(false);
            leftController.gameObject.SetActive(false);
        }
        else
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            rightController.gameObject.SetActive(true);
            leftController.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

