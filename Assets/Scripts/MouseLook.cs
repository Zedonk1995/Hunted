   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Rigidbody myRigidbody = null;

    float mouseX;
    float mouseY;

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
        mouseY = -LookInput.y * mouseSensitivity * Time.deltaTime;

        myRigidbody.transform.Rotate(Vector3.up * mouseX, Space.World);
        myRigidbody.transform.Rotate(Vector3.right * mouseY);
    }

    private void FixedUpdate()
    {

    }
}
