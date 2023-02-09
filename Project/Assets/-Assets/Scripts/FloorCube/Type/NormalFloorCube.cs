using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFloorCube : FloorCube
{
    public void Awake()
    {
        Type = FloorCubeType.Normal;
    }
}
