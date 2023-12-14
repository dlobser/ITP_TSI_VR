using UnityEngine;

[ExecuteAlways]
public class PropertyBlockController : MonoBehaviour
{
    public Renderer renderer;
    public float Multiply = 1.0f;
    public float Gamma = 1.0f;
    public float Add = 1.0f;
    public Color ColorA = Color.white;
    public Color ColorB = Color.white;
    public float StrobeSpeed = 1.0f;
    public float RingMix = 0.5f;
    public float RingThickness = 1.0f;
    public float RingBrightness = 1.0f;
    public float RingSize = 1.0f;
    public float RingEdgeFade = 0.5f;
    public float StrobeAmount = 0.0f;

    private MaterialPropertyBlock propBlock;

    void Start()
    {
        propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        if (renderer == null)
            return;
        if(propBlock == null)
            propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Multiply", Multiply);
        propBlock.SetFloat("_Gamma", Gamma);
        propBlock.SetFloat("_Add", Add);
        propBlock.SetColor("_ColorA", ColorA);
        propBlock.SetColor("_ColorB", ColorB);
        propBlock.SetFloat("_StrobeSpeed", StrobeSpeed);
        propBlock.SetFloat("_RingMix", RingMix);
        propBlock.SetFloat("_RingThickness", RingThickness);
        propBlock.SetFloat("_RingBrightness", RingBrightness);
        propBlock.SetFloat("_RingSize", RingSize);
        propBlock.SetFloat("_RingEdgeFade", RingEdgeFade);
        propBlock.SetFloat("_StrobeAmount", StrobeAmount);
        renderer.SetPropertyBlock(propBlock);
    }
}
