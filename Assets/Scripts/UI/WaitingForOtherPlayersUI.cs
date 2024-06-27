using System;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    private void Start()
    {
        Hide();

        GameManager.Instance.OnLocalPlayerReady += GameManager_OnLocalPlayerReady;
    }

    private void GameManager_OnLocalPlayerReady(object sender, EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
