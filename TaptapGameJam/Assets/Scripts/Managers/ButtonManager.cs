using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button[] btnArr;
    // Start is called before the first frame update
    void Start()
    {
        btnArr = GetComponentsInChildren<Button>(true);

        for(int i=0;i<btnArr.Length;i++)
        {
            btnArr[i].onClick.AddListener(() => { HandlerNotification(btnArr[i].transform); });
        }
    }

    private void HandlerNotification(Transform targetBtn)
    {
        switch(targetBtn.name)
        {
            case "startbtn":
                EventCenter.Broadcast<int>(MyEventType.enterStage,0);
                break;
            case "optionbtn":
                break;
            case "exitbtn":

                break;
            case "pausebtn":

                break;
            case "mainmenubtn":

                break;
            case "restartbtn":

                break;
            case "exitoption":

                break;
            default:
                Debug.LogError("不存在的按钮名称");
                break;
        }
    }
    
}
