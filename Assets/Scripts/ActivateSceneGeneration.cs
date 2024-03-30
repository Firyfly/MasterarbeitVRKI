using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Activates the Scenegeneration in the DontDestroyOnLoad Object
//-----------------------------------------

public class ActivateSceneGeneration : MonoBehaviour
{

    void Start()
    {
        GameObject.Find("FeedbackGenerator").GetComponent<FeedbackCreationAndGeneration>().GenerateFeedback();
    }

}
