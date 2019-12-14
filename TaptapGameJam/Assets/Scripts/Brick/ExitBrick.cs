using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBrick : BrickBase
{
    
    public override void GetCollided()
    {
        EventCenter.Broadcast<int>(MyEventType.enterStage, stageLevel);
    }
    
}
