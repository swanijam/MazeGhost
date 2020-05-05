using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionStretch : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float stretchFactor = .3f;
    public float stretchLerpFactor = .98f;
    public Transform stretchTransform;
    public Vector2 velocityMagnitudeRange;
    // Vector3 startScale = 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector3 prevVelocity = Vector3.zero;
    // Update is called once per frame
    float stretch = 0f;
    float stretchTarget = 1f;
    void Update()
    {
        Vector3 acceleration = rigidbody.velocity - prevVelocity;
        transform.rotation = Quaternion.LookRotation(acceleration);
        stretchTarget = Mathf.InverseLerp(velocityMagnitudeRange.x, velocityMagnitudeRange.y, rigidbody.velocity.magnitude);
        stretch = Mathf.Lerp(stretch, stretchTarget, .98f);
        stretchTransform.localScale = new Vector3(1f, 1f, 1f+stretch*stretchFactor);
    }

    private void OnCollisionEnter(Collision other)
    {
        stretch = 0f;
    }
}
