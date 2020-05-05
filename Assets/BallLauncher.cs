using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    // public Rigidbody ball;
    public SquishImpact ball_squish;

    public float launchSpeed = 5f;
    public bool ballInHand = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (ballInHand) {
                ball_squish.transform.SetParent(null);
                ball_squish.SetHeld(false);
                ball_squish.rigidbody.velocity = transform.forward * launchSpeed;
                ballInHand = false;
            } else {
                ball_squish.SetHeld(true);
                ball_squish.transform.SetParent(transform);
                ball_squish.transform.position = transform.position;
                ballInHand = true;
            }
        }   
    }
}
