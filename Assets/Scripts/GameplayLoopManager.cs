using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Amazon.Runtime;
using Amazon.Comprehend;
using System.IO;

public class GameplayLoopManager : MonoBehaviour
{

    [SerializeField]
    private Material buttonRed;
    [SerializeField]
    private Material buttonGreen;

    [SerializeField]
    private GameObject buttonTop;

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

    // Start is called before the first frame update
    void Start()
    {

        //GetSentiment("Ein wundervolles Produkt!");
        //GetSentiment("Eine sehr schlechte Idee, das mag ich gar nicht.");
        //GetSentiment("Naja, ich habe kaum Erfahrung in dem Feld, eher in anderen Bereichen.");
        //GetSentiment("Ja ich kenne mich in diesem Bereich bereits gut aus und habe Erfahrungen");

        
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

    // Update is called once per frame
    void Update()
    {


    }

    public void GetLoudnessFromMicrophone(AudioClip clip)
    {   
        // < 0.7 viel zu leise
        // < 0.9 Zu leise
        // < 1.3 ziemlich gut
        // < 1.5 deutlich aber nicht lauter
        // > 1.5 zu laut
        loudness = this.GetComponent<MeasureLoudness>().GetLoudnessFromAudioClip(clip);     
    }

    //TODO: Outspource Own script
    public string GetSentiment(string review)
    {
        string result = this.GetComponent<SentimentAnalysis>().GetSentimentFromString(review);
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

    public async Task StartLoop() //ChatGPT Answer and Text2Speech
    {

        buttonTop.GetComponent<Renderer>().material = buttonRed;

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

            case 11:    //Nicht erreichbar, da die

                break;
        }

        if(round <= 11)
        {

            stateManager.currentGamestate = StateManager.Gamestate.ManagerTalking;
            
            //Entweder begrüßungs gap fillers oder das andere
            chatGPT.inputField.text = gptMessage;
            await chatGPT.SendReply();
            textToSpeech.text2SpeechMessage = chatGPT.gptAnswer;
            supervisorAnimations.isTalking = true;
            await textToSpeech.T2SResponse();
            while (T2SGameobject.GetComponent<AudioSource>().isPlaying) await Task.Yield();
            supervisorAnimations.isTalking = false;
            supervisorAnimations.StopTalking();
            

            whisper.StartRecording();

            buttonTop.GetComponent<Renderer>().material = buttonGreen;

            stateManager.currentGamestate = StateManager.Gamestate.ApplicantTalking;

        }
        else
        {

            gptMessage = "Bitte gebe nun eine Bewertung des Interviews ab und beziehe dich vor allem auf den Konversationskontext.";
            chatGPT.inputField.text = gptMessage;
            await chatGPT.SendReply();
            
            feedbackCreationAndGeneration.chatGPTAnswer = chatGPT.gptAnswer;

            this.GetComponent<SwitchScenes>().ChangeScene();
        }



    }

    public void EndLoop()
    {

        //loudness
        //Sentiment
        //message
        //Wie weiter geht

        //TODO:
        //Handbewegung

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
        

        gptMessage = "Der Bewerber hat folgende Antwort gegeben \"" + Speech2TextMessage + "\". Dabei hat er " + loudnessText + " gesporchen. Seine Stimmung war dabei "+ GetSentiment(Speech2TextMessage) + "." + roundText;


        round += 1;
        StartLoop();

        //if(round != 11)
        //{
        //    StartLoop();
        //}
        //else
        //{
        //    //Bitte gib rückmeldung und evaluation-> an chatgpt und das in feedback einbinden
        //}
        
    }

    private void OnDestroy()
    {

        float averageOwnTurn = 0.0f;
        int ownCount = 0;
        float averageSupervisorTurn = 0.0f;
        int supervisorCount = 0;

        for(int i = 0; i < handContrDistanceTracking.RightGeneralAverageCollection.Count; i++)
        {
            Debug.Log(i);
            if (i == 0)
            {
                averageSupervisorTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageSupervisorTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                supervisorCount += 2;
                Debug.Log("Zero");
            }
            else if(i % 2 == 0)
            {
                averageSupervisorTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageSupervisorTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                supervisorCount += 2;
                Debug.Log("right");
            }
            else
            {
                averageOwnTurn += handContrDistanceTracking.RightGeneralAverageCollection[i];
                averageOwnTurn += handContrDistanceTracking.LeftGeneralAverageCollection[i];
                ownCount += 2;
                Debug.Log("left");
            }
        }


        feedbackCreationAndGeneration.averageHandMovementWhenOwnTurn = averageOwnTurn / ownCount;
        feedbackCreationAndGeneration.averageHandMovementWhenSuperviserTurn = averageSupervisorTurn / supervisorCount;
    }

}
