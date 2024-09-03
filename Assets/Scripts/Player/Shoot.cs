using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : NetworkBehaviour
{ 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDelay;

    private bool rapidFire;
    private bool fireSingle;
    private float lastFire;

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBulletServerRPC(firePoint.position, firePoint.rotation);
        }
    }

    [ServerRpc]

    private void SpawnBulletServerRPC(Vector2 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<NetworkObject>().Spawn();

        rb.velocity = bulletSpeed * transform.up;
    }
}
