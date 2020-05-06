using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    // public Rigidbody ball;
    public SquishImpact ball_squish;
    public TrailRenderer ballTrail;
    public float launchSpeed = 5f;
    public bool ballInHand = true;

    private Quaternion startRotation;

    private void Start() {
        startRotation = ball_squish.transform.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (ballInHand) {
                ballTrail.emitting = true;
                ball_squish.transform.SetParent(null);
                ball_squish.SetHeld(false);
                ball_squish.rigidbody.velocity = transform.forward * launchSpeed;
                ballInHand = false;
            } else {
                ballTrail.emitting = false;
                ball_squish.transform.SetParent(transform);
                ball_squish.SetHeld(true);
                ball_squish.transform.localPosition = Vector3.zero;
                ball_squish.transform.localRotation = startRotation;
                ballInHand = true;
            }
        }   
    }
}
