using System;
using UnityEngine;

public class BallReceiver : MonoBehaviour
{
    private GameObject targetBall = null;

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
        throw new NotImplementedException();
    }

    private void BallMiss()
    {
        Debug.Log("Miss");
        throw new NotImplementedException();
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
