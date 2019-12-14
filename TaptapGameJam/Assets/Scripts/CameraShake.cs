using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private float shakeLevel = 2f;// 震动幅度
    private float setShakeTime = 0.3f;   // 震动时间
    private float curShakeTime = 0;
    private float shakeFps = 60f;    // 震动的FPS
    private bool isShaking = false;// 震动标志

    private float shakeTime = 0.0f;
    private float fps;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    private Camera selfCamera;

    void Start()
    {
        selfCamera = gameObject.GetComponent<Camera>();
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.006f;

        EventCenter.AddListener(MyEventType.PlayerDash, StartShake);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (curShakeTime < setShakeTime)
            {
                curShakeTime += Time.deltaTime;
                if (curShakeTime >= setShakeTime)
                {
                    isShaking = false;
                    selfCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    curShakeTime = 0;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        selfCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value), shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }

    void StartShake()
    {
        isShaking = true;
        curShakeTime = 0;
    }
    
}