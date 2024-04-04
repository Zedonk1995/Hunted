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
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextSpawnTime)
        {
            NextSpawnTime = Time.time + SpawnTimeInterval;

            int chosenSpawnPointIndex = UnityEngine.Random.Range(0, SpawnPointList.Length);
            Vector3 chosenSpawnPoint = SpawnPointList[chosenSpawnPointIndex].position;
            Instantiate(BitetyThing, chosenSpawnPoint, Quaternion.identity);
        }
    }
}
