   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// --------------------- HORIZONTAL MOUSELOOK -----------------------
public class LookHorizontal : MonoBehaviour
{
    ILookHorizontalInput input = null;

    float mouseX;
    float yaw = 0f;

    private readonly float mouseSensitivity = 200.0f;

    Vector2 LookInput = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<ILookHorizontalInput>();

        Cursor.lockState = CursorLockMode.Locked;

        // I'm not sure this is needed.
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookInput = input.LookInput;
        mouseX = LookInput.x * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;

        Quaternion rotation = this.transform.localRotation;

        this.transform.localRotation = Quaternion.Euler(rotation.x, yaw, rotation.z);
    }
}
