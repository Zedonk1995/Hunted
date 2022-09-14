using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Transform bulletOrigin;
    float timeFiredInterval = 0.1f;
    float timeLastFired = 0f;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bulletOrigin = transform.Find("BulletOrigin");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time >= timeLastFired + timeFiredInterval)
            {
                timeLastFired = Time.time;
                FireWeapon();
                Invoke("FireWeapon", 0.02f);
                Invoke("FireWeapon", 0.04f);
                Invoke("FireWeapon", 0.06f);
                Invoke("FireWeapon", 0.08f);

            }
        }
    }

    private void FireWeapon()
    {
        
        var playerRotation = transform.rotation;
        var smallDeviation = Quaternion.Slerp(playerRotation, UnityEngine.Random.rotation, 0.05f);

        var bullet = Instantiate(bulletPrefab, bulletOrigin.position, smallDeviation);
    }
}
