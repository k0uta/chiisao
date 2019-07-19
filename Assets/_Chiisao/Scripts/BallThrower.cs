using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private Transform startingPosition;

    [SerializeField] private float throwForce;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var ball = Instantiate(ballPrefab);
            ball.transform.position = startingPosition.position;

            ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, throwForce), ForceMode.Impulse);
        }
    }
}
