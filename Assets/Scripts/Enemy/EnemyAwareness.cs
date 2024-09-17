using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAwareness : NetworkBehaviour
{
    public bool awareOfPlayer { get; private set; }

    public Vector2 directionToPlayer { get; private set; }

    [SerializeField] private float playerAwarnessDistance;

    private Transform player;

    private void Start()
    {
        if (!IsServer)
        {
            return;
        }

        Invoke(nameof(FindPlayer), 1.0f);
    }

    private void FindPlayer()
    {
        Player maybePlayer = FindObjectOfType<Player>();

        if (maybePlayer != null)
        {
            player = maybePlayer.transform;
            return;
        }

        Invoke(nameof(FindPlayer), 1.0f);
    }

    void Update()
    {
        if (!IsServer)
        { 
            return; 
        }

        if (player != null)
        {
            Vector2 enemyToPlayerVector = player.position - transform.position;
            directionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= playerAwarnessDistance)
            {
                awareOfPlayer = true;
            }
            else
            {
                awareOfPlayer = false;
            }

            UpdateAwarenessClientRpc(awareOfPlayer, directionToPlayer);
        }
    }

    [ClientRpc]

    private void UpdateAwarenessClientRpc(bool awareOfPlayer, Vector2 directionToPlayer)
    {
        this.awareOfPlayer = awareOfPlayer;
        this.directionToPlayer = directionToPlayer;
    }
}
