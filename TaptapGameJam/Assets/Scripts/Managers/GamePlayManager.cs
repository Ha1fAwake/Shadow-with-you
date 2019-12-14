﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : TMonoSingleton<GamePlayManager>, IInitializable
{
    public bool isControllingShadow = false;
    public bool isSpiningStage = false;
    public bool isUsingTorch = false;
    public int stageSpinFrame = 30;
    public float curXScale = 1, curYScale = 1;

    public Rigidbody2D playerRig, shadowRig;
    public Transform reality, shadowWorld, torch,itemsInLight;

    bool isInGameScene = false;

    public void Init()
    {

    }

    private void Update()
    {
        //旋转关卡，在影子和爱丽丝之间切换
        if(Input.GetKeyDown(KeyCode.Tab) && !isSpiningStage && isInGameScene)
        {
            isControllingShadow = !isControllingShadow;
            playerRig.constraints = RigidbodyConstraints2D.FreezeAll;
            shadowRig.constraints = RigidbodyConstraints2D.FreezeAll;
            isSpiningStage = true;
            EventCenter.Broadcast(MyEventType.startControlShadow);
        }

        if(!isControllingShadow && isUsingTorch && isInGameScene)
        {
            curYScale = 25f / torch.localPosition.y;
            foreach(Transform t in itemsInLight)
            {
                t.localScale = new Vector3(curYScale, curYScale, 1);
            }
            //itemsInLight.localScale = new Vector3(curYScale, curYScale, 1);
            shadowRig.transform.localScale = new Vector3(curYScale * 0.05f, curYScale * 0.05f, 1);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level != 0)
        {
            isInGameScene = true;
            LoadPlayerAndOtherThings();
        }
    }

    void LoadPlayerAndOtherThings()
    {
        reality = GameObject.Find("Reality").transform;
        shadowWorld = GameObject.Find("ShadowWorld").transform;
        playerRig = GameObject.Find(reality.name + "/Player").GetComponent<Rigidbody2D>();
        shadowRig = GameObject.Find("PlayerShadow").GetComponent<Rigidbody2D>();
        itemsInLight = GameObject.Find(shadowWorld.name + "/ItemsInLight").transform;
        torch = GameObject.Find(playerRig.transform.name + "/Torch").transform;
    }

    /// <summary>
    /// 旋转到一半时调用
    /// </summary>
    public void AdjustGravityScale()
    {
        //调整重力大小
        playerRig.gravityScale = 1 - playerRig.gravityScale;
        shadowRig.gravityScale = 1 - shadowRig.gravityScale;

        //调整影子的位置
        float targetX = playerRig.transform.position.x;
        Vector3 tempPos = shadowRig.transform.position;
        shadowRig.transform.position = new Vector3(targetX, tempPos.y, tempPos.z);

    }

    /// <summary>
    /// 关卡旋转结束时调用
    /// </summary>
    public void EndSpinStage()
    {
        isSpiningStage = false;
        playerRig.constraints = RigidbodyConstraints2D.FreezeRotation;
        shadowRig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator StartAdjustShadowPos()
    {
        float radio = 0;
        float targetX = playerRig.transform.position.x;
        float curX = shadowRig.transform.position.x;
        Vector3 tempPos = shadowRig.transform.position;
        for (int i = 0; i < stageSpinFrame; i++)
        {
            radio = i / stageSpinFrame;
            shadowRig.transform.position = new Vector3(Mathf.Lerp(curX, targetX, radio), tempPos.y, tempPos.z);
            yield return new WaitForEndOfFrame();
        }
        
    }
}