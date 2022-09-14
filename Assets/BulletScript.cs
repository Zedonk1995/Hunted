using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    float speed = 20.0f;
    float timeCreated;
    float lifespan = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody myRigidbody;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = speed * transform.forward;

        timeCreated = Time.time;
    }

    private void FixedUpdate()
    {
        if ( Time.time >= timeCreated + lifespan )
        {
            Destroy( gameObject );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float damage = 1f;
        var otherObject = collision.gameObject;
        otherObject.GetComponent<Health>()?.onHit(damage);

        if (otherObject.tag != "Bullet" )
        {
            Destroy(gameObject);
        }
    }

}
