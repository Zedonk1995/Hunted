using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;

    private float mouseSensitivity = 10.0f;

    

    // Start is called before the first frame update
    void Start()
    {
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
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;




        PlayerScript.Player.transform.Rotate(Vector3.up * mouseX, Space.World);
        PlayerScript.Player.transform.Rotate(Vector3.right * mouseY);
    }
}
