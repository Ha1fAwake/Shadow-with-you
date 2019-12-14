using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : TMonoSingleton<GamePlayManager>, IInitializable
{
    public bool isControllingShadow = false;
    public bool isSpiningStage = false;
    public int stageSpinFrame = 30;

    Rigidbody2D playerRig, shadowRig;

    public void Init()
    {
        playerRig = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        shadowRig = GameObject.Find("PlayerShadow").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isSpiningStage)
        {
            isControllingShadow = !isControllingShadow;
            playerRig.constraints = RigidbodyConstraints2D.FreezeAll;
            shadowRig.constraints = RigidbodyConstraints2D.FreezeAll;
            isSpiningStage = true;
            EventCenter.Broadcast(MyEventType.startControlShadow);
        }
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
        //StartCoroutine(StartAdjustShadowPos());

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
