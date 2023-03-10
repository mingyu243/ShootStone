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

    [Header("Sub Score")]
    [SerializeField] Animator _subScoreAnimator;
    [SerializeField] TextMeshProUGUI _subScoreText;

    int _score;

    public void Init()
    {
        _score = 0;
        _scoreText.text = string.Empty;
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void AddSubScore(int subScore)
    {
        _subScoreText.text = $"+{subScore}";
        _subScoreAnimator.SetTrigger("SHOW");

        StartCoroutine(Do());

        IEnumerator Do()
        {
            yield return new WaitForSeconds(0.8f);
            AddScore(subScore);
        }
    }
}
