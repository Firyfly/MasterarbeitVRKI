using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using UnityEngine.Networking;

//-----------------------------------------
//Text to speech Script with amazon polly
//-----------------------------------------

public class TextToSpeech : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    public string text2SpeechMessage;
    public VoiceId voiceID = VoiceId.Daniel;

    public async Task T2SResponse()
    {
        var credentials = new BasicAWSCredentials("YourAcceessKey", "YourSecretKey");
        var client = new AmazonPollyClient(credentials,Amazon.RegionEndpoint.EUCentral1);

        //Creates the request
        var request = new SynthesizeSpeechRequest()
        {
            Text = text2SpeechMessage,
            LanguageCode = "de-DE",
            Engine = Engine.Neural,  //more expensive but less robotic
            VoiceId = voiceID,       //VoiceId.Daniel, //Vicki for female german voice capable of neural
            OutputFormat = OutputFormat.Mp3
        };

        var response = await client.SynthesizeSpeechAsync(request);
        WriteIntoFile(response.AudioStream);

        //Create audioclip and play it
        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();
            while (!op.isDone) await Task.Yield();

            var clip = DownloadHandlerAudioClip.GetContent(www);

            audioSource.clip = clip;
            audioSource.Play();

        }

    }

    private void WriteIntoFile(Stream stream)
    {
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[8 + 1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) >0 ){
                fileStream.Write(buffer, 0, bytesRead);
            }
        }
    }

}
