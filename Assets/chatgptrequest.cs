using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class chatgptrequest : MonoBehaviour
{
    private readonly string apiURL = "https://api.openai.com/v1/completions";
    private readonly string apiKey = "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"; // Replace with your API Key

    void Start()
    {
        StartCoroutine(SendRequestToChatGPT("Please make up ten jokes about apples and worms"));
    }

    IEnumerator SendRequestToChatGPT(string prompt)
    {
        var jsonPayload = "{\"model\": \"text-davinci-002\", \"prompt\": \"" + prompt + "\", \"max_tokens\": 150}";


        using (UnityWebRequest webRequest = new UnityWebRequest(apiURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonPayload);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
