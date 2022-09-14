using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMissileScript : MonoBehaviour
{
    private MovementScript movementScript;

    private GameObject player;
    private Vector3 PlayerAiVector => player.transform.position + new Vector3( 0, 0.25f ) - transform.position;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MovementScript>();

        movementScript.maxSpeed = 20.0f;
        movementScript.dragCoefficient = 1.5f;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(PlayerAiVector);
        movementScript.directionOfPropulsion = transform.TransformVector(new Vector3(0, 0, 1)).normalized;
    }
}
