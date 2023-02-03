using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnTouchDown;
    public event Action OnTouchUp;

    [SerializeField] bool _isTouching = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isTouching)
        {
            OnTouchDown?.Invoke();
            _isTouching = true;
            print("OnTouchDown");
        }
        else if (Input.GetMouseButtonUp(0) && _isTouching)
        {
            OnTouchUp?.Invoke();
            _isTouching = false;
            print("OnTouchUp");
        }
    }
}
