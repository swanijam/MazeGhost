using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_EDITOR
            ToggleCursorLock();
        #endif
        
    }
    public bool cursorLocked = false;
    public void ToggleCursorLock() {
        cursorLocked = !cursorLocked;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) { // middle mouse button toggle cursor lock
            ToggleCursorLock();
        }
    }
}
