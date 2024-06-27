using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private OptionsUI optionsUI;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenu);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += LocalGameManager_OnLocalGamePaused;
        GameManager.Instance.OnLocalGameUnpaused += LocalGameManager_OnLocalGameUnpaused;
        
        optionsButton.onClick.AddListener(OnOptionsMenuButton);
        
        Hide();
    }

    private void OnOptionsMenuButton()
    {
        optionsUI.Show();
    }

    private void LocalGameManager_OnLocalGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void LocalGameManager_OnLocalGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
