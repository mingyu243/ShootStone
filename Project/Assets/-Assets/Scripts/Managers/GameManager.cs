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
    int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            
            if(BestScore < _score)
            {
                BestScore = _score;
            }
        }
    }
    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt("BEST_SCORE");
        }
        set
        {
            PlayerPrefs.SetInt("BEST_SCORE", value);
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
