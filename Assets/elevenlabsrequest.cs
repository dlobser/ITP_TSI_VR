using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class elevenlabsrequest : MonoBehaviour
{
    private readonly string url = "https://api.elevenlabs.io/v1/text-to-speech/Paul"; // Replace <voice-id>
    private readonly string apiKey = "665908f9ff3502b13c7340cc935175d8"; // Replace <xi-api-key>

    void Start()
    {
        string jsonData = "{\"text\": \"Born and raised in the charming south, " +
                          "I can add a touch of sweet southern hospitality " +
                          "to your audiobooks and podcasts\", " +
                          "\"model_id\": \"eleven_monolingual_v1\", " +
                          "\"voice_settings\": {\"stability\": 0.5, \"similarity_boost\": 0.5}}";

        StartCoroutine(SendRequestToElevenLabs(jsonData));
    }

    IEnumerator SendRequestToElevenLabs(string jsonData)
    {
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "audio/mpeg");
            webRequest.SetRequestHeader("xi-api-key", apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(webRequest);
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }
}
