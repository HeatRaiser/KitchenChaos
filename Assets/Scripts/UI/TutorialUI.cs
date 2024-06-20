using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    
    [SerializeField] private GameObject tutorialCanvas;
    private const string PLAYER_PREFS_TUTORIAL_AUTOSHOW = "TutorialAutoShow";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PLAYER_PREFS_TUTORIAL_AUTOSHOW))
        {
            Show();
            PlayerPrefs.SetInt(PLAYER_PREFS_TUTORIAL_AUTOSHOW, 0);
        }
        else
        {
            Hide(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += Hide;
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        
        UpdateVisual();
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
    }

    private void Hide(object sender, EventArgs e)
    {
        tutorialCanvas.SetActive(false);
    }

    public void Show()
    {
        tutorialCanvas.SetActive(true);
    }
}
