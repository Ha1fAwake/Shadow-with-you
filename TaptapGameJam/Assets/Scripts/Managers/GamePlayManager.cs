﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : TMonoSingleton<GamePlayManager>, IInitializable
{
    public bool isControllingShadow = false;
    public bool isSpiningStage = false;
    public bool isUsingTorch = false;
    public bool ifHaveMatch = false, ifHaveNormalKey = false, ifHaveTriKey = false, ifdDoorUnlocked = false, stoneShadowIsReady = false;
    public int stageSpinFrame = 15;
    public float curXScale = 1, curYScale = 1;

    public Rigidbody2D playerRig, shadowRig;
    public Transform reality, shadowWorld, torch,itemsInLight;

    public GameObject shadow, fire;
    public bool ifDeviceMove = false;
    public float deviceMoveSpeed = 0;
    public GameObject device1, device2;
    public GameObject steel, steel2;
    public GameObject stone, stone2;

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
            if(stoneShadowIsReady)
            {
                stone2.SetActive(true);
                stoneShadowIsReady = false;
            }
        }

        if(!isControllingShadow && isUsingTorch && isInGameScene)
        {
            curYScale = 35f / torch.localPosition.y;
            itemsInLight.localScale = new Vector3(curYScale, curYScale, 1);
            shadowRig.transform.localScale = new Vector3(curYScale * 0.05f, curYScale * 0.05f, 1);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        isControllingShadow = false;
        isUsingTorch = false;
        if(level != 0)
        {
            isInGameScene = true;
            LoadPlayerAndOtherThings();
        }
        if(level == 1)
        {
            shadow = GameObject.Find("NoShadow");
            fire = GameObject.Find("Fire");
            fire.SetActive(false);
        }
        if (level == 2)
        {
            device1 = GameObject.Find("Device");
            device2 = GameObject.Find("Device2");
        }
        if (level == 3)
        {
            steel = GameObject.Find("Steel");
            steel2 = GameObject.Find("Steel2");
        }
        if (level == 3)
        {
            stone = GameObject.Find("Stone2");
            stone.SetActive(false);
            stone2 = GameObject.Find("Stone");
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
