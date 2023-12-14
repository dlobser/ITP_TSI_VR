using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "New Perlin Noise Mover", menuName = "Playables/Perlin Noise Mover", order = 1)]
public class PerlinNoiseMoverPlayableAsset : PlayableAsset
{
    public ExposedReference<Transform> targetTransform;
    public Vector2 minPosition = new Vector2(-5f, -5f);
    public Vector2 maxPosition = new Vector2(5f, 5f);
    public ExposedReference<AnimatedPropertyHolder> propertyHolder; // Add this line

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PerlinNoiseMoverPlayable>.Create(graph);
        PerlinNoiseMoverPlayable perlinMover = playable.GetBehaviour();

        perlinMover.targetTransform = targetTransform.Resolve(graph.GetResolver());
        perlinMover.minPosition = minPosition;
        perlinMover.maxPosition = maxPosition;
        perlinMover.propertyHolder = propertyHolder.Resolve(graph.GetResolver()); // Assign the property holder

        return playable;
    }
}
