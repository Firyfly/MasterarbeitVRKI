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
                loudnessText = "Die Stimmlautst�rke war verst�ndlich und eine Kommunikation zu f�hren war m�glich. Versuchen sie ein kleines bisschen lauter zu sprechen, dann kommen sie noch selbstbewusster r�ber!";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else if(loudness4 >= loudness3)
            {
                loudnessText = "Die Stimmlautst�rke war sehr deutlich, die Kommunikation war m�glich. Passen sie jedoch auf, nicht lauter zu sprechen, da es sonst zu laut wird. Versuchen sie etwas ruhiger zu Sprechen";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            //Gold
            else
            {
                loudnessText = "Sehr gut! Sie haben deutlich und verst�ndlich gesprochen. Genauso weiter!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
        }
        else
        {
            //Bronze
            if (loudness1 > 2 && loudness5 > 2)
            {
                loudnessText = "Die Sprachlautst�rke war mehrmals zu laut und zu leise, passen sie auf ihre Stimmlautst�rke konsistent zu halten und einen Mittelwert zu finden!";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else if(loudness1 <= loudness5)
            {
                loudnessText = "Die Sprachlautst�rke war deutlich zu leise, versuchen sie lauter zu sprechen. Sie brauchen keine Angst haben damit unh�flich zu wirken.";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
            else
            {
                loudnessText = "Die Sprachlautst�rke war deutlich zu laut, versuchen sie leiser zu sprechen, denn das Verhalten kann sonst als aufdringlich gewertet werden";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
        }
        generationObjects.tmpLoudnessText.text = loudnessText;
        


        //Sentiment
        if(sentimentNegative >= 2)
        {
            //Bronze
            sentimentText = "Sie haben mehrmals mit deutlich erkennbarer negativer Stimmung geantwortet, dies gibt den Bewerbungsleitern ein schlechtes Bild. Versuchen sie deutlich positivere S�tze zu bilden.";
            GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SentimentPosition);
            InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
        }
        else
        {
            if(sentimentPositive >= 5)
            {
                //Gold
                sentimentText = "Sie haben in dem Jobinterview eine gute positive Stimmung r�bergebracht. Behalten sie dies bei!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SentimentPosition);
                InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
            }
            else
            {
                //Silver
                sentimentText = "Sie haben mehrfach mit sehr neutraler Stimmung geantwortet. W�hrend dies nicht direkt negativ ist, �berbringt dies das Gef�hl von Desinteresse. Versuchen sie ihre positivit�t etwas mehr zu zeigen.";
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
                handMovementText = "Super. Die eigenen Handbewegungen waren passend und wenn die andere Person gesprochen hat wurde zugeh�rt. Weiter so!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
        }

        generationObjects.tmpHandMovementText.text = handMovementText;

        //ChatGPT Feedback
        generationObjects.tmpGPTText.text = chatGPTAnswer;
    }

}
