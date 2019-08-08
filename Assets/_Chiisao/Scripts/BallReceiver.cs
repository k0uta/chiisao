using System;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Collections;

public class BallReceiver : MonoBehaviour
{
    private GameObject targetBall = null;

    [SerializeField] private GameObject explosionParticle;

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

        targetBall.GetComponent<Rigidbody>().velocity = Vector3.zero;

        playableDirector.Play();
    }

    public void ReleaseBall()
    {
        StartCoroutine(TimeScaleCoroutine());
    }

    IEnumerator TimeScaleCoroutine()
    {
        var timelineAsset = (TimelineAsset)playableDirector.playableAsset;
        var track = timelineAsset.GetOutputTrack(2);

        var ball = playableDirector.GetGenericBinding(track) as GameObject;
        playableDirector.SetGenericBinding(track, null);
        Debug.Log("Relase Ball");

        var explosion = Instantiate(explosionParticle) as GameObject;
        explosion.transform.position = ball.transform.position;

        Time.timeScale = 0.01f;

        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1f;

        ball.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 25f, 50f), ForceMode.Impulse);
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
