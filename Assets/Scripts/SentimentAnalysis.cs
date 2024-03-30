using Amazon.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Does a sentiment analysis with amazon comprehend
//-----------------------------------------

public class SentimentAnalysis : MonoBehaviour
{

    public string GetSentimentFromString(string review)
    {
        var credentials = new BasicAWSCredentials("YourAcceessKey", "YourSecretKey");

        using (var comprehendClient = new Amazon.Comprehend.AmazonComprehendClient(credentials, Amazon.RegionEndpoint.EUCentral1))
        {
            var sentimentRequest = new Amazon.Comprehend.Model.DetectSentimentRequest();
            sentimentRequest.Text = review;
            sentimentRequest.LanguageCode = "de";
            var sentimentResult = comprehendClient.DetectSentimentAsync(sentimentRequest).GetAwaiter().GetResult();

            return sentimentResult.Sentiment;
        }
    }

}
