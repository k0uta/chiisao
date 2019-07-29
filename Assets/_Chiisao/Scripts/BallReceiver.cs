using System;
using UnityEngine;

public class BallReceiver : MonoBehaviour
{
    private GameObject targetBall = null;

    private Animator animator;

    private void Start()
    {
        animator = transform.root.GetComponent<Animator>();
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
        animator.SetTrigger("BaseballHit");
        animator.SetBool("Running", true);
        targetBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 30f, 100f), ForceMode.Impulse);
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
