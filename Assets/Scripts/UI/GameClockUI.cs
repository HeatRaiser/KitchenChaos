using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImager;

    private void Update()
    {
        timerImager.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
