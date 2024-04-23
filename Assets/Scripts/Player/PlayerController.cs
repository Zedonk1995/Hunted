using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDeath
{
    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
