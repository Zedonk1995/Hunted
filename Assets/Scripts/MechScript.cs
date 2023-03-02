using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechScript : MonoBehaviour, ILandInput
{



    private GameObject player;
    private Vector3 PlayerAiVector => player.transform.position - transform.position;
    private Vector3 PlayerHorizontalAiVector => Vector3.ProjectOnPlane(PlayerAiVector, Vector3.up);

    public Vector2 MoveInput { get; private set; }

    public bool JumpIsPressed => false;

    //public Transform BulletOrigin;
    //public GameObject bulletPrefab;
    //float timeFiredInterval = 0.5f;
    //float timeLastFired = 0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(PlayerHorizontalAiVector);
        MoveInput = Vector2.up;


        //if (Time.time >= timeLastFired + timeFiredInterval)
        //{
        //    timeLastFired = Time.time;
        //    Instantiate(bulletPrefab, BulletOrigin.position, BulletOrigin.rotation);
        //}

    }
}
