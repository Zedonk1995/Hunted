using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private UIHandler uiHandler;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        player.TryGetComponent(out uiHandler);

        // reset scores
        Global.KillCount = 0;
        Global.GameTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Global.GameTime += Time.fixedDeltaTime;
        uiHandler.UpdateGameTimer(Global.GameTime);
    }

    public void SetKillCount(int newKillCount)
    {
        uiHandler.UpdateKillCount(newKillCount);
        Global.KillCount = newKillCount;
    }
}
