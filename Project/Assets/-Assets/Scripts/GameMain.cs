using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerView _playerView;

    [Header("오브젝트")]
    [SerializeField] Obstacle _obstacle;
    [SerializeField] MovedObject _movedObject;
    [SerializeField] Charger _charger;

    [Header("값")]
    [SerializeField] bool _canTouch;
    [SerializeField] bool _isShoot;
    [SerializeField] float _shotPower;

    Coroutine _playCoroutine;

    public Charger Charger => _charger;

    private void Awake()
    {
        Managers.Game.OnGameStateChanged += OnGameStateChanged;

        _playerController.OnTouchDown += OnTouchDown;
        _playerController.OnTouchUp += OnTouchUp;

        _movedObject.OnDied += Stop;
    }

    void OnTouchDown()
    {
        if (Managers.Game.GameState == GameState.Ready)
        {
            Managers.Game.GameState = GameState.Play;
            return;
        }

        if (!_canTouch)
        {
            return;
        }

        _charger.Play();
    }

    void OnTouchUp()
    {
        if (!_charger.IsCharging)
        {
            return;
        }

        _charger.Stop();
        _movedObject.Shoot(_charger.Value * _shotPower);
        _isShoot = true;
    }

    private void Start()
    {
        Managers.Game.GameState = GameState.Ready;
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
                break;
            default:
                break;
        }
    }

    void Ready()
    {
    }

    void Play()
    {
        _playCoroutine = StartCoroutine(Loop());

        IEnumerator Loop()
        {
            while (true)
            {
                yield return _obstacle.Hide();

                // 장애물 생성.
                _obstacle.transform.position = _movedObject.transform.position + Vector3.forward * UnityEngine.Random.Range(5, 20);
                yield return _obstacle.Show();

                // 장전하고 쏘기.
                _canTouch = true;
                yield return new WaitUntil(() => _isShoot);
                _isShoot = false;
                _canTouch = false;

                // 날아감.
                yield return new WaitUntil(() => _movedObject.Rb.velocity.sqrMagnitude <= 0);

                // 카메라 따라가기.
                yield return _playerView.Move(_movedObject.transform);
            }
        }
    }

    void Stop()
    {
        if (_playCoroutine != null)
        {
            StopCoroutine(_playCoroutine);
            _playCoroutine = null;
        }

        Managers.Game.GameState = GameState.Result;
    }
}
