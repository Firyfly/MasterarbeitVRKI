using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {

        

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateFeedback()
    {

        generationObjects = GameObject.Find("GenerationObjects").GetComponent<ActivateGenerationObjects>();

        Debug.Log(generationObjects.SpeechPosition.position);
        Debug.Log(generationObjects.SentimentPosition.position);
        Debug.Log(generationObjects.HandMovementPosition.position);


        //Sprache
        if (loudness1 + loudness5 <= 2)
        {
            //im normalen und guten bereich, silber
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
            else
            {
                //Perfekt, gold
                loudnessText = "Sehr gut! Sie haben deutlich und verst�ndlich gesprochen. Genauso weiter!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SpeechPosition);
                InstantiatedObject.transform.position = generationObjects.SpeechPosition.position;
            }
        }
        else
        {
            //Schlecht, bronze
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
            //schlecht bronze
            sentimentText = "Sie haben mehrmals mit deutlich erkennbarer negativer Stimmung geantwortet, dies gibt den Bewerbungsleitern ein schlechtes Bild. Versuchen sie deutlich positivere S�tze zu bilden.";
            GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.SentimentPosition);
            InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
        }
        else
        {
            if(sentimentPositive >= 5)
            {
                //Gut gold
                sentimentText = "Sie haben in dem Jobinterview eine gute positive Stimmung r�bergebracht. Behalten sie dies bei!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.SentimentPosition);
                InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
            }
            else
            {
                //Mittel Silber
                sentimentText = "Sie haben mehrfach mit sehr neutraler Stimmung geantwortet. W�hrend dies nicht direkt negativ ist, �berbringt dies das Gef�hl von Desinteresse. Versuchen sie ihre positivit�t etwas mehr zu zeigen.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.SentimentPosition);
                InstantiatedObject.transform.position = generationObjects.SentimentPosition.position;
            }
        }
        generationObjects.tmpSentimentText.text = sentimentText;


        //Handbewegung und K�rperhaltung etc

        if(averageHandMovementWhenOwnTurn <= 0.2f)//Schlecht
        {

            if(averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen sollten etwas mehr zum eigenen Vorteil genutzt werden beim Sprechen, und weniger wenn die andere Person spricht";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.HandMovementPosition);//Ganz schlecht
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            else
            {
                handMovementText = "Die eigenen Handbewegungen sollten etwas mehr zum eigenen Vorteil genutzt werden beim Sprechen.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }

        }
        else if(averageHandMovementWhenOwnTurn >= 0.7f)//Schlecht
        {
            if (averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen sollten ruhiger und langsamer werden, vor allem wenn andere Personen sprechen. ";
                GameObject InstantiatedObject = Instantiate(generationObjects.BronzeTrophy, generationObjects.HandMovementPosition);//Ganz schlecht
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            else
            {
                handMovementText = "Die eigenen Handbewegungen sollten ruhiger und langsamer werden.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
        }
        else//Gut
        {
            if (averageHandMovementWhenSuperviserTurn >= 0.2f)
            {
                handMovementText = "Die eigenen Handbewegungen haben das gesagte unterstrichen, jedoch sollten diese unterbunden werden, wenn die andere Person spricht.";
                GameObject InstantiatedObject = Instantiate(generationObjects.SilverTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
            else
            {
                handMovementText = "Super. Die eigenen Handbewegungen waren passend und wenn die andere Person gesprochen hat wurde zugeh�rt. Weiter so!";
                GameObject InstantiatedObject = Instantiate(generationObjects.GoldTrophy, generationObjects.HandMovementPosition);
                InstantiatedObject.transform.position = generationObjects.HandMovementPosition.position;
            }
        }

        generationObjects.tmpHandMovementText.text = handMovementText;


        //"eyetracking"





        //GPT R�ckmeldung

        generationObjects.tmpGPTText.text = chatGPTAnswer;














    }

}
