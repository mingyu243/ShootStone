using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PlayHighScoreMode : MonoBehaviour
{
    [SerializeField] GameHighScoreMode _gameMain;

    [Header("Score")]
    [SerializeField] Animator _scoreAnimator;
    [SerializeField] TextMeshProUGUI _scoreText;

    [Header("Distance")]
    [SerializeField] Animator _distAnimator;
    [SerializeField] TextMeshProUGUI _distText;

    public void SetScore(int score)
    {
        _scoreAnimator.SetTrigger("Focus");
        _scoreText.text = score.ToString();
    }

    public void SetDistance(int dist)
    {
        _distAnimator.SetTrigger("Focus");
        _distText.text = dist.ToString();
    }
}
