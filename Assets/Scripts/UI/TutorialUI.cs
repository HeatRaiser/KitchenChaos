using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
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
