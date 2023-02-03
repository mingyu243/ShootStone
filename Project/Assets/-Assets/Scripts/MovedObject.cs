using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedObject : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] bool _isDie;

    public Rigidbody Rb => _rb;
    public bool IsDie
    {
        get => _isDie;
        set
        {
            _isDie = value;
            if (_isDie) 
            {
                OnDied?.Invoke();
            }
        }
    }

    public event Action OnDied;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        IsDie = false;
    }

    public void Shoot(float power)
    {
        _rb.velocity = Vector3.forward * power;
    }
}
