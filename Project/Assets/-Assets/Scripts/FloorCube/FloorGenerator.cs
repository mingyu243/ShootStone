using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject _floorCubeControlPrefab;
    [SerializeField] GameObject _smoothPlane;

    [Header("값")]
    int _viewRowCount = 14; // 한번에 보여질 줄 개수.
    float _startX = -1.5f;
    float _gapX = 1.0f;

    [Header("스폰 확률")]
    [SerializeField] int _level;

    Queue<FloorCubeControl> _floorCubeControlQueue = new Queue<FloorCubeControl>();

    public event Action<FloorCube> OnTriggerEnterAction;
    public event Action<FloorCube> OnTriggerExitAction;

    public void TriggerEnter(FloorCube floorCube)
    {
        OnTriggerEnterAction?.Invoke(floorCube);
    }
    public void TriggerExit(FloorCube floorCube)
    {
        OnTriggerExitAction?.Invoke(floorCube);
    }

    void Create()
    {
        for (int i = 0; i < _viewRowCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject go = Instantiate(_floorCubeControlPrefab);
                go.transform.SetParent(this.transform);
                go.SetActive(false);

                _floorCubeControlQueue.Enqueue(go.GetComponent<FloorCubeControl>());
            }
        }
    }

    public void Init()
    {
        if(_floorCubeControlQueue.Count <= 0) // 처음에 만들어져있는 게 없으면 생성.
        {
            Create();
        }

        FloorCubeControl[] controls = _floorCubeControlQueue.ToArray();

        for (int i = 0; i < controls.Length; i++)
        {
            GameObject go = controls[i].gameObject;
            go.transform.position = new Vector3(_startX + (_gapX * i % 4), 0.0f, (i / 4) + 1);
        }
        _smoothPlane.transform.localScale = new Vector3(1, 1, _viewRowCount + 1);
        _smoothPlane.transform.position = new Vector3(0, 0, _viewRowCount / 2.0f);
    }

    public void SetLevel(float levelRatio)
    {
        _level = Mathf.RoundToInt(levelRatio * 100);
    }

    public void Show(int startIndex = 0)
    {
        FloorCubeControl[] controls = _floorCubeControlQueue.ToArray();

        for (int i = startIndex; i < controls.Length; i++)
        {
            controls[i].gameObject.name = i.ToString();
            controls[i].gameObject.SetActive(true);

            int random = UnityEngine.Random.Range(0, 100);
            FloorCubeType type;
            if (random >= _level)
            {
                type = FloorCubeType.Normal;
            }
            else
            {
                if(UnityEngine.Random.Range(0, 10) >= 9)
                {
                    type = FloorCubeType.Correct;
                }
                else
                {
                    type = FloorCubeType.Wrong;
                }
            }

            float delay = ((i - startIndex) / 4) * 0.07f;
            controls[i].Show(type, delay);
        }
    }

    public void Remake(int zCount)
    {
        int count = zCount * 4;

        for (int i = 0; i < count; i++)
        {
            FloorCubeControl floorCubeControl = _floorCubeControlQueue.Dequeue();

            floorCubeControl.transform.position += Vector3.forward * _viewRowCount;
            _floorCubeControlQueue.Enqueue(floorCubeControl);
        }

        if (zCount > 0)
        {
            _smoothPlane.transform.position += Vector3.forward * zCount;
        }

        Show((_viewRowCount - zCount) * 4); // 자신이 서있을 4줄은 남기고 맵을 새로 리셋함.
    }
}
