using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRebindingController : MonoBehaviour
{
    // [SerializeField] private Button MoveUp;
    // [SerializeField] private Button MoveDown;
    // [SerializeField] private Button MoveLeft;
    // [SerializeField] private Button MoveRight;
    // [SerializeField] private Button Interact;
    // [SerializeField] private Button InteractAlternate;
    // [SerializeField] private Button Pause;
    
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    // [SerializeField] private TextMeshProUGUI InteractText;
    // [SerializeField] private TextMeshProUGUI InteractAlternateText;
    // [SerializeField] private TextMeshProUGUI PauseText;

    private void OnEnable()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
    }
}
