using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    private float health = 10f;

    public void OnHit( float damage )
    {
        health -= damage;

        if ( health <= 0 )
        {
            IDeath deathComponent = GetComponent<IDeath>();
            deathComponent.Die();
        }
    }
}
