using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public Vector3 minRotation;
    public Vector3 maxRotation;

    public GameObject target;

    // This is the public function you can call via a signal or other methods
    public void RandomizeRotation()
    {
        Vector3 randomRotation = new Vector3(
            Random.Range(minRotation.x, maxRotation.x),
            Random.Range(minRotation.y, maxRotation.y),
            Random.Range(minRotation.z, maxRotation.z)
        );

        target.transform.eulerAngles = randomRotation;
    }
}
