using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

enum aiStrat
{
    Intercept,
    CircleClockwise,
    CircleAntiClockwise,
    Erratic
}


/// <summary>
/// This class is poorly written as I was just messing around :P
/// </summary>
public class Ai : MonoBehaviour
{

    // Start is called before the first frame update
    private Rigidbody myRigidbody;
    public float maxSpeed => 40f;

    public static List<Ai> RoboAis = new List<Ai>();


    private Vector3 PlayerAiVector => PlayerController.player.transform.position - transform.position;
    private Vector3 PlayerHorizontalAiVector => Vector3.ProjectOnPlane(PlayerAiVector, Vector3.up);

    private aiStrat currentStrategy = aiStrat.Erratic;

    private float stratChangeTime = 0f;
    private float timeAtLastStratChange = 0f;




    void Start()
    {
        RoboAis.Add(this);

        myRigidbody = GetComponent<Rigidbody>();

    }

    private void OnDestroy()
    {
        RoboAis.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {


        var accelration = 40f;



        var dragFactor = accelration / maxSpeed;

        var horizontalVelocity = myRigidbody.velocity;
        horizontalVelocity.y = 0;
        SetStrategy();
        var direction = GetNormalizedDirection();



        myRigidbody.AddForce(dragFactor * (maxSpeed * direction - horizontalVelocity));
        RotateToPlayer();
    }

    public void SetStrategy()
    {
        if(Time.time >= stratChangeTime + timeAtLastStratChange)
        {
            stratChangeTime = 5f * UnityEngine.Random.value;
            timeAtLastStratChange = Time.time;

            Array values = Enum.GetValues(typeof(aiStrat));
            var randomStrat = (aiStrat)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            currentStrategy = randomStrat;
        }
        
    }



    private Vector3 GetNormalizedDirection()
    {
        Vector3 GetDirection()
        {
            var otherAis = RoboAis.Where(x => x != this);
            if(otherAis.Any())
            {
                var closestAiVectors = otherAis.Select(x => x.transform.position - this.transform.position).OrderBy(x => x.magnitude).ToList();
                var closest = closestAiVectors.First();
                if (closest.magnitude < 10f)
                {
                    return -closest;
                }
                if (PlayerAiVector.magnitude > 50f)
                {
                    return PlayerAiVector;
                }
                if (closest.magnitude > 20f)
                {
                    return closest;
                }
                else if (closestAiVectors.Count >= 2 && closestAiVectors[1].magnitude > 25f)
                {
                    return closestAiVectors[1];
                }
            }



            if (currentStrategy == aiStrat.CircleAntiClockwise)
            {
                return GetCircleDirection(false);
            }
            if (currentStrategy == aiStrat.CircleClockwise)
            {
                return GetCircleDirection(true);
            }
            if (currentStrategy == aiStrat.Intercept)
            {
                return PlayerAiVector;
            }
            return GetErraticDirection();
        }

        var direction = GetDirection();

        direction.y = 0;
        return direction.normalized;

    }


    private Vector3 GetErraticDirection()
    {
        return UnityEngine.Random.onUnitSphere + 0.5f * this.myRigidbody.velocity.normalized + 0.1f * PlayerHorizontalAiVector.normalized;
    }

    private void RotateToPlayer()
    {
        transform.rotation = Quaternion.LookRotation(PlayerHorizontalAiVector);
    }

    public Vector3 GetCircleDirection(bool clockwise)
    {
        var perpPlayerAiVector = Vector3.Cross(PlayerHorizontalAiVector, Vector3.up).normalized;
        return clockwise ? perpPlayerAiVector : -perpPlayerAiVector;
    }

    /*
    public Vector3 GetInterceptDirection()
    {
        //Not accurate just a vague intercept attempt.  Redo or delete if you want.
        var PlayerAiDistance = PlayerAiVector.magnitude;
        var timeTakenToReachPlayer = PlayerAiDistance / maxSpeed;
        var approxProjectedPlayerPosition = timeTakenToReachPlayer * PlayerController.player.myRigidbody.velocity + PlayerController.player.transform.position;

        return (approxProjectedPlayerPosition - transform.position).normalized;


    }*/

}

