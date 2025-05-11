using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    IFireInput input = null;
    [SerializeField] GameObject bulletPrefab;

    GameObject Owner;
    Transform BulletOrigin;

    readonly float fireCooldown = 0.2f;
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
        for (int i = 0; i < 10; i++)
        {
            float randomAngleBound = 5;
            Quaternion randomRotation = Quaternion.Euler(
                new Vector3(
                        Random.Range(-randomAngleBound, randomAngleBound),
                        Random.Range(-randomAngleBound, randomAngleBound),
                        0
                    )
                ) ;

            GameObject bullet = Instantiate(bulletPrefab, BulletOrigin.position, BulletOrigin.rotation * randomRotation);
            bullet.GetComponent<BulletScript>().SetOwner(Owner);
        }

        timeLastFired = Time.time;
    }
}
