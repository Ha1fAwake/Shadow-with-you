using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneButton : MonoBehaviour
{
    int sceneNum;

    // Start is called before the first frame update
    void Start()
    {
        sceneNum = Convert.ToInt32(name);
    }
    
    public void EnterScene()
    {
        EventCenter.Broadcast<int>(MyEventType.enterStage, sceneNum);
    }
}
