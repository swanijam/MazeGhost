using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveController : MonoBehaviour
{
    public float moveSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        transform.position = transform.position + GetInputDirection() * moveSpeed * Time.deltaTime;
    }

    public Vector3 GetInputDirection() {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.A)) {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction += transform.right;
        }
        direction = Vector3.ProjectOnPlane(direction, Vector3.up);
        if (Input.GetKey(KeyCode.Space)) {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            direction -= Vector3.up;
        }
        return direction.normalized;
    }
}
