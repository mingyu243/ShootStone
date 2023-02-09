using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorCubeType
{
    None,
    Normal,
    Correct,
    Wrong
}

public class FloorCubeControl : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] FloorCube _normal;
    [SerializeField] FloorCube _correct;
    [SerializeField] FloorCube _wrong;

    [Header("값")]
    [SerializeField] FloorCube _curFloorCube;
    public FloorCube CurFloorCube => _curFloorCube;

    private void Awake()
    {
        _normal.transform.position = Vector3.zero;
        _correct.transform.position = Vector3.zero;
        _wrong.transform.position = Vector3.zero;

        _normal.gameObject.SetActive(false);
        _correct.gameObject.SetActive(false);
        _wrong.gameObject.SetActive(false);
    }

    void SetType(FloorCubeType type)
    {
        switch (type)
        {
            case FloorCubeType.None:
                _curFloorCube = null;
                break;
            case FloorCubeType.Normal:
                _curFloorCube = _normal;
                break;
            case FloorCubeType.Correct:
                _curFloorCube = _correct;
                break;
            case FloorCubeType.Wrong:
                _curFloorCube = _wrong;
                break;
            default:
                break;
        }
    }

    public void Show(FloorCubeType type, float delayTime = 0)
    {
        // 이전 것 사라지기.
        _curFloorCube?.gameObject.SetActive(false);
        
        // 타입 바꾼 후 나타나기.
        SetType(type);
        _curFloorCube?.Init();
        StartCoroutine(DelayShow(delayTime));
    }
    IEnumerator DelayShow(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        _curFloorCube.gameObject.SetActive(true);
        StartCoroutine(_normal.Show());
    }
}
