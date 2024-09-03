using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAwareness : NetworkBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float playerAwarnessDistance;

    private Transform player;

    private void Start()
    {
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
        if (player != null)
        {
            Vector2 enemyToPlayerVector = player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= playerAwarnessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }
}
