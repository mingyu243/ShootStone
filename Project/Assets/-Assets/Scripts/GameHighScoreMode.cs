using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHighScoreMode : MonoBehaviour
{
    [SerializeField] FloorGenerator _floorGenerator;

    [Header("UI")]
    [SerializeField] UI_PlayHighScoreMode _ui;

    [Header("오브젝트")]
    [SerializeField] MovedObject _movedObject;
    [SerializeField] Aiming _aiming;

    [Header("값")]
    [SerializeField] float _shotPower;
    [SerializeField] int _score;
    [SerializeField] int _distance;

    Coroutine _playCoroutine;
    HashSet<FloorCube> _triggeredFloorCubes = new HashSet<FloorCube>();

    public Aiming Aiming => _aiming;

    private void Awake()
    {
        Managers.Game.OnGameStateChanged += OnGameStateChanged;

        _movedObject.OnPointerDownAction += _aiming.OnPointerDownAction;
        _movedObject.OnPointerUpAction += _aiming.OnPointerUpAction;
        _movedObject.OnDied += Stop;

        _floorGenerator.OnTriggerEnterAction += OnTriggerEnterAction;
        _floorGenerator.OnTriggerExitAction += OnTriggerExitAction;
    }

    private void Start()
    {
        Managers.Game.GameState = GameState.Ready;
    }

    private void OnTriggerEnterAction(FloorCube floorCube)
    {
        if (!_movedObject.IsMoving || _movedObject.IsDie)
        {
            return;
        }

        bool isSuccess = _triggeredFloorCubes.Add(floorCube);
        if(!isSuccess)
        {
            return;
        }

        print("Enter " + floorCube.gameObject.name);

        switch (floorCube.Type)
        {
            case FloorCubeType.None:
                break;
            case FloorCubeType.Normal:
                AddScore(1);
                break;
            case FloorCubeType.Correct:
                AddScore(1);
                break;
            case FloorCubeType.Wrong:
                AddScore(1);
                break;
        }
    }
    private void OnTriggerExitAction(FloorCube floorCube)
    {
        if (!_movedObject.IsMoving || _movedObject.IsDie)
        {
            return;
        }

        print("Exit " + floorCube.gameObject.name);
        _triggeredFloorCubes.Remove(floorCube);
    }

    private void CheckJudge()
    {
        bool isDie = true;

        foreach (var floorCube in _triggeredFloorCubes)
        {
            switch (floorCube.Type)
            {
                case FloorCubeType.None:
                    break;
                case FloorCubeType.Normal:
                    isDie = false;
                    break;
                case FloorCubeType.Correct:
                    isDie = false;
                    AddScore(5);
                    break;
                case FloorCubeType.Wrong:
                    break;
            }
        }

        _movedObject.IsDie = isDie;
    }

    void AddScore(int addScore)
    {
        _score += addScore;
        UpdateScore(_score);
    }

    void UpdateScore(int score)
    {
        _score = score;
        _ui.SetScore(score);
    }

    void UpdateDistance(int dist)
    {
        _score = dist;
        _ui.SetDistance(dist);
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
        // 발판 원래대로.
        _floorGenerator.Init();
        _floorGenerator.Show();

        // 점수 초기화.
        UpdateScore(0);

        // 스톤 초기화.
        _movedObject.Init();
        _movedObject.transform.position = Vector3.up * 0.5f;
        _movedObject.transform.rotation = Quaternion.identity;
        _movedObject.Rb.isKinematic = true; // 공중에 있다가 떨어지는 느낌 줄려고.
    }

    void Play()
    {
        _playCoroutine = StartCoroutine(Loop());

        IEnumerator Loop()
        {
            _movedObject.Rb.isKinematic = false;
            yield return new WaitForSeconds(0.5f);

            float oldZ = 0;

            while (true)
            {
                // 장전하고 쏘기.
                _aiming.SetOn(true);
                yield return new WaitUntil(() => _aiming.IsReady);
                _movedObject.Shoot(_aiming.Value * _shotPower);
                _aiming.SetOn(false);

                // 날아감.
                yield return new WaitUntil(() => _movedObject.IsMoving == false);
                if (_movedObject.IsDie)
                {
                    break;
                }

                // 멈췄을 때 판정.
                CheckJudge();
                if (_movedObject.IsDie)
                {
                    break;
                }

                // 재배치.
                int newZ = Mathf.FloorToInt(_movedObject.transform.position.z - oldZ) - 1;
                _floorGenerator.Remake(newZ);
                oldZ += newZ;
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
