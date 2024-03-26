using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BiteyThingController : MonoBehaviour, ILandMovementInput
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidBody = null;
    Animator myAnimator = null;

    AStarPathFinder aStarPathFinder = null;

    public Vector2 MoveInput { get; private set; }

    public bool JumpIsPressed { get; private set; }

    GameObject player;
    public GameObject attackOrigin;

    Vector3 direction;
    Vector3 horizontalDirection;

    private const float pathCalculationInterval = 0.1f;
    private float timeSinceLastPathCalculation = -pathCalculationInterval;

    public float attackRange = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        myRigidBody = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        TryGetComponent(out aStarPathFinder);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float distanceToTarget = GetDistanceToTarget();

        if ( distanceToTarget > attackRange - 0.5 )
        {
            ChaseTarget();
            return;
        }
        LookAtTarget();
        Attack();
    }

    void ChaseTarget()
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

    void LookAtTarget()
    {
        BoxCollider playerBoxCollider = player.GetComponent<BoxCollider>();

        Vector3 thisPosition = myBoxCollider.transform.position;
        Vector3 playerPosition = playerBoxCollider.transform.position;

        Vector3 horizontalDirectionToPlayer = Vector3.ProjectOnPlane(playerPosition - thisPosition, Vector3.up);
        this.transform.rotation = Quaternion.LookRotation(horizontalDirectionToPlayer);
    }

    void Attack()
    {
        myAnimator.SetTrigger("Attack");

        Vector3 halfBoxCastSize = myBoxCollider.size * 0.9f;
        halfBoxCastSize.z = 0.1f;

        float boxColliderTravelDistance = (myBoxCollider.size.z / 2) + attackRange - halfBoxCastSize.z;

        // this gets the 9th layer mask, for the nth layer mask use 1 << n
        int layerMask = 1 << 9;

        bool didHit = Physics.BoxCast(attackOrigin.transform.position, halfBoxCastSize, transform.forward, out RaycastHit enemyHit, myRigidBody.rotation, boxColliderTravelDistance, layerMask);

        if (!didHit)
        {
            return;
        }

        bool enemyHealthScriptExists = enemyHit.collider.TryGetComponent(out IHealth enemyHealthScript);

        if (!enemyHealthScriptExists)
        {
            return;
        }

        enemyHealthScript.OnHit(1f);
    }

    // Finds distance to target (which is the player) taking into account the size of the box colliders.  Distance is based on the positions of
    // the feet of both objects.
    // potential perf - use square of distance to avoid square roots
    float GetDistanceToTarget()
    {
        BoxCollider playerBoxCollider = player.GetComponent<BoxCollider>();

        Vector3 thisPosition = myBoxCollider.transform.position;
        Vector3 playerPosition = playerBoxCollider.transform.position;

        Vector3 thisGroundPosition = thisPosition;
        thisGroundPosition.y = thisPosition.y - myBoxCollider.size.y/2;
        Vector3 playerGroundPosition = playerPosition;
        playerGroundPosition.y = playerPosition.y - playerBoxCollider.size.y/2;

        return Vector3.Distance(thisGroundPosition, playerGroundPosition) - playerBoxCollider.size.z/2 - myBoxCollider.size.z/2;
    }
}
