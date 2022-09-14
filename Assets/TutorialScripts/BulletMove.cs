using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletMove : MonoBehaviour
{
    float speed = 75.0f;
    float lifespan = 10.0f;
    float birthTime;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed * transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += speed * Time.fixedDeltaTime * transform.forward;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float damage = 10f;
        var otherObject = collision.gameObject;
        otherObject.GetComponent<IHealth>()?.TakeDamage(damage);

        Destroy(gameObject);
    }


    void Update()
    {
        if(Time.time > birthTime + lifespan)
        {
            Destroy(gameObject);
        }
    }
}
