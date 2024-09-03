using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bullet;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        base.OnNetworkSpawn();
        GetComponent<Rigidbody2D>().velocity = this.transform.forward * bulletSpeed;

        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        if (IsServer)
        {
            yield return new WaitForSeconds(2f);
            NetworkObject.Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() && IsServer)
        {
            if (collision.TryGetComponent(out NetworkObject networkObject))
            {
                if (networkObject.IsSpawned)
                {
                    networkObject.Despawn();
                }
            }

            NetworkObject.Despawn();
        }
    }
}
