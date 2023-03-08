using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCube : MonoBehaviour
{
    [SerializeField] protected Transform _object;
    [SerializeField] protected MeshRenderer _meshRenderer;
    protected Material _material;

    [Header("값")]
    [SerializeField] protected bool _isTouching;
    [SerializeField] protected Color _normalColor;
    [SerializeField] protected Color _triggerColor;
    public FloorCubeType Type;

    [SerializeField] FloorGenerator floorGenerator;

    Material Material
    {
        get
        {
            if(_material == null)
            {
                _material = _meshRenderer.material;
            }
            return _material;
        }
    }

    private void Start()
    {
        floorGenerator = GetComponentInParent<FloorGenerator>();
    }
    
    public void Init()
    {
        _isTouching = false;
        //Material.SetColor("_BaseColor", _normalColor);
    }

    public virtual void TriggerEnter()
    {
        _isTouching = true;
        //Material.SetColor("_BaseColor", _triggerColor);

        floorGenerator.TriggerEnter(this);
    }
    public virtual void TriggerExit()
    {
        _isTouching = false;
        //Material.SetColor("_BaseColor", _normalColor);

        floorGenerator.TriggerExit(this);
    }

    public virtual void ReactEffect()
    {
    }

    public bool IsTouching => _isTouching;

    public virtual IEnumerator Show()
    {
        yield return Move(new Vector3(0, -1.0f, 0), new Vector3(0, -0.5f, 0));
    }

    //public virtual IEnumerator Hide()
    //{
    //    yield return Move(new Vector3(0, 0.5f, 0), new Vector3(0, -1.0f, 0));
    //}

    IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            _object.localPosition = Vector3.Lerp(startPos, endPos, t / 0.3f);
            yield return null;
        }
        _object.localPosition = endPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerExit();
    }
}
