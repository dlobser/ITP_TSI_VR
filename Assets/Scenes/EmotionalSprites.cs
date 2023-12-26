using UnityEngine;

public class EmotionalSprites : MonoBehaviour
{
    public GameObject spritePrefab; // Assign a sprite prefab with a default sprite shader

    void Start()
    {
        CreateSprites();
    }

    void CreateSprites()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject sprite = Instantiate(spritePrefab, RandomPosition(), Quaternion.identity, transform);
            SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();
            spriteRenderer.color = GetColor(i);
            sprite.name = "Sprite" + i; // Naming for identification
        }
    }

    Vector3 RandomPosition()
    {
        // Ensure sprites are between 1 and 20 units away from the camera
        Vector3 direction = Random.onUnitSphere;
        float distance = Random.Range(1f, 20f);
        return direction * distance;
    }

    Color GetColor(int index)
    {
        // Define colors
        Color[] colors = new Color[]
        {
            new Color(0.5f, 0.5f, 0.5f), // Gray
            new Color(0.3f, 0.3f, 0.6f), // Muted Blue
            new Color(0.7f, 0.2f, 0.2f), // Faded Red
            new Color(0.6f, 0.5f, 0.3f), // Muted Orange
            new Color(0.4f, 0.4f, 0.4f)  // Dark Gray
        };

        return colors[index % colors.Length];
    }

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject sprite = transform.GetChild(i).gameObject;
            Color color = sprite.GetComponent<SpriteRenderer>().color;

            if (color == new Color(0.5f, 0.5f, 0.5f)) // Gray
            {
                GrayAnimation(sprite);
            }
            else if (color == new Color(0.3f, 0.3f, 0.6f)) // Muted Blue
            {
                BlueAnimation(sprite);
            }
            else if (color == new Color(0.7f, 0.2f, 0.2f)) // Faded Red
            {
                RedAnimation(sprite);
            }
            else if (color == new Color(0.6f, 0.5f, 0.3f)) // Muted Orange
            {
                OrangeAnimation(sprite);
            }
            else if (color == new Color(0.4f, 0.4f, 0.4f)) // Dark Gray
            {
                DarkGrayAnimation(sprite);
            }
        }
    }

    void GrayAnimation(GameObject sprite)
    {
        float scale = Mathf.PingPong(Time.time, 0.2f) + 0.9f;
        sprite.transform.localScale = new Vector3(scale, scale, scale);
    }

    void BlueAnimation(GameObject sprite)
    {
        float yPos = Mathf.Sin(Time.time * 0.5f) * 0.5f;
        sprite.transform.position = new Vector3(sprite.transform.position.x, yPos, sprite.transform.position.z);
    }

    void RedAnimation(GameObject sprite)
    {
        float scale = Mathf.PingPong(Time.time, 0.5f) + 0.5f;
        sprite.transform.localScale = new Vector3(scale, scale, scale);
    }

    void OrangeAnimation(GameObject sprite)
    {
        sprite.transform.Rotate(new Vector3(0f, 0f, 20f) * Time.deltaTime);
    }

    void DarkGrayAnimation(GameObject sprite)
    {
        sprite.transform.position += new Vector3(0f, -0.1f * Time.deltaTime, 0f);
    }
}
