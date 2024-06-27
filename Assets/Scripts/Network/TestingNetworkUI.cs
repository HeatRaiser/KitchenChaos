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
        clientButton.onClick.AddListener(StartClient);
    }

    private void StartClient()
    {
        KitchenGameMultiplayer.Instance.StartHost();
        
        Hide();
    }

    private void StartHost()
    {
        KitchenGameMultiplayer.Instance.StartClient();
        
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
