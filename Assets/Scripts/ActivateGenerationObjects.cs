using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-----------------------------------------
//Objects for the Scenegeneration. Only here to be used by other Scripts.
//-----------------------------------------

public class ActivateGenerationObjects : MonoBehaviour
{
    
    public GameObject GoldTrophy;
    public GameObject SilverTrophy;
    public GameObject BronzeTrophy;

    public Transform SpeechPosition;
    public TMP_Text tmpLoudnessText;

    public Transform SentimentPosition;
    public TMP_Text tmpSentimentText;

    public Transform HandMovementPosition;
    public TMP_Text tmpHandMovementText;

    public TMP_Text tmpGPTText;

}
