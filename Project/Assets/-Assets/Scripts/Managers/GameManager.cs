using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Ready,
    Play,
    Result
}

public class PlayerState
{
    public int Score;
    public int BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BEST_SCORE") == false)
            {
                PlayerPrefs.SetInt("BEST_SCORE", 0);
            }

            return PlayerPrefs.GetInt("BEST_SCORE");
        }
        set
        {
            if (BestScore < value)
            {
                PlayerPrefs.SetInt("BEST_SCORE", value);
            }
        }
    }
}

public class GameManager
{
    GameState _gameState = GameState.None;
    public GameState GameState 
    { 
        get => _gameState;
        set
        {
            if (_gameState == value)
            {
                return;
            }
            _gameState = value;
            OnGameStateChanged?.Invoke(_gameState);
        }
    }

    PlayerState _playerState = new PlayerState();
    public PlayerState PlayerState
    {
        get => _playerState;
        set => _playerState = value;
    }

    public event Action<GameState> OnGameStateChanged;
}
