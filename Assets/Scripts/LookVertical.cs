using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------- VERTICAL MOUSELOOK -----------------------
public class LookVertical : MonoBehaviour
{
    ILookVerticalInput input = null;

    float mouseY;
    float pitch = 0f;

    private float mouseSensitivity = Global.MouseSensitivity;

    Vector2 LookInput = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<ILookVerticalInput>();
    }

    // Update is called once per frame
    void Update()
    {
        LookInput = input.LookInput;
        mouseY = -LookInput.y * mouseSensitivity * Time.deltaTime;

        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        Quaternion rotation = this.transform.localRotation;

        this.transform.localRotation = Quaternion.Euler(pitch, rotation.y, rotation.z);
    }
}
