using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectFloorCube : FloorCube
{
    [SerializeField] ParticleSystem _fxFlash;

    public void Awake()
    {
        Type = FloorCubeType.Correct;
    }

    public override void ReactEffect()
    {
        base.ReactEffect();

        _fxFlash.Play();
    }
}