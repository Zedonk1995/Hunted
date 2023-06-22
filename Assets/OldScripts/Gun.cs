using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    AnimatorController animatorController = null;
    Animator animator = null;

    public Transform BulletOrigin;
    public GameObject bulletPrefab;
    float timeFiredInterval = 0.1f;
    float timeLastFired = 0f;

    float attackDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponentInParent<AnimatorController>();
        animator = GetComponentInParent<Animator>();
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

            bool isAttacking = animatorController.IsAttacking;

            if (!isAttacking)
            {
                animatorController.ChangeAnimationState(AnimatorController.StateSelector.Attack);

                animatorController.IsAttacking = true;
                Invoke("AttackComplete", attackDelay);
            }
        }
    }

    private void AttackComplete()
    {
        animatorController.IsAttacking = false;
        animatorController.ChangeAnimationState(AnimatorController.StateSelector.Idle);
    }



}
