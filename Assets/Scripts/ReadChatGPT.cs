using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//-----------------------------------------
//Reads the ChatGPT feedback in the feedback scene
//-----------------------------------------

public class ReadChatGPT : MonoBehaviour
{
    [SerializeField]
    GameObject mouthMoverObject;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextToSpeech>().text2SpeechMessage = GameObject.Find("FeedbackGenerator").GetComponent<FeedbackCreationAndGeneration>().chatGPTAnswer;
        this.GetComponent<TextToSpeech>().voiceID = Amazon.Polly.VoiceId.Vicki;
        this.GetComponent<TextToSpeech>().T2SResponse();

        mouthMoverObject.GetComponent<MouthMover>().isTalking = true;
        animator.SetBool("isTalking", true);
        while (this.GetComponent<AudioSource>().isPlaying) Task.Yield();
        mouthMoverObject.GetComponent<MouthMover>().isTalking = false;
        animator.SetBool("isTalking", false);
    }

}
