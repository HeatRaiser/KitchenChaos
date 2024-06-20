using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(StartHost);
        hostButton.onClick.AddListener(StartClient);
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        
        Hide();
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
