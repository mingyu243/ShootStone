using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Play,
    Result
}

public class GameManager
{
    GameState _gameState;
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

    public event Action<GameState> OnGameStateChanged;
}
