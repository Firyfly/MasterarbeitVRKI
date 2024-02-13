using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.XR.Hands{

    public class SetCalibration : MonoBehaviour
    {
        [SerializeField]
        private GameObject SceneManager;
        float pinchTimer = 5.0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //bool test = Hands.MetaAimHand.right.indexPressed.isPressed;
            //Debug.Log(test);
            //float test2 = Hands.MetaAimHand.right.pinchStrengthIndex.ReadValue();
            //Debug.Log(test2);

            if(MetaAimHand.left.indexPressed.isPressed && MetaAimHand.left.pinchStrengthIndex.ReadValue() >= 0.8)
            {
                
                if(pinchTimer >= 0)
                {
                    pinchTimer -= Time.deltaTime * 1.0f;
                    Debug.Log("pinchTimer: " + pinchTimer);
                }
                else
                {
                    //Set Kalibration Distance
                    GameObject LeftWrist = GameObject.Find("L_Wrist");
                    GameObject Camera = GameObject.Find("Main Camera");

                    float distance = Vector3.Distance(LeftWrist.transform.position, Camera.transform.position);
                    //TODO: SetPlayerPrefs
                    PlayerPrefs.SetFloat("CalibrationDistance", distance);
                    Debug.Log(distance);

                    SceneManager.GetComponent<SwitchScenes>().ChangeScene();
                }
            }
            else if(MetaAimHand.right.indexPressed.isPressed == true && MetaAimHand.right.pinchStrengthIndex.ReadValue() >= 0.8)
            {
                if (pinchTimer >= 0)
                {
                    pinchTimer -= Time.deltaTime * 1.0f;
                    Debug.Log("pinchTimer: " + pinchTimer);
                }
                else
                {
                    //Set Kalibration Distance
                    GameObject RightWrist = GameObject.Find("R_Wrist");
                    GameObject Camera = GameObject.Find("Main Camera");

                    float distance = Vector3.Distance(RightWrist.transform.position, Camera.transform.position);
                    //TODO: SetPlayerPrefs
                    PlayerPrefs.SetFloat("CalibrationDistance", distance);
                    Debug.Log(distance);

                    SceneManager.GetComponent<SwitchScenes>().ChangeScene();
                }
            }
            else
            {
                pinchTimer = 5.0f;
            }


        }
    }
}
