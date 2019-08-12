using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PositionClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private ExposedReference<Transform> targetLocation;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.All; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PositionBehaviour>.Create(graph);

        PositionBehaviour positionBehaviour = playable.GetBehaviour();
        
        var timelineAsset = (TimelineAsset)owner.GetComponent<PlayableDirector>().playableAsset;
        var track = timelineAsset.GetOutputTrack(3);

        positionBehaviour.targetLocation = targetLocation.Resolve(graph.GetResolver());

        return playable;
    }
}
