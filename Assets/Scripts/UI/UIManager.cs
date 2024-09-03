using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button joinButton;
    [SerializeField] public TMP_InputField nameInputField;

    private void Start()
    {
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        joinButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
