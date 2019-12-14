using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownText : MonoBehaviour
{
    bool isCountingDown = false, isAccumulating = false;
    float countDownTimer = 0,accumulateTimer = 0;
    Text text; 

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        EventCenter.AddListener<int>(MyEventType.startCountDown, StartCountDown);
        EventCenter.AddListener(MyEventType.startAccumulateTime,StartAccumulate);
        EventCenter.AddListener(MyEventType.PlayerDie, StopTimer);
        EventCenter.Broadcast(MyEventType.startAccumulateTime);
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        AccumulateTime();
    }

    /// <summary>
    /// 进行时间的累加
    /// </summary>
    private void AccumulateTime()
    {
        if(isAccumulating)
        {
            accumulateTimer += Time.deltaTime;
            text.text = Mathf.RoundToInt(accumulateTimer).ToString();
        }
    }

    /// <summary>
    /// 开始累加时间
    /// </summary>
    /// <param name="seconds"></param>
    void StartAccumulate()
    {
        isAccumulating = true;
        accumulateTimer = 0;
    }

    /// <summary>
    /// 进行倒计时
    /// </summary>
    private void CountDown()
    {
        if(isCountingDown)
        {
            if(countDownTimer > 0)
            {
                countDownTimer -= Time.deltaTime;
                text.text = Mathf.RoundToInt(countDownTimer).ToString();
            }
            else
            {
                isCountingDown = false;
                EventCenter.Broadcast(MyEventType.TimeOver);
            }
        }
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    /// <param name="seconds"></param>
    void StartCountDown(int seconds)
    {
        isCountingDown = true;
        countDownTimer = seconds;
    }

    void StopTimer()
    {
        isCountingDown = false;
        isAccumulating = false;
    }
}
