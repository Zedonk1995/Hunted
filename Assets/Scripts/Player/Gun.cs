using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    IFireInput input = null;
    [SerializeField] GameObject bullet;

    Transform BulletOrigin;

    float fireCooldown = 0.1f;
    float timeLastFired = 0f;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponentInParent<IFireInput>();
        BulletOrigin = transform.Find("BulletOrigin").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool fireInput = GetFireInput();
        if (fireInput)
        {
            if (Time.time >= timeLastFired + fireCooldown)
            {
                Fire();
            }
        }
    }

    bool GetFireInput()
    {
        return input.FireIsPressed;
    }

    void Fire()
    {
        Instantiate(bullet, BulletOrigin);
        timeLastFired = Time.time;
    }
}
