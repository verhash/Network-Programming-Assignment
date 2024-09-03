using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Wave : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        base.OnNetworkSpawn();

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

}
