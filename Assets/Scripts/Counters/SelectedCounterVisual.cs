
using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisuals;
    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChange += OnSelectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
        
    }

    private void Player_OnAnyPlayerSpawned(object sender, EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChange -= OnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChange += OnSelectedCounterChanged;
        }
    }

    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        foreach (GameObject selectedVisual in selectedCounterVisuals)
        {
            selectedVisual.SetActive(baseCounter == e.selectedCounter);
        }
    }
}
