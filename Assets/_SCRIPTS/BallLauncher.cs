using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody ball;

    public float launchSpeed = 5f;
    public bool ballInHand = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (ballInHand) {
                ball.transform.SetParent(null);
                ball.velocity = transform.forward * launchSpeed;
                ball.isKinematic = false;
                ball.useGravity = true;
                ballInHand = false;
            } else {
                ball.transform.SetParent(transform);
                ball.transform.position = transform.position;
                ball.velocity = Vector3.zero;
                ball.isKinematic = true;
                ball.useGravity = false;
                ballInHand = true;
            }
        }   
    }
}
