using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private float timeUntilSpawn;

    void Start()
    {
        if (!IsServer)
        {
            return;
        }

        SetTimeUntilSpawn();
    }

    void Update()
    {
        if (!IsServer)
        {
            return;
        }

        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyObject.GetComponent<NetworkObject>().Spawn();
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
