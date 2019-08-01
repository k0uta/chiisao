using System;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BallReceiver : MonoBehaviour
{
    private GameObject targetBall = null;

    private Animator animator;

    private PlayableDirector playableDirector;

    private void Start()
    {
        animator = transform.root.GetComponent<Animator>();
        playableDirector = GetComponent<PlayableDirector>();
        animator.SetTrigger("GotBaseballBat");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsBallInRange())
            {
                BallHit();
            }
            else
            {
                BallMiss();
            }
        }
    }

    private void BallHit()
    {
        Debug.Log("Hit");
        animator.SetBool("Running", true);

        var timelineAsset = (TimelineAsset)playableDirector.playableAsset;
        var track = timelineAsset.GetOutputTrack(2);

        playableDirector.SetGenericBinding(track, targetBall);

        playableDirector.Play();
    }

    private void BallMiss()
    {
        Debug.Log("Miss");
        animator.SetTrigger("BaseballHitMiss");
    }

    private bool IsBallInRange()
    {
        return targetBall != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball") && targetBall == null)
        {
            targetBall = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Ball") && targetBall == other.gameObject)
        {
            targetBall = null;
        }
    }
}
