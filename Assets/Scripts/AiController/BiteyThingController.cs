using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BiteyThingController : MonoBehaviour, ILandMovementInput
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidBody = null;

    AStarPathFinder aStarPathFinder = null;

    public Vector2 MoveInput { get; private set;  }

    public bool JumpIsPressed { get; private set; }

    GameObject player;
    public GameObject AttackOrigin;

    Vector3 direction;
    Vector3 horizontalDirection;

    private const float pathCalculationInterval = 0.1f;
    private float timeSinceLastPathCalculation = -pathCalculationInterval;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        myRigidBody = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");

        TryGetComponent(out aStarPathFinder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!aStarPathFinder)
        {
            Debug.Log("No AStarPathFinder Script detected!  Using simple movement.");
            direction = player.transform.position - transform.position;
            horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

            this.transform.rotation = Quaternion.LookRotation(horizontalDirection);
            MoveInput = Vector2.up;
            return;
        }

        if (Time.time - timeSinceLastPathCalculation > pathCalculationInterval)
        {
            timeSinceLastPathCalculation = Time.time;
            aStarPathFinder.CalculatePath(transform.position, player.transform.position);
        }

        // direction of path in 3d coordinates
        Vector3 directionOfPath = aStarPathFinder.GetDirectionOfPath();
        this.transform.rotation = Quaternion.LookRotation(directionOfPath);

        MoveInput = Vector2.up;

    }
}
