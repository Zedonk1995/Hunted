using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BiteyThingController : MonoBehaviour, ILandMovementInput
{
    public Vector2 MoveInput { get; private set;  }

    public bool JumpIsPressed { get; private set; }

    Rigidbody myRigidBody = null;

    GameObject player;
    Vector3 direction;
    Vector3 horizontalDirection;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        direction = player.transform.position - transform.position;
        horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

        this.transform.rotation = Quaternion.LookRotation(horizontalDirection);
        MoveInput = Vector2.up;
    }
}
