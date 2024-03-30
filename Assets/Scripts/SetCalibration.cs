using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.Hands{

    //-----------------------------------------
    //Reads the ChatGPT feedback in the feedback scene
    //-----------------------------------------

    public class SetCalibration : MonoBehaviour
    {
        [SerializeField]
        private GameObject SceneManager;
        float pinchTimer = 5.0f;

        [SerializeField]
        private Image radialIndicatorLeft;
        [SerializeField]
        private Image radialIndicatorRight;

        // Update is called once per frame
        void Update()
        {
            //if left is pinched, start timer and set distance
            if(MetaAimHand.left.indexPressed.isPressed && MetaAimHand.left.pinchStrengthIndex.ReadValue() >= 0.8)
            {
                if(pinchTimer >= 0)
                {
                    pinchTimer -= Time.deltaTime * 1.0f;
                    radialIndicatorLeft.fillAmount = 1 - (pinchTimer / 5.0f) ;
                }
                else
                {
                    //Set Kalibration Distance
                    GameObject LeftWrist = GameObject.Find("L_Wrist");
                    GameObject Camera = GameObject.Find("Main Camera");

                    float distance = Vector3.Distance(LeftWrist.transform.position, Camera.transform.position);
                    //Set PlayerPrefs
                    PlayerPrefs.SetFloat("CalibrationDistance", distance);

                    SceneManager.GetComponent<SwitchScenes>().ChangeScene();
                }
            }
            //if right is pinched, start timer and set distance
            else if (MetaAimHand.right.indexPressed.isPressed == true && MetaAimHand.right.pinchStrengthIndex.ReadValue() >= 0.8)
            {
                if (pinchTimer >= 0)
                {
                    pinchTimer -= Time.deltaTime * 1.0f;
                    radialIndicatorRight.fillAmount = 1 - (pinchTimer / 5.0f);
                }
                else
                {
                    //Set Kalibration Distance
                    GameObject RightWrist = GameObject.Find("R_Wrist");
                    GameObject Camera = GameObject.Find("Main Camera");

                    float distance = Vector3.Distance(RightWrist.transform.position, Camera.transform.position);
                    //SetPlayerPrefs
                    PlayerPrefs.SetFloat("CalibrationDistance", distance);

                    SceneManager.GetComponent<SwitchScenes>().ChangeScene();
                }
            }
            //reset timer if nothing pressed
            else
            {
                pinchTimer = 5.0f;
                radialIndicatorLeft.fillAmount = 0;
                radialIndicatorRight.fillAmount = 0;
            }


        }
    }
}
