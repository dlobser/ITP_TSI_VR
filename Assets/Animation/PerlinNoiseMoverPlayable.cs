using UnityEngine;
using UnityEngine.Playables;

public class PerlinNoiseMoverPlayable : PlayableBehaviour
{
    public Transform targetTransform;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public AnimatedPropertyHolder propertyHolder; // Reference to the property holder

    private float lastTime = 0f; // Track the last frame's time
    private float accumulatedTime = 0f; // Accumulated time

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (targetTransform == null || propertyHolder == null)
            return;

        float currentTime = (float)playable.GetTime();
        float deltaTime = currentTime - lastTime; // Calculate the change in time since last frame

        // Update the accumulated time
        accumulatedTime += deltaTime * propertyHolder.animatedSpeed;

        // Generate Perlin noise-based positions using the accumulated time
        float x = Mathf.Lerp(minPosition.x, maxPosition.x, Mathf.PerlinNoise(accumulatedTime, 0.0f)) * propertyHolder.multiply;
        float y = Mathf.Lerp(minPosition.y, maxPosition.y, Mathf.PerlinNoise(0.0f, accumulatedTime)) * propertyHolder.multiply;

        // Apply the position
        targetTransform.position = new Vector3(x, y, targetTransform.position.z);

        lastTime = currentTime; // Update the lastTime for the next frame
    }
}
