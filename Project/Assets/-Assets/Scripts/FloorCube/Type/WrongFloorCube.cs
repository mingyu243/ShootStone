using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongFloorCube : FloorCube
{
    public void Awake()
    {
        Type = FloorCubeType.Wrong;
    }
}
