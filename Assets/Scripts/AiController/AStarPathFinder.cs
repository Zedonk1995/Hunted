using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class AStarPathFinder : MonoBehaviour
{
    private Seeker seeker = null;

    private Path path;

    private int currentWaypointIndex = 0;

    // objects within this radius of a waypoint are considered to be close to the waypoint
    public float closeWaypointRadius = 0.5f;

    public bool reachedEndOfPath;

    public void CalculatePath(Vector3 currentPosition, Vector3 targetPosition)
    {
        // add checks that script exists before executing
        if (TryGetComponent<Seeker>(out seeker))
        {
            seeker.StartPath(currentPosition, targetPosition, OnPathComplete);
        } else
        {
            Debug.LogError("No seeker component found!");
        }
    }

    public void OnPathComplete(Path newPath)
    {
        if (newPath.error)
        {
            Debug.Log("Path error: " + newPath.error);
            return;
        }
        path = newPath;
        // Reset waypoint counter
        currentWaypointIndex = 0;
    }

    // perf - use square distance and perhaps don't call project on plane quite so much
    // returns the horizontal direction to the next waypoint in the path
    public Vector3 GetDirectionOfPath()
    {
        // project current position onto xz-plane
        Vector3 projectedCurrentPosition = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        // path is only defined after async StartPath is called so path may not be defined yet
        if (path == null)
        {
            return Vector3.zero;     
        }

        // find whether we've reached the next waypoint.  If we have, then go to the next waypoint.
        // This next waypoint isn't necessarily the next waypoint in vectorPath array as we might reach
        // multiple waypoints in the same frame.
        int finalWaypointIndex = path.vectorPath.Count - 1;
        for (int waypointIndex = currentWaypointIndex; waypointIndex <= finalWaypointIndex; waypointIndex++)
        {
            Vector3 projectedNextWaypointPosition = Vector3.ProjectOnPlane(path.vectorPath[waypointIndex], Vector3.up);

            // perf - avoid using square root
            float distanceToWaypoint = Vector3.Distance(projectedCurrentPosition, projectedNextWaypointPosition);

            if (distanceToWaypoint > closeWaypointRadius)   
            {
                currentWaypointIndex = waypointIndex;
                break;
            }
        }

        Vector3 projectedFinalWaypointPosition = Vector3.ProjectOnPlane(path.vectorPath[finalWaypointIndex], Vector3.up);
        float lastWaypontDistance = Vector3.Distance(projectedCurrentPosition, projectedFinalWaypointPosition);

        // if you're near the final waypoint then you've reached the end of the path
        reachedEndOfPath = lastWaypontDistance < closeWaypointRadius;

        Vector3 projectedCurrentWaypointPosition = Vector3.ProjectOnPlane(path.vectorPath[currentWaypointIndex], Vector3.up);
        Vector3 directionOfCurrentWaypoint = (projectedCurrentWaypointPosition - projectedCurrentPosition).normalized;

        // once you're close to the last waypoint, slow down smoothly until you reach it
        float endOfPathSpeedFactor = reachedEndOfPath ? lastWaypontDistance / closeWaypointRadius : 1f;
        Vector3 moveInput = endOfPathSpeedFactor * directionOfCurrentWaypoint;

        return moveInput;
    }
}
