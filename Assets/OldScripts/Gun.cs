using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletOrigin;
    public GameObject bulletPrefab;
    float timeFiredInterval = 0.1f;
    float timeLastFired = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if( Time.time >= timeLastFired + timeFiredInterval )
            {
                timeLastFired = Time.time;
                Instantiate(bulletPrefab, BulletOrigin.position, BulletOrigin.rotation);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
