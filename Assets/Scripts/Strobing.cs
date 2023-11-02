using UnityEngine;

[ExecuteInEditMode]
public class Strobing : MonoBehaviour
{
    public float speed = 1.0f; // The speed at which the counter will increase.
    public Color color1 = Color.red; // The first color.
    public Color color2 = Color.blue; // The second color.

    private SpriteRenderer spriteRenderer;
    private float counter = 0.0f;

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject this script is on.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // If there is no SpriteRenderer component, print a warning.
        if (spriteRenderer == null)
        {
            Debug.LogWarning("No SpriteRenderer found on this GameObject. Please attach one.");
        }
    }

    void Update()
    {
        // Increase the counter by speed * deltaTime.
        counter += speed * Time.deltaTime;

        // Use the counter to drive a sin wave. This will return a value between -1 and 1.
        float sinValue = Mathf.Sin(counter);

        // Convert the sinValue range from [-1, 1] to [0, 1] for lerp.
        float lerpValue = (sinValue + 1) / 2.0f;

        // Lerp between the two colors using the value.
        Color currentColor = Color.Lerp(color1, color2, lerpValue);

        // Set the sprite renderer's color to the computed color.
        if (spriteRenderer != null)
        {
            spriteRenderer.color = currentColor;
        }
    }
}
