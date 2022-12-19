using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Rigidbody myRigidbody = null;

    float mouseX;
    float mouseY;

    private float mouseSensitivity = 10.0f;

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

    }

    private void FixedUpdate()
    {

        LookInput = input.LookInput;
        mouseX = LookInput.x * mouseSensitivity;
        mouseY = -LookInput.y * mouseSensitivity;


        //Debug.Log(mouseX);
        //Debug.Log(mouseY);
        //Debug.Log(this.transform.position);

        myRigidbody.transform.Rotate(Vector3.up * mouseX, Space.World);
        myRigidbody.transform.Rotate(Vector3.right * mouseY);
    }
}
