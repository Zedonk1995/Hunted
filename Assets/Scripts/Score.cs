using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    static public int KillCount = 0;
    static public float GameTime { get; private set; } = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.fixedDeltaTime;
    }
}
