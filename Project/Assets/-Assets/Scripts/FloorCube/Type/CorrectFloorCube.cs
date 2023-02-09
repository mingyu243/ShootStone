using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectFloorCube : FloorCube
{
    public void Awake()
    {
        Type = FloorCubeType.Correct;
    }
}