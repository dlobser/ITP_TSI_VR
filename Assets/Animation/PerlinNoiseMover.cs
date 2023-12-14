using UnityEngine;

[ExecuteAlways]
public class PerlinNoiseMover : MonoBehaviour
{
    public TimelineTimeTracker timeTracker;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public float speed = 1.0f;

    void Update()
    {
        if (timeTracker != null)
        {
            float perlinNoiseTime = timeTracker.timelineTime * speed;

            // Generate Perlin noise-based positions
            float x = Mathf.Lerp(minPosition.x, maxPosition.x, Mathf.PerlinNoise(perlinNoiseTime, 0.0f));
            float y = Mathf.Lerp(minPosition.y, maxPosition.y, Mathf.PerlinNoise(0.0f, perlinNoiseTime));

            // Apply the position
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
