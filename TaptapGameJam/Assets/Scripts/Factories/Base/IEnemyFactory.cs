using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum BrickType
{
    standard,
    wall,
    exit,
    spike,
    launchpad
}

public interface IBrickFactory
{
    BrickBase CreateBrick(int type, Vector3 pos, Vector3 rotation, Vector2 scale);
}