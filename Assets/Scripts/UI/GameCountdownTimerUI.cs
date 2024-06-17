using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownTimerText;

    private const float countdownToStart = 3f;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        countdownTimerText.gameObject.SetActive(true);
        StartCoroutine(StartCounting());
    }

    private void Hide()
    {
        countdownTimerText.gameObject.SetActive(false);
        StopCoroutine(StartCounting());
    }

    IEnumerator StartCounting()
    {
        float maxTime = countdownToStart;
        countdownTimerText.text = maxTime.ToString();

        do
        {
            yield return new WaitForSeconds(1f);

            maxTime -= 1f;
            countdownTimerText.text = maxTime.ToString();
        } 
        while (maxTime > 0f);
        
    }
}
