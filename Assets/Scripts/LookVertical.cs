using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------- VERTICAL MOUSELOOK -----------------------
public class LookVertical : MonoBehaviour
{
   float mouseY;

    private float mouseSensitivity = 200.0f;

    Vector2 LookInput = Vector2.zero;

    [SerializeField] PlayerLandInput input;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookInput = input.LookInput;
        mouseY = -LookInput.y * mouseSensitivity * Time.deltaTime;

        this.transform.Rotate(Vector3.right * mouseY);
    }
}
