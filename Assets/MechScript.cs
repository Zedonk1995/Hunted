using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechScript : MonoBehaviour
{
    private MovementScript movementScript;

    private GameObject player;
    private Vector3 PlayerAiVector => player.transform.position - transform.position;
    private Vector3 PlayerHorizontalAiVector => Vector3.ProjectOnPlane(PlayerAiVector, Vector3.up);

    public Transform BulletOrigin;
    public GameObject bulletPrefab;
    float timeFiredInterval = 0.5f;
    float timeLastFired = 0f;


    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MovementScript>();

        movementScript.maxSpeed = 20.0f;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(PlayerHorizontalAiVector);
        movementScript.directionOfPropulsion = Vector3.ProjectOnPlane(transform.TransformVector(new Vector3(0, 0, 1)), Vector3.up).normalized;


        if (Time.time >= timeLastFired + timeFiredInterval)
        {
            timeLastFired = Time.time;
            Instantiate(bulletPrefab, BulletOrigin.position, BulletOrigin.rotation);
        }

    }
}
