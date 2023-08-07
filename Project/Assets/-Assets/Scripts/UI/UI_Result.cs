using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Result : MonoBehaviour
{
    Animator _animator;

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _bestText;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _scoreText.text = Managers.Game.PlayerState.Score.ToString();
        _bestText.text = Managers.Game.PlayerState.BestScore.ToString();

        _animator.SetTrigger("SHOW");
    }

    public void OnClickMenuButton() // Bind Button Event.
    {
        Managers.Game.GameState = GameState.Ready;
    }

    public void OnClickRestartButton() // Bind Button Event.
    {
        Managers.Game.GameState = GameState.Ready;
        Managers.Game.GameState = GameState.Play;
    }
}
