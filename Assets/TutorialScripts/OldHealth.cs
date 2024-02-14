using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OldIHealth
{
    void TakeDamage(float damage);
}


public class OldHealth : MonoBehaviour, OldIHealth
{
    private float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
