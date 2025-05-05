using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] GameObject SpawnPointListGameObject = null;
    private Transform[] SpawnPointList;

    [Header("Prefab References")]
    [SerializeField] GameObject BitetyThing = null;

    private float SpawnTimeInterval = 10.0f;
    private float NextSpawnTime = float.NaN;


    // Start is called before the first frame update
    void Start()
    {
        if ( SpawnPointListGameObject != null )
        {
            SpawnPointList = SpawnPointListGameObject.GetComponentsInChildren<Transform>();
        }

        NextSpawnTime = SpawnTimeInterval;


        // spawn initial enemies
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextSpawnTime)
        {
            SpawnTimeInterval = Math.Max( SpawnTimeInterval * 0.6f, 2.5f );
            NextSpawnTime = Time.time + SpawnTimeInterval;

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int chosenSpawnPointIndex = UnityEngine.Random.Range(0, SpawnPointList.Length);
        Vector3 chosenSpawnPoint = SpawnPointList[chosenSpawnPointIndex].position;
        Instantiate(BitetyThing, chosenSpawnPoint, Quaternion.identity);
    }
}
