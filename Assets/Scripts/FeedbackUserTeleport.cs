using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackUserTeleport : MonoBehaviour
{

    [SerializeField]
    private Transform SpeechPos;
    [SerializeField]
    private Transform SentimentPos;
    [SerializeField]
    private Transform HandsPos;
    [SerializeField]
    private Transform EyetrackingPos;

    [SerializeField]
    private Transform ChatGPTPos;


    //Vector3 pos1Speech = new Vector3(-1,0,-7);
    //Vector3 pos2Sentiment = new Vector3(-1,0,-5);
    //Vector3 pos3Hands = new Vector3(-1,0,-3);
    //Vector3 pos4Eyetracking = new Vector3(-1,0,-1);

    [SerializeField]
    private GameObject UserRig;

    int positionNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoRight()
    {
        positionNumber += 1;
        TeleportUser();
    }

    public void GoLeft()
    {
        positionNumber -= 1;
        TeleportUser();
    }


    private void TeleportUser()
    {

        switch (positionNumber)
        {
            case 1:
                UserRig.transform.position = SpeechPos.position;
                break;
            case 2:
                UserRig.transform.position = SentimentPos.position;
                break;
            case 3:
                UserRig.transform.position = HandsPos.position;
                break;
            case 4:
                UserRig.transform.position = EyetrackingPos.position;
                UserRig.transform.rotation = EyetrackingPos.rotation;
                break;
            case 5:
                UserRig.transform.position = ChatGPTPos.position;
                UserRig.transform.rotation = ChatGPTPos.rotation;
                break;
            case 6:
                this.GetComponent<SwitchScenes>().ChangeScene();
                break;
            default:

                break;
        }


    }

}
