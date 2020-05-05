using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraLook : MonoBehaviour
{
    public float lookSpeedH = 150f;
    public float lookSpeedV = 50f;
    float curX = 0f;
    float curY = 0f;
    // Update is called once per frame
    bool cursorLocked = false;
    private void Start()
    {
    }
    
    void Update()
    {

        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        curX += inputX;
        curY += inputY;

        transform.eulerAngles = new Vector3(-curY, curX, 0f);
    }
}