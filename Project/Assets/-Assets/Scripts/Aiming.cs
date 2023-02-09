using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Aiming : MonoBehaviour
{
    [SerializeField] GameObject _stone;

    [Header("오브젝트")]
    [SerializeField] GameObject _line;
    [SerializeField] GameObject _aimRaycastArea;

    [Header("상태")]
    [SerializeField] bool _canAiming;
    [SerializeField] bool _isAiming;
    [SerializeField] bool _isReady;

    public bool IsReady => _isReady; // 쏠 준비 완료되면 true.
    public bool IsAiming => _isAiming;
    public bool CanAiming => _canAiming;
    public Vector3 Value => _value;

    [Header("값")]
    [SerializeField] Vector3 _value;
    [SerializeField] float _minDistance;
    [SerializeField] float _maxDistance;
    [SerializeField] float _curDistance;
    Vector3 _curDir;

    private void Awake()
    {
        _aimRaycastArea.SetActive(false);
        _line.transform.localScale = new Vector3(0, 0, 0);
    }

    // IDragHandler의 OnDrag는 처음에 일정 거리를 이동하기 전에는 호출이 안돼서, 호출이 되는 순간 거리가 어느정도 있기 때문에 팍 튀는 것처럼 보임.
    // 그래서 Update로 바꿈.
    void Update()
    {
        if (!_canAiming || !_isAiming)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999, 1 << LayerMask.NameToLayer("AimRaycast")))
        {
            Vector3 dir = hit.point - _stone.transform.position;
            dir.y = 0;

            // 회전.
            float dot = Vector3.Dot(Vector3.back, dir.normalized);
            float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;
            if (theta <= 70.0f) // 회전 각 제한.
            {
                _curDir = -dir;
                _curDir.Normalize();
                _stone.transform.LookAt(_curDir + _stone.transform.position);
                this.transform.LookAt(_curDir + this.transform.position);
            }

            // 거리에 비례하여 값.
            _curDistance = dir.magnitude;
            if (_curDistance > _maxDistance) // 최대 값 제한.
            {
                _curDistance = _maxDistance;
            }
            float ratio = _curDistance / _maxDistance;
            _line.transform.localScale = new Vector3(1, 1, ratio);

            _value = _curDir * ratio;
        }
    }

    public void OnPointerDownAction()
    {
        if (!_canAiming || _isAiming)
        {
            return;
        }

        this.transform.position = _stone.transform.position;

        _aimRaycastArea.SetActive(true);
        _isAiming = true;
    }

    public void OnPointerUpAction()
    {
        if (!_canAiming || !_isAiming)
        {
            return;
        }

        _aimRaycastArea.SetActive(false);
        _line.transform.localScale = new Vector3(0, 0, 0);
        _isAiming = false;

        // 쏠 준비가 됐는지 검사.
        if (_curDistance > _minDistance) // 너무 작으면 쏘지 않음.
        {
            _isReady = true;
        }
    }

    public void SetOn(bool isOn)
    {
        if (isOn)
        {
            _isReady = false;
            _isAiming = false;
            _value = Vector3.zero;

            _canAiming = true;
        }
        else
        {
            _canAiming = false;
        }
    }
}
