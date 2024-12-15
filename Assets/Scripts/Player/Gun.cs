using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    IFireInput input = null;
    [SerializeField] GameObject bulletPrefab;

    GameObject Owner;
    Transform BulletOrigin;

    readonly float fireCooldown = 0.05f;
    float timeLastFired = 0f;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponentInParent<IFireInput>();
        BulletOrigin = transform.Find("BulletOrigin").transform;
        Owner = GameObject.Find("Player");
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
        GameObject bullet = Instantiate(bulletPrefab, BulletOrigin);
        bullet.GetComponent<BulletScript>().SetOwner(Owner);

        timeLastFired = Time.time;
    }
}
