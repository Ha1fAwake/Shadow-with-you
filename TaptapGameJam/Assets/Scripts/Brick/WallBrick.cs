using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBrick : BrickBase
{
    private void Start()
    {
        reboundValue = 1;
    }

    public override void GetCollided()
    {

    }
}
