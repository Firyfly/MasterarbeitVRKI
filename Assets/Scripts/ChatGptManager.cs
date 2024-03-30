using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

//-----------------------------------------
//Not the used Skript to send the ChatGPT Messages, "ChatGPT" ist the actual script but works in almost the same way
//-----------------------------------------

public class ChatGptManager : MonoBehaviour
{

    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();

    public async void AskChatGPT(string newText)
    {
        //Creates the message Object
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";
        messages.Add(newMessage);

        //Creates the Request
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();

        //Sets the message and awaits response from ChatGPT
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";
        var response = await openAI.CreateChatCompletion(request);

        //Error handling
        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);
            Debug.Log(chatResponse.Content);
        }
    }

}
