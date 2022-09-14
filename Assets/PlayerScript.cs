using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Player { get; private set; }


    private MovementScript movementScript;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MovementScript>();

        movementScript.maxSpeed = 10.0f;

        Player = this;
    }

    private void FixedUpdate()
    {
       

        float wOrS = Input.GetAxis("Vertical");
        float aOrD = Input.GetAxis("Horizontal");

       movementScript.directionOfPropulsion = Vector3.ProjectOnPlane(transform.TransformVector(new Vector3(aOrD, 0, wOrS)), Vector3.up).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
