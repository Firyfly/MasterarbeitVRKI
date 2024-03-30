using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-----------------------------------------
//Generates the Feedback from the collected Data
//-----------------------------------------

public class FeedbackCreationAndGeneration : MonoBehaviour
{

    private ActivateGenerationObjects generationObjects;

    public int loudness1 = 0;
    public int loudness2 = 0;
    public int loudness3 = 0;
    public int loudness4 = 0;
    public int loudness5 = 0;
    private string loudnessText;

    public int sentimentPositive;
    public int sentimentNegative;
    public int sentimentNeutral;
    public string sentimentText;

    public float averageHandMovementWhenOwnTurn;
    public float averageHandMovementWhenSuperviserTurn;
    public string handMovementText;

    public string chatGPTAnswer;

    //Makes the Object not destroy during scene change
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Generates the Feedback
    public void GenerateFeedback()
    {
        //Get the Objects from changed Scene
        generationObjects = GameObject.Find("GenerationObjects").GetComponent<ActivateGenerationObjects>();

        //Speech Generation
        if (loudness1 + loudness5 <= 2)
        {
            //Silver
            if(loudness2 >= loudness3)
            {
                loudnessText = "Die Stimmlautstärke war verständlich und eine Kommunikation zu führen war möglich. Versuchen sie ein kleines bisschen lauter zu sprechen, dann kommen sie noch selbstbewusster rüber!";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else if(loudness4 >= loudness3)
            {
                loudnessText = "Die Stimmlautstärke war sehr deutlich, die Kommunikation war möglich. Passen sie jedoch auf, nicht lauter zu sprechen, da es sonst zu laut wird. Versuchen sie etwas ruhiger zu Sprechen";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            //Gold
            else
            {
                loudnessText = "Sehr gut! Sie haben deutlich und verständlich gesprochen. Genauso weiter!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
        }
        else
        {
            //Bronze
            if (loudness1 > 2 && loudness5 > 2)
            {
                loudnessText = "Die Sprachlautstärke war mehrmals zu laut und zu leise, passen sie auf ihre Stimmlautstärke konsistent zu halten und einen Mittelwert zu finden!";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else if(loudness1 <= loudness5)
            {
                loudnessText = "Die Sprachlautstärke war deutlich zu leise, versuchen sie lauter zu sprechen. Sie brauchen keine Angst haben damit unhöflich zu wirken.";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else
            {
                loudnessText = "Die Sprachlautstärke war deutlich zu laut, versuchen sie leiser zu sprechen, denn das Verhalten kann sonst als aufdringlich gewertet werden";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
        }
        generationObjects.tmpLoudnessText.text = loudnessText;
        


        //Sentiment
        if(sentimentNegative >= 2)
        {
            //Bronze
            sentimentText = "Sie haben mehrmals mit deutlich erkennbarer negativer Stimmung geantwortet, dies gibt den Bewerbungsleitern ein schlechtes Bild. Versuchen sie deutlich positivere Sätze zu bilden.";
            GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SentimentPosition);
            InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
        }
        else
        {
            if(sentimentPositive >= 5)
            {
                //Gold
                sentimentText = "Sie haben in dem Jobinterview eine gute positive Stimmung rübergebracht. Behalten sie dies bei!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SentimentPosition);
                InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
            }
            else
            {
                //Silver
                sentimentText = "Sie haben mehrfach mit sehr neutraler Stimmung geantwortet. Während dies nicht direkt negativ ist, überbringt dies das Gefühl von Desinteresse. Versuchen sie ihre positivität etwas mehr zu zeigen.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SentimentPosition);
                InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
            }
        }
        generationObjects.tmpSentimentText.text = sentimentText;

        //Hand Movement
        if(averageHandMovementWhenOwnTurn <= 0.2f)
        {
            //Bronze
            if(averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen sollten etwas mehr zum eigenen Vorteil genutzt werden beim Sprechen, und weniger wenn die andere Person spricht";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.HandMovementPosition);//Ganz schlecht
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            //Silver
            else
            {
                handMovementText = "Die eigenen Handbewegungen sollten etwas mehr zum eigenen Vorteil genutzt werden beim Sprechen.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }

        }
        else if(averageHandMovementWhenOwnTurn >= 0.7f)
        {
            //Bronze
            if (averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen sollten ruhiger und langsamer werden, vor allem wenn andere Personen sprechen. ";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.HandMovementPosition);//Ganz schlecht
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            //Silver
            else
            {
                handMovementText = "Die eigenen Handbewegungen sollten ruhiger und langsamer werden.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
        }
        else
        {
            //Silver
            if (averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen haben das gesagte unterstrichen, jedoch sollten diese unterbunden werden, wenn die andere Person spricht.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            //Gold
            else
            {
                handMovementText = "Super. Die eigenen Handbewegungen waren passend und wenn die andere Person gesprochen hat wurde zugehört. Weiter so!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
        }

        generationObjects.tmpHandMovementText.text = handMovementText;

        //ChatGPT Feedback
        generationObjects.tmpGPTText.text = chatGPTAnswer;
    }

}
