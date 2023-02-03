using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Charger : MonoBehaviour
{
    [SerializeField] bool _isCharging;
    [SerializeField] float _chargeSpeed;
    [SerializeField] float _value;
    [SerializeField] bool _dirUpFlag;

    public float Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }

    public bool IsCharging => _isCharging;

    public event Action<float> OnValueChanged;

    public void Play()
    {
        Value = 0;
        _dirUpFlag = true;
        _isCharging = true;

        StartCoroutine(Charging());

        IEnumerator Charging()
        {
            while (IsCharging)
            {
                float newValue = _value;
                if (_dirUpFlag)
                {
                    newValue += Time.deltaTime * _chargeSpeed;
                }
                else
                {
                    newValue -= Time.deltaTime * _chargeSpeed;
                }

                if (newValue >= 1 || newValue <= 0)
                {
                    newValue = Mathf.Clamp(newValue, 0, 1);
                    _dirUpFlag = !_dirUpFlag;
                }

                Value = newValue;

                yield return null;
            }
        }
    }

    public void Stop()
    {
        _isCharging = false;
    }
}
