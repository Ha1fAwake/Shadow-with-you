using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiKillSign : MonoBehaviour
{
    Text text;
    Image image;
    bool isCounting = false;
    float countDownSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        image = GetComponentInChildren<Image>();
        text.rectTransform.position = new Vector3(2333, 2333, 0);
        EventCenter.AddListener<int>(MyEventType.MultiKill, OnMultiKillAdd);
    }

    private void Update()
    {
        if(isCounting)
        {
            image.fillAmount -= countDownSpeed;
            if(image.fillAmount <= 0)
            {
                isCounting = false;
                text.rectTransform.position = new Vector3(2333, 2333, 0);
                EventCenter.Broadcast(MyEventType.MultiKillEnd);
            }
        }
    }

    void OnMultiKillAdd(int killNum)
    {
        if(killNum > 0)
        {
            text.rectTransform.localPosition = new Vector3(0, 0, 0);
            isCounting = true;
            text.text = killNum.ToString();
            image.fillAmount = 1;
        }
    }
}
