using UnityEngine;
using UnityEngine.Playables;

[ExecuteAlways]
public class TimelineTimeTracker : MonoBehaviour
{
    public PlayableDirector director;
    public float timelineTime;

    void Update()
    {
        if (director != null)
        {
            timelineTime = (float)director.time;
        }
    }
}
