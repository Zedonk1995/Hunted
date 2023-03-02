   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------- HORIZONTAL MOUSELOOK -----------------------
public class LookHorizontal : MonoBehaviour
{
    Rigidbody myRigidbody = null;

    float mouseX;

    private float mouseSensitivity = 50.0f;

    Vector2 LookInput = Vector2.zero;

    [SerializeField] PlayerLandInput input;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        // I'm not sure this is needed.
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookInput = input.LookInput;
        mouseX = LookInput.x * mouseSensitivity * Time.deltaTime;

        myRigidbody.transform.Rotate(Vector3.up * mouseX, Space.World);
    }

    private void FixedUpdate()
    {

    }
}
