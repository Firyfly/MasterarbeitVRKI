using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Amazon.Runtime;
using Amazon.Comprehend;
using System.IO;

//-----------------------------------------
//The Manager which guides the whole simulation Loop
//-----------------------------------------

public class GameplayLoopManager : MonoBehaviour
{

    [SerializeField]
    private Material buttonRed;
    [SerializeField]
    private Material buttonGreen;

    public Material blueButton;


    [SerializeField]
    public GameObject buttonTop;
    [SerializeField]
    private GameObject buttonTutorial;

    [SerializeField]
    GameObject whisperGameobject;
    OpenAI.Whisper whisper;
    [SerializeField]
    GameObject T2SGameobject;
    TextToSpeech textToSpeech;
    [SerializeField]
    GameObject ChatGPTGameobject;
    OpenAI.ChatGPT chatGPT;

    [SerializeField]
    GameObject SupervisorAnimatiosnGameobject;
    SupervisorAnimations supervisorAnimations;

    private AIGapFillers aiGapFillers;

    [SerializeField]
    private GameObject StateManagerGameobject;
    private StateManager stateManager;

    public string text2SpeechMessage;
    public string gptMessage = "Beginne den Anfang des Jobinterviews mit einer Begrüßung zum Jobinterview und Frage nach dem Namen.";
    public string Speech2TextMessage;

    [SerializeField]
    private GameObject FeedbackCreationGameobject;
    private FeedbackCreationAndGeneration feedbackCreationAndGeneration;

    [SerializeField]
    private GameObject DistanceTrackerGameobject;
    private HandContrDistanceTracking handContrDistanceTracking;

    private float loudness;
    private int round = 1;

    //Gets and instantiates all the needed Components and Gameobjects / Scripts in the variables
    void Start()
    {
        whisper = whisperGameobject.GetComponent<OpenAI.Whisper>();
        textToSpeech = T2SGameobject.GetComponent<TextToSpeech>();
        chatGPT = ChatGPTGameobject.GetComponent<OpenAI.ChatGPT>();

        supervisorAnimations = SupervisorAnimatiosnGameobject.GetComponent<SupervisorAnimations>();

        aiGapFillers = this.GetComponent<AIGapFillers>();

        feedbackCreationAndGeneration = FeedbackCreationGameobject.GetComponent<FeedbackCreationAndGeneration>();
        handContrDistanceTracking = DistanceTrackerGameobject.GetComponent<HandContrDistanceTracking>();

        stateManager = StateManagerGameobject.GetComponent<StateManager>();

        chatGPT.messages.Clear();
        
        StartLoop();
    }

    //Decentralized Function to Get the Loudness
    public void GetLoudnessFromMicrophone(AudioClip clip)
    {   
        // < 0.7 Way too quiet
        // < 0.9 quiet
        // < 1.3 good
        // < 1.5 good, just a bit loud
        // > 1.5 too loud
        loudness = this.GetComponent<MeasureLoudness>().GetLoudnessFromAudioClip(clip);     
    }

    //Decentralized Function to Get the Sentiment
    public string GetSentiment(string review)
    {
        string result = this.GetComponent<SentimentAnalysis>().GetSentimentFromString(review);
        //Set results for feedback
        if(result == "POSITIVE")
        {
            feedbackCreationAndGeneration.sentimentPositive += 1;
        }
        else if(result == "NEGATIVE")
        {
            feedbackCreationAndGeneration.sentimentNegative += 1;
        }
        else if(result == "NEUTRAL")
        {
            feedbackCreationAndGeneration.sentimentNeutral += 1;
        }
        else
        {

        }

        return result;
    }

    //Starts the loop, which includes ChatGPT and TextToSpeech
    public async Task StartLoop()
    {
        buttonTop.GetComponent<Renderer>().material = buttonRed;
        
        //Depending on the Round set ai Gapfillers
        switch (round)
        {
            case 2:
                int rand = Random.Range(1, aiGapFillers.introductionGapFillers.Count);
                textToSpeech.text2SpeechMessage = aiGapFillers.introductionGapFillers[rand];
                await textToSpeech.T2SResponse();
                break;

            case >2 and <11:
                int rand2 = Random.Range(1, aiGapFillers.talkingGapFillers.Count);
                textToSpeech.text2SpeechMessage = aiGapFillers.talkingGapFillers[rand2];
                await textToSpeech.T2SResponse();
                break;
        }

        //If its not the End, go into the next loop
        if(round <= 11)
        {
            
            stateManager.currentGamestate = StateManager.Gamestate.ManagerTalking;

            //ChatGPT and T2S with Animations
            chatGPT.inputField.text = gptMessage;
            await chatGPT.SendReply();
            textToSpeech.text2SpeechMessage = chatGPT.gptAnswer;
            supervisorAnimations.isTalking = true;
            await textToSpeech.T2SResponse();
            while (T2SGameobject.GetComponent<AudioSource>().isPlaying) await Task.Yield();
            supervisorAnimations.isTalking = false;
            supervisorAnimations.StopTalking();

            //Starts the S2T Recording, which in its function starts the EndLoop Function
            whisper.StartRecording();
            buttonTop.GetComponent<Renderer>().material = buttonGreen;
            buttonTutorial.SetActive(true);

            stateManager.currentGamestate = StateManager.Gamestate.ApplicantTalking;

        }
        else
        {
            //If the simulation ended, get the Feedback from ChatGPT
            gptMessage = "Bitte gebe nun eine Bewertung des Interviews ab und beziehe dich vor allem auf den Konversationskontext.";
            chatGPT.inputField.text = gptMessage;
            await chatGPT.SendReply();
            
            feedbackCreationAndGeneration.chatGPTAnswer = chatGPT.gptAnswer;

            this.GetComponent<SwitchScenes>().ChangeScene();
        }
    }

    //The End of the simulation Loop, gathering all the information and preparing the Data / next message
    public void EndLoop()
    {
        //Sets the loudness for chatGPT text
        string loudnessText;
        switch (loudness)
        {
            case < 0.0007f:
                loudnessText = "sehr leise ";
                feedbackCreationAndGeneration.loudness1 += 1;
                break;

            case > 0.0007f and < 0.0013f:
                loudnessText = "leise ";
                feedbackCreationAndGeneration.loudness2 += 1;
                break;

            case > 0.0013f and < 0.0018f:
                loudnessText = "mit guter Lautstärke ";
                feedbackCreationAndGeneration.loudness3 += 1;
                break;

            case > 0.0018f and < 0.0023f:
                loudnessText = "gut und deutlich ";
                feedbackCreationAndGeneration.loudness4 += 1;
                break;

            case > 0.0023f:
                loudnessText = "zu laut ";
                feedbackCreationAndGeneration.loudness5 += 1;
                break;

            default:
                loudnessText = "gut";
                feedbackCreationAndGeneration.loudness3 += 1;
                break;
        }

        //Sets the prompt topic for the next message
        string roundText;
        switch (round)
        {
            case 1:
                roundText = "Führe das Interview nun mit dem Themengebiet Erfahrungen im IT Bereich weiter durch.";
                break;

            case 2:
                roundText = "Führe das Interview nun mit dem Themengebiet Erfahrungen mit persönlichen Projekten im IT Bereich weiter durch.";
                break;

            case 3:
                roundText = "Führe das Interview nun mit dem Themengebiet positive und negative Eigenschaften des Bewerbers weiter durch.";
                break;

            case 4:
                roundText = "Führe das Interview nun mit dem Themengebiet besondere Fähigkeiten des Bewerbers weiter durch.";
                break;

            case 5:
                roundText = "Führe das Interview nun mit dem Themengebiet Soziale Eigenschaften des Bewerbers weiter durch.";
                break;

            case 6:
                roundText = "Führe das Interview nun mit dem Themengebiet Warum diese Firma weiter durch.";
                break;

            case 7:
                roundText = "Führe das Interview nun mit dem Themengebiet Ängste des Benutzers weiter durch.";
                break;

            case 8:
                roundText = "Führe das Interview nun mit dem Themengebiet was am wichtigsten in der neuen Arbeitsstelle ist für den Benutzer weiter durch.";
                break;

            case 9:
                roundText = "Führe das Interview nun mit dem Themengebiet Fragen seitens des Benutzers weiter durch.";
                break;

            case 10:
                roundText = "Beende das Interview freundlich";
                break;

            default:
                roundText = "Führe das Interview nun mit dem Themengebiet Erfahrungen im IT Bereich weiter durch.";
                break;
        }

        //Combine the Data in a message and start the next loop
        gptMessage = "Der Bewerber hat folgende Antwort gegeben \"" + Speech2TextMessage +
        "\". Dabei hat er " + loudnessText + " gesporchen. Seine Stimmung war dabei "+ GetSentiment(Speech2TextMessage) + "." + roundText;

        round += 1;
        StartLoop();
        
    }

    //When scene is changing and the object gets destroyed, transfer data to FeedbackCreationAndGeneration which hasnt already transfered
    private void OnDestroy()
    {
        float averageOwnTurn = 0.0f;
        int ownCount = 0;
        float averageSupervisorTurn = 0.0f;
        int supervisorCount = 0;

        for(int i = 0; i < handContrDistanceTracking.RightGeneralAverageCollection.Count; i++)
        {
            if (i == 0)
            {
                averageSupervisorTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageSupervisorTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                supervisorCount += 2;
            }
            else if(i % 2 == 0)
            {
                averageSupervisorTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageSupervisorTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                supervisorCount += 2;
            }
            else
            {
                averageOwnTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageOwnTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                ownCount += 2;
            }
        }

        feedbackCreationAndGeneration.averageHandMovementWhenOwnTurn = averageOwnTurn / ownCount;
        feedbackCreationAndGeneration.averageHandMovementWhenSuperviserTurn = averageSupervisorTurn / supervisorCount;
    }

}
