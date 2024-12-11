using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BiteyThingController : MonoBehaviour, ILandMovementInput, IDeath
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidBody = null;
    Animator myAnimator = null;

    AStarPathFinder aStarPathFinder = null;

    public Vector2 MoveInput { get; private set; }

    public bool JumpIsPressed { get; private set; }

    GameObject player;
    public Vector3 TargetPosition { get; private set; }
    bool shouldTargetDefault = true;

    public GameObject attackOrigin;

    Vector3 direction;
    Vector3 horizontalDirection;

    private const float pathCalculationInterval = 0.1f;
    private float timeSinceLastPathCalculation = -pathCalculationInterval;

    public readonly float attackRange = 2.5f;
    private readonly float attackCooldown = 1.0f;
    private float nextAttackTime = 0f;

    // how long the bitety thing has been directly chasing the player for when smart targeting is on.
    // -1 means the bitetything is currently not actively chasing the player
    public float directChaseTimer = -1f;
    public readonly float maxChaseTime = 5f;

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
        // if the attack is on cooldown then we assume that means that attack is still in progress
        // so the monster should not be moving.
        if (Time.time < nextAttackTime)
        {
            MoveInput = Vector2.zero;
            return;
        }

        if ( shouldTargetDefault )
        {
            TargetPosition = player.transform.position;
        }

        float distanceToTarget = GetDistanceToAttackTarget();

        if ( distanceToTarget > attackRange - 0.5 )
        {
            ChaseTarget();
            return;
        }

        Attack();
        nextAttackTime = Time.time + attackCooldown;        
    }

    void ChaseTarget()
    {
        if (!aStarPathFinder)
        {
            Debug.Log("No AStarPathFinder Script detected!  Using simple movement.");
            direction = TargetPosition - transform.position;
            horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

            this.transform.rotation = Quaternion.LookRotation(horizontalDirection);
            MoveInput = Vector2.up;
            return;
        }

        if (Time.time - timeSinceLastPathCalculation > pathCalculationInterval)
        {
            timeSinceLastPathCalculation = Time.time;
            aStarPathFinder.CalculatePath(transform.position, TargetPosition);
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

        LookAtTarget();

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

        enemyHit.collider.TryGetComponent(out IHealth enemyHealthScript);
        enemyHealthScript?.OnHit(20f);
    }

    // Finds distance to target (which is the player) taking into account the size of the box colliders.  Distance is based on the positions of
    // the feet of both objects.
    // potential perf - use square of distance to avoid square roots
    float GetDistanceToAttackTarget()
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

    public float GetXZDistanceToTargetPosition()
    {
        Vector3 thisPosition = myBoxCollider.transform.position;
        return Vector2.Distance(new Vector2(thisPosition.x, thisPosition.z), new Vector2(TargetPosition.x, TargetPosition.z));
    }

    public void Die()
    {
        Destroy(gameObject);

        GameObject global = GameObject.Find("Global");
        global.TryGetComponent(out Score score);

        if (score != null)
        {
            score.SetKillCount(Global.KillCount + 1);
        }
    }

    // sets the position the monster will try to navigate to.  By default it targets the player.
    public void SetTargetPosition(Vector3? newTargetPosition = null)
    {
        if ( newTargetPosition == null )
        {
            shouldTargetDefault = true;
            return;
        }

        // We know newTargetPosition is not null so target position is never the zero vector.
        // However, I had to make it default to Vector3.zero just so the code wouldn't error.
        TargetPosition = newTargetPosition ?? Vector3.zero;
        shouldTargetDefault = false;
    }
}
