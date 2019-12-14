using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class KillManager : TMonoSingleton<KillManager>, IInitializable
{
    int curKillNum = 0;
    float freshTime;
    float freshTimer = 0;
    
    public void Init()
    {
        EventCenter.AddListener(MyEventType.MultiKillEnd, OnMultiKillEnd);
    }

    public void GetKill(int type,int point = 1)
    {
        curKillNum++;
        //TODO:Change MultiKill fresh interval

        EventCenter.Broadcast<int>(MyEventType.MultiKill, curKillNum);
        EventCenter.Broadcast<int>(MyEventType.BrickDestroyed, point);
    }

    public void OnMultiKillEnd()
    {
        curKillNum = 0;
    }
    
}
