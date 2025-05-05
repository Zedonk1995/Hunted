using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    private UIHandler uiHandler;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;

    private float health;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        TryGetComponent<UIHandler>(out uiHandler);
    }

    public void OnHit( float damage )
    {
        health -= damage;

        if (uiHandler != null)
        {
            uiHandler.UpdateHealth(health, maxHealth);
        }

        /*
         * you can only die if you are currently alive.  There is no such thing
         * as dying twice (simultaneously).
         */
        if ( health <= 0 && isAlive )
        {
            isAlive = false;
            TryGetComponent(out IDeath deathComponent);
            deathComponent?.Die();
        }
    }
}
