using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRebindingController : MonoBehaviour
{
    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAlternateButton;
    [SerializeField] private Button PauseButton;
    
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAlternateText;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        MoveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.MoveUp);
        });
        MoveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.MoveDown);
        });
        MoveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.MoveLeft);
        });
        MoveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.MoveRight);
        });
        InteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.Interact);
        });
        InteractAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.InteractAlternate);
        });
        PauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Bindings.Pause);
        });
    }

    private void Start()
    {
        HidePressToRebindKey();
    }

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
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Bindings binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBindings(binding,() =>
        {
            HidePressToRebindKey();
            UpdateVisuals();
        });
    }
}
