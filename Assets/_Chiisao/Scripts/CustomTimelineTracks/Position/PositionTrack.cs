using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(1f, 0f, 0f)]
[TrackBindingType(typeof(GameObject))]
[TrackClipType(typeof(PositionClip))]
public class PositionTrack : TrackAsset
{
}
