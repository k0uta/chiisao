using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class PositionBehaviour : PlayableBehaviour
{
    public Transform targetLocation;

    private GameObject targetUnit;

    private Vector3 originalPosition;

    private Vector3 targetPosition;

    private bool firstFrame = true;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        targetUnit = playerData as GameObject;

        if (targetUnit == null)
        {
            return;
        }

        if (firstFrame)
        {
            firstFrame = false;
            originalPosition = targetUnit.transform.position;
            targetPosition = targetLocation.position;
        }

        float elapsed = (float) (playable.GetTime() / playable.GetDuration());

        Vector3 position = Vector3.LerpUnclamped(originalPosition, targetPosition, elapsed);

        targetUnit.transform.position = position;

        base.ProcessFrame(playable, info, playerData);
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        firstFrame = true;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        firstFrame = true;
    }
}
