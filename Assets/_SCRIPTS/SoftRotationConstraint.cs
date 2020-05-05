using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftRotationConstraint : MonoBehaviour
{
    Quaternion relative;
    // Start is called before the first frame update
    void Start()
    {
        relative = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    public Transform target;
    public float lerpFactor = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation * relative, lerpFactor);
    }
}
