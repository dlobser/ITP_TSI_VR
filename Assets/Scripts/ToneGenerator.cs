using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToneGenerator : MonoBehaviour
{
    public float frequency = 440.0f; // Frequency of the sine wave (e.g., 440Hz is the musical note A4).
    private float phase = 0.0f;      // Keeps track of the phase of the sine wave.
    private float sampleRate;        // Sample rate of the audio system.

    private void Start()
    {
        // Assuming the audio system's sample rate is consistent across calls to OnAudioFilterRead.
        sampleRate = AudioSettings.outputSampleRate;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        // Calculate the increment in phase for each sample.
        float increment = frequency * 2.0f * Mathf.PI / sampleRate;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            // Ensure phase is always between 0 and 2*PI.
            if (phase > 2.0f * Mathf.PI)
            {
                phase -= 2.0f * Mathf.PI;
            }

            float sample = Mathf.Sin(phase);

            // Apply the sample to all channels (e.g., stereo has 2 channels).
            for (int c = 0; c < channels; c++)
            {
                data[i + c] = sample;
            }
        }
    }
}
