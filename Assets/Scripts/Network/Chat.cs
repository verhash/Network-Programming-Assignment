using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Chat : NetworkBehaviour 
{
    [SerializeField] private GameObject wavePrefab;
    [SerializeField] private Transform chatPoint;

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnWaveServerRPC(chatPoint.position, chatPoint.rotation);
        }
    }

    [ServerRpc]

    private void SpawnWaveServerRPC(Vector2 position, Quaternion rotation)
    {
        GameObject wave = Instantiate(wavePrefab, chatPoint.transform.position, wavePrefab.transform.rotation);
        wave.GetComponent<NetworkObject>().Spawn();
    }
}
