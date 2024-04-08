using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [Header("Health")]
    [SerializeField] private float health = 100f;

    public void OnHit( float damage )
    {
        health -= damage;

        if ( health <= 0 )
        {
            TryGetComponent(out IDeath deathComponent);
            deathComponent?.Die();
        }
    }
}
