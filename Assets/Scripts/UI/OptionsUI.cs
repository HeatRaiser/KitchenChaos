using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private VolumeSlider volumeController;
    
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TutorialUI tutorialUI;

    private void Start()
    {
        closeButton.onClick.AddListener(Hide);
        howToPlayButton.onClick.AddListener(ShowTutorialUI);
        
        Hide();
    }

    private void ShowTutorialUI()
    {
        tutorialUI.Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Show()
    {
        volumeController.UpdateSliders();
        gameObject.SetActive(true);
    }
}
