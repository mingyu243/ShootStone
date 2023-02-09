using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovedObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Rigidbody _rb;
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
    public bool IsMoving => (_rb.velocity.sqrMagnitude > 0);

    public event Action OnPointerDownAction;
    public event Action OnPointerUpAction;
    public event Action OnDied;

    public void Init()
    {
        IsDie = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke();
    }

    public void Shoot(Vector3 power)
    {
        _rb.velocity = power;
    }

    private void Update()
    {
        if(IsDie)
        {
            return;
        }

        if(this.transform.position.y < -1f)
        {
            IsDie = true;
        }
    }
}
