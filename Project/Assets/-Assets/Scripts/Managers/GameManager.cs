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
