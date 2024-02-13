using Amazon.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentimentAnalysis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public string GetSentimentFromString(string review)
    {

        var credentials = new BasicAWSCredentials("AKIA2UH6HQ3CZZYQA6AZ", "5ic1cbU6APbIfjaNM7EQn+Ics6QUOJ1S7+uhNELp");

        using (var comprehendClient = new Amazon.Comprehend.AmazonComprehendClient(credentials, Amazon.RegionEndpoint.EUCentral1))
        {
            var sentimentRequest = new Amazon.Comprehend.Model.DetectSentimentRequest();
            sentimentRequest.Text = review;
            sentimentRequest.LanguageCode = "de";
            var sentimentResult = comprehendClient.DetectSentimentAsync(sentimentRequest).GetAwaiter().GetResult();
            //Debug.Log("Statement: " + review);
            //Debug.Log("Sentiment: " + sentimentResult.Sentiment);
            //Debug.Log("With Positive Score" + sentimentResult.SentimentScore.Positive);
            //Debug.Log("With Negative Score" + sentimentResult.SentimentScore.Negative);
            //Debug.Log("With Neutral Score" + sentimentResult.SentimentScore.Neutral);
            //Debug.Log("With Mixed Score" + sentimentResult.SentimentScore.Mixed);

            return sentimentResult.Sentiment;
        }
    }


}
