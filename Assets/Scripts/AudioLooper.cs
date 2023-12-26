using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    private AudioSource originalAudioSource; // Attach the original AudioSource here

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Start()
    {
        originalAudioSource = GetComponent<AudioSource>();
        // Create child GameObjects and copy AudioSource settings
        audioSource1 = CreateChildAudioSource("AudioSource1");
        audioSource2 = CreateChildAudioSource("AudioSource2");

        StartCoroutine(HandleAudioFading(audioSource1));
        Invoke("PlayAudio", audioSource2.clip.length / 2f); // Start audioSource1 after half the clip duration")
    }

    void PlayAudio()
    {
        StartCoroutine(HandleAudioFading(audioSource2));
    }

    private AudioSource CreateChildAudioSource(string childName)
    {
        GameObject child = new GameObject(childName);
        child.transform.SetParent(transform);
        child.transform.localPosition = Vector3.zero;
        child.transform.localRotation = Quaternion.identity;
        child.transform.localScale = Vector3.one;

        AudioSource childAudioSource = child.AddComponent<AudioSource>();
        CopyAudioSourceSettings(originalAudioSource, childAudioSource);

        return childAudioSource;
    }

    void Update()
    {
        CopyAudioSourceSettings(originalAudioSource, audioSource1, true);
        CopyAudioSourceSettings(originalAudioSource, audioSource2, true);
    }

    private void CopyAudioSourceSettings(AudioSource source, AudioSource target, bool onUpdate = false)
    {
        if (!onUpdate)
        {
            target.clip = source.clip;
            target.volume = source.volume;
        }

        target.pitch = source.pitch;
        target.mute = source.mute;
        target.bypassEffects = source.bypassEffects;
        target.bypassListenerEffects = source.bypassListenerEffects;
        target.bypassReverbZones = source.bypassReverbZones;
        target.playOnAwake = source.playOnAwake;
        target.loop = source.loop;
        target.priority = source.priority;
        target.spatialBlend = source.spatialBlend;
        target.reverbZoneMix = source.reverbZoneMix;
        target.dopplerLevel = source.dopplerLevel;
        target.spread = source.spread;
        target.rolloffMode = source.rolloffMode;
        target.minDistance = source.minDistance;
        target.maxDistance = source.maxDistance;
        target.panStereo = source.panStereo;
        // Add other properties as needed
    }

    IEnumerator HandleAudioFading(AudioSource a)
    {

        while (true)
        {
            a.Play();
            // Fade audioSource1 in and out
            yield return StartCoroutine(FadeAudio(a, 0f, 1f, (originalAudioSource.clip.length / 2) / originalAudioSource.pitch));
            yield return StartCoroutine(FadeAudio(a, 1f, 0f, (originalAudioSource.clip.length / 2) / originalAudioSource.pitch));
        }
    }

    IEnumerator FadeAudio(AudioSource audioSource, float startVolume, float endVolume, float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume * originalAudioSource.volume, endVolume * originalAudioSource.volume, (Time.time - startTime) / duration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }
}
