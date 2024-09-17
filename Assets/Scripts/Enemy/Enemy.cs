using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    
   [SerializeField] private float speed;
   [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;
    private EnemyAwareness enemyAwareness;
    private Vector2 targetDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAwareness = GetComponent<EnemyAwareness>();
    }

    private void FixedUpdate()
    {
        if (!IsServer)
        {
            return;
        }

        UpdateTargetDirection();
        RotationTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (enemyAwareness.awareOfPlayer)
        {
            targetDirection = enemyAwareness.directionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }

        UpdateTargetDirectionClientRpc(targetDirection);
    }

    private void RotationTowardsTarget()
    {
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        UpdateRotationClientRpc(rotation);
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            UpdateVelocityClientRpc(Vector2.zero);
        }
        else
        {
            UpdateVelocityClientRpc(transform.up * speed);
        }
    }

    [ClientRpc]

    private void UpdateRotationClientRpc(Quaternion rotation)
    {
        rb.SetRotation(rotation);
    }

    [ClientRpc]

    private void UpdateVelocityClientRpc(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    [ClientRpc]

    private void UpdateTargetDirectionClientRpc(Vector2 direction)
    {
        targetDirection = direction;
    }

}
