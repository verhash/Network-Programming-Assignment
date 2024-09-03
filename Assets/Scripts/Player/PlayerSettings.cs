using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Collections;

/* public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private TextMeshPro playerName;

    NetworkVariable<NetworkString> networkPlayerName = new NetworkVariable<NetworkString>("Unknown");

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            networkPlayerName.Value = GameObject.Find("UIManager").GetComponent<UIManager>().nameInputField.text;
        }

        playerName.text = networkPlayerName.Value.ToString();
        networkPlayerName.OnValueChanged += NetworkPlayerName_OnValueChanged;
    }

    void NetworkPlayerName_OnValueChanged(NetworkString previousValue, NetworkString newValue)
    {
        playerName.text = newValue;
    } 
}

public struct NetworkString : INetworkSerializeByMemcpy
{
    private ForceNetworkSerializeByMemcpy<FixedString32Bytes> info;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer)
    where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }

    public override string ToString()
    {
        return info.Value.ToString();
    }

    public static implicit operator string(NetworkString s) => s.ToString();
    public static implicit operator NetworkString(string s) => new NetworkString() { info = new FixedString32Bytes(s) };
}
*/