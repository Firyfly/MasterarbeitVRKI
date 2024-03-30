using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Teleports the player to the feedback position. Not in use anymore
//-----------------------------------------

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

    [SerializeField]
    private GameObject UserRig;

    int positionNumber = 1;

    //Teleports to the right    
    public void GoRight()
    {
        positionNumber += 1;
        TeleportUser();
    }

    //Teleports to the left  
    public void GoLeft()
    {
        positionNumber -= 1;
        TeleportUser();
    }

    //Activate Teleport
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
