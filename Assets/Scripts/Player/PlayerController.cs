using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDeath
{
    public void Die()
    {
        Debug.Log("You died!   :( ");
    }
}
