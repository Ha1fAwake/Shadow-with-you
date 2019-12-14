using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDash : MonoBehaviour
{
    //可调整参数
    float moveSpeed = 6, dashSpeed = 9f, minTimeBeforeDash = 0.5f;
    float dashMinDis = 40f;

    //LaunchPad相关
    bool isOnPad = true;
    float disToPad;
    Transform launchPad;

    //冲刺相关变量
    bool dashTimerStart = false;
    int dashCounter = 999;
    float dashTimer = 0;

    //输入相关
    float horizontalValue, verticalValue;
    Vector3 mouseDownPos, mouseUpPos, lastMousePos, curMousePos, deltaMousePos;
    Vector3 dashFingerShift;
    Vector3 dashStartMousePos;

    Vector3 preVelocity = Vector3.zero;
    Coroutine dashCoroutine;
    
    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        TimersCountDown();
        JudgePlayerDash();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 curDir = transform.TransformDirection(Vector2.up);
        Vector2 newDir = Vector2.Reflect(curDir, normal).normalized;

        Quaternion rotation = Quaternion.FromToRotation(Vector2.up, newDir);
        transform.rotation = rotation;

        BrickBase b = collision.transform.GetComponent<BrickBase>();
        float rebounderValue = 1;
        if (b != null)
        {
            rebounderValue = b.reboundValue;
            rig.velocity = newDir * preVelocity.magnitude * rebounderValue;
        }
        else
        {
            rig.velocity = newDir * preVelocity.magnitude * 1;
            return;
        }

        b.GetCollided();
        switch (b.brickType)
        {
            case BrickType.standard:
                break;
            case BrickType.wall:
                break;
            case BrickType.exit:
                rig.velocity = Vector3.zero;
                break;
            case BrickType.spike:
                break;
            case BrickType.launchpad:
                isOnPad = true;
                rig.constraints = RigidbodyConstraints2D.FreezeAll;
                transform.parent = collision.transform;
                break;
        }
    }

    /// <summary>
    /// 跟随发射板进行移动
    /// </summary>
    protected void MoveWithLaunchPad()
    {

    }

    /// <summary>
    /// 更新某些需要更新的值
    /// </summary>
    private void UpdateValues()
    {
        preVelocity = rig.velocity;
    }

    /// <summary>
    /// 根据手指的抬起放下滑动判断是否进行冲刺
    /// </summary>
    private void JudgePlayerDash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("1");
            mouseDownPos = Input.mousePosition;
            lastMousePos = mouseDownPos;
        }
        if (Input.GetMouseButton(0))
        {
            // Get Drag Information
            if (curMousePos != Vector3.one)
            {
                lastMousePos = curMousePos;
            }
            curMousePos = Input.mousePosition;
            deltaMousePos = curMousePos - lastMousePos;

            // Deal With Drag Information
            if (deltaMousePos.magnitude > dashMinDis)//冲刺手势
            {
                //Debug.Log("2");
                dashStartMousePos = lastMousePos;
                dashTimerStart = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("3");
            dashFingerShift = Input.mousePosition - mouseDownPos;
            curMousePos = Vector3.one;
            if (dashTimerStart && dashCounter > 0)
            {
                dashCounter--;
                if(isOnPad)
                {
                    isOnPad = false;
                    rig.constraints = RigidbodyConstraints2D.FreezeRotation;
                    transform.parent = null;
                }
                EventCenter.Broadcast(MyEventType.PlayerDash);
                
                //真正开始冲刺
                StartDash(dashFingerShift.normalized, dashFingerShift.magnitude);
            }
        }
    }

    /// <summary>
    /// 判定冲刺是否取消的计时器
    /// </summary>
    private void TimersCountDown()
    {
        if (dashTimerStart)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer > minTimeBeforeDash)
            {
                dashTimer = 0;
                dashTimerStart = false;
            }
        }
    }

    /// <summary>
    /// 开始冲刺的协程
    /// </summary>
    void StartDash(Vector3 dir,float length)
    {
        if(dashCoroutine != null)
            StopCoroutine(dashCoroutine);

        //重置冲刺触发计时器
        dashTimer = 0;
        dashTimerStart = false;

        //转向冲刺方向
        transform.rotation = Quaternion.Euler(0,0,Global.Vector3ToAngle(dir));

        //进行冲刺
        length = Mathf.Clamp(length, 300, 600);
        IEnumerator i = PigDash(dir, length);
        dashCoroutine = StartCoroutine(i);
    }

    /// <summary>
    /// 冲刺协程
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    IEnumerator PigDash(Vector3 dir, float length)
    {
        rig.velocity = dir * dashSpeed;
        yield return new WaitForSeconds(length * 0.00005f);
        transform.localScale = new Vector3(0.8f, 1, 1);
        yield return new WaitForSeconds(length * 0.00005f);
        transform.localScale = new Vector3(0.6f, 1, 1);
        yield return new WaitForSeconds(length * 0.00005f);
        transform.localScale = new Vector3(0.8f, 1, 1);
        yield return new WaitForSeconds(length * 0.00025f);
        transform.localScale = Vector3.one;
        rig.velocity = rig.velocity.normalized * moveSpeed;
        //rig.velocity = dir * moveSpeed;
        //yield return new WaitForSeconds(length * 0.0003f);
    }


}
