using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    IIsAttacking animatorController = null;

    public Transform BulletOrigin;
    public GameObject bulletPrefab;
    float timeFiredInterval = 0.1f;
    float timeLastFired = 0f;

    float attackDelay = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponentInParent<IIsAttacking>();
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

            if (animatorController != null && !animatorController.IsAttacking)
            {
                animatorController.IsAttacking = true;

                Invoke(nameof(AttackComplete), attackDelay);
            }
        }
    }

    private void AttackComplete()
    {
        animatorController.IsAttacking = false;
    }
}
