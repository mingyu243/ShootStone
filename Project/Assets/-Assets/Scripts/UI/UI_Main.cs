using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : MonoBehaviour
{
    [SerializeField] GameObject _readyGameObject;
    [SerializeField] GameObject _playGameObject;
    [SerializeField] GameObject _resultGameObject;

    public enum Panel
    {
        None,
        Ready,
        Play,
        Result
    }

    void Awake()
    {
        Managers.Game.OnGameStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Ready:
                Ready();
                break;
            case GameState.Play:
                Play();
                break;
            case GameState.Result:
                Result();
                break;
            default:
                break;
        }
    }

    void Ready()
    {
        ShowPanel(Panel.Ready);
    }

    void Play()
    {
        ShowPanel(Panel.Play);
    }

    void Result()
    {
        ShowPanel(Panel.Result);
    }

    void ShowPanel(Panel panel)
    {
        _readyGameObject.SetActive(false);
        _playGameObject.SetActive(false);
        _resultGameObject.SetActive(false);

        switch (panel)
        {
            case Panel.Ready:
                _readyGameObject.SetActive(true);
                break;
            case Panel.Play:
                _playGameObject.SetActive(true);
                break;
            case Panel.Result:
                _resultGameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
