using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishImpact : MonoBehaviour
{
    public bool heldInHand;
    public Rigidbody rigidbody;
    public float radius = .5f;
    public float exitBuffer = .1f;
    [Range(0f, 1f)]
    public float bounce;
    public float velocityThreshold = 1f;
    [Range(0f, 1f)]
    public float dotProductThreshold = .15f;
    public bool flipOnImpact = true;
    bool flip = false;

    public GameObject onHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHeld(bool held) {
        heldInHand = held;
            stretchTransform.localScale = Vector3.one;
        if (heldInHand) {
            rigidbody.velocity = Vector3.zero;
            // stretchTransform.localScale = Vector3.one;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            StopAllCoroutines();
        } else {
            // stretchTransform.localScale = Vector3.one;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }
    Vector3 lastFrameVelocity;
    // Update is called once per frame
    bool squishing = false;
    public float maxVelocity = 0;
    void Update()
    {
        if (heldInHand) return;
        Vector3 acceleration = rigidbody.velocity - lastFrameVelocity;
        if (!squishing) transform.rotation = Quaternion.LookRotation((flipOnImpact && flip ? -1 : 1) * rigidbody.velocity);
        stretchTarget = Mathf.InverseLerp(velocityMagnitudeRange.x, velocityMagnitudeRange.y, rigidbody.velocity.magnitude);
        stretch = Mathf.Lerp(stretch, stretchTarget, .98f);
        if (!squishing) stretchTransform.localScale = new Vector3(1f, 1f, 1f+rigidbody.velocity.magnitude*stretchFactor);

        lastFrameVelocity = rigidbody.velocity;
        if (lastFrameVelocity.magnitude > maxVelocity) maxVelocity = lastFrameVelocity.magnitude;
    }

    private void OnCollisionEnter(Collision other) {
        if (heldInHand) return;
        stretch = 0f;
        if (lastFrameVelocity.magnitude < velocityThreshold) return;
        Instantiate(onHitEffect, other.GetContact(0).point, Quaternion.LookRotation(other.GetContact(0).normal));
        float dot = Vector3.Dot(lastFrameVelocity.normalized, -(other.GetContact(0).point-transform.position).normalized);
        // Debug.Log("dot product : " + dot);
        Vector3 reflect = Vector3.Reflect(lastFrameVelocity, other.GetContact(0).normal);
        // Debug.Log(lastFrameVelocity +"("+ lastFrameVelocity.magnitude +"), "+ reflect +"("+reflect.magnitude +"), "+(reflect.magnitude-lastFrameVelocity.magnitude));
        if (dot > -dotProductThreshold) return;
        // if (!once) 
        float squishAmount = Mathf.InverseLerp(velocityMagnitudeRange.x, velocityMagnitudeRange.y, lastFrameVelocity.magnitude);
        StartCoroutine(BeginSquishRoutine(squishAmount, reflect, other.GetContact(0).normal, other.GetContact(0).point + other.GetContact(0).separation*other.GetContact(0).normal));
    }

    [Range(0f, 1f)]
    public float lerpVal;
    public AnimationCurve squishCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float squishTime = 1f;
    bool once = false;
    public IEnumerator BeginSquishRoutine(float squishAmount, Vector3 reflect, Vector3 normal, Vector3 point) {
        squishing = true;
        Vector3 ivel = lastFrameVelocity;
        yield return null;
        // Debug.Log("ahhh");
        transform.rotation = Quaternion.LookRotation((flipOnImpact && flip ? -1 : 1) * -normal);
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        Vector3 iPos = point + normal * radius;
        Vector3 finalPos = iPos - normal * radius; 
        transform.position = finalPos;
        stretchTransform.localScale = new Vector3(1f, 1f, .2f);
        float currTime = 0f;
        // lerpVal;
        float _squishTime = squishTime * squishAmount;
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        while (currTime < _squishTime/2f) {
            currTime += Time.deltaTime;
            lerpVal = squishCurve.Evaluate(Mathf.InverseLerp(0f, _squishTime, currTime));
            transform.position = Vector3.Lerp(finalPos, iPos, Remap(lerpVal, squishAmount));
            stretchTransform.localScale = new Vector3(1f, 1f, Remap(lerpVal, squishAmount) );
            yield return wfeof;
        }
        
        // currTime = 0f;
        while (currTime < _squishTime) {
            currTime += Time.deltaTime;
            lerpVal = squishCurve.Evaluate(Mathf.InverseLerp(0f, _squishTime, currTime));
            transform.position = Vector3.Lerp(finalPos, iPos, Remap(lerpVal, squishAmount));
            stretchTransform.localScale = new Vector3(1f, 1f, Remap(lerpVal, squishAmount) );
            yield return wfeof;
        }
        // rigidbody.transform.position +
        yield return null;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        Vector3 offset = reflect.normalized * radius;
        rigidbody.transform.position += offset;
        rigidbody.velocity = reflect * bounce;
        // Debug.Log(lastFrameVelocity +"/// "+reflect);
        once = true;
        squishing = false;
        stretch = 0f;
        flip = !flip;
    }

    float Remap(float v, float scale) {
        return 1f-scale + v * scale;
    }

    // public Rigidbody rigidbody;
    public float stretchFactor = .3f;
    public float stretchLerpFactor = .98f;
    public Transform stretchTransform;
    public Vector2 velocityMagnitudeRange;
    
    Vector3 prevVelocity = Vector3.zero;
    // Update is called once per frame
    float stretch = 0f;
    
    float stretchTarget = 1f;
}
