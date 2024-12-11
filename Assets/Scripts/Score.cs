using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private UIHandler uiHandler;

    public int KillCount { get; private set; } = 0;
    static public float GameTime { get; private set; } = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        player.TryGetComponent(out uiHandler);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.fixedDeltaTime;
    }

    public void SetKillCount(int newKillCount)
    {
        uiHandler.UpdateKillCount(newKillCount);
        KillCount = newKillCount;
    }
}
