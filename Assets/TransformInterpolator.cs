using UnityEngine;
using DG.Tweening;

public class TransformInterpolator : MonoBehaviour
{
    [Header("Transform Targets")]
    public Transform startTransform;
    public Transform endTransform;

    [Header("Interpolation Settings")]
    [Range(0, 1)] public float interpolationFactor = 0;
    public Ease interpolationMethod = Ease.Linear;

    private void Update()
    {
        if (startTransform == null || endTransform == null) return;

        InterpolateTransforms();
    }

    private void InterpolateTransforms()
    {
        // Interpolating position, rotation, and scale
        transform.position = Vector3.Lerp(startTransform.position, endTransform.position, interpolationFactor);
        transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, interpolationFactor);
        transform.localScale = Vector3.Lerp(startTransform.localScale, endTransform.localScale, interpolationFactor);

        // Applying the selected easing function
        float easeValue = DOVirtual.EasedValue(0, 1, interpolationFactor, interpolationMethod);
        transform.position = Vector3.Lerp(startTransform.position, endTransform.position, easeValue);
        transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, easeValue);
        transform.localScale = Vector3.Lerp(startTransform.localScale, endTransform.localScale, easeValue);
    }
}
