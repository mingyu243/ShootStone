using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFloorCube : FloorCube
{
    [SerializeField] ParticleSystem _fxFlash;

    public void Awake()
    {
        Type = FloorCubeType.Normal;
    }

    public override void ReactEffect()
    {
        base.ReactEffect();

        _fxFlash.Play();
    }
}
