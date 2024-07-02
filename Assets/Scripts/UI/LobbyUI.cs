using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private CreateLobbyUI createLobbyUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;


    private void Awake()
    {
        quickJoinButton.onClick.AddListener(KitchenGameLobby.Instance.QuickJoin);

        createLobbyButton.onClick.AddListener(() =>
            createLobbyUI.Show());

        mainMenuButton.onClick.AddListener(() => {
            KitchenGameLobby.Instance.LeaveLobby();
            Loader.LoadNetwork(Loader.Scene.MainMenu);
        });
        
        //joinCodeButton.onClick.AddListener(() => KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text));

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        KitchenGameLobby.Instance.OnLobbyListChanged += KitchenGameLobby_OnLobbyListChanged;

        UpdateLobbyList(new List<Lobby>());
    }

    private void KitchenGameLobby_OnLobbyListChanged(object sender, KitchenGameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in lobbyContainer)
        {
            if (child == lobbyTemplate) continue;

            Destroy(child);
        }

        foreach(Lobby lobby in lobbyList)
        {
            Transform newLobby = Instantiate(lobbyTemplate, lobbyContainer);
            newLobby.gameObject.SetActive(true);
            newLobby.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }
}
