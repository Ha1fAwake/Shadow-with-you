using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    Text text;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = num.ToString();
        EventCenter.AddListener<int>(MyEventType.BrickDestroyed, UpdatePoint);
        EventCenter.AddListener(MyEventType.GameStart, ResetPoint);
    }
    
    void UpdatePoint(int point)
    {
        num+=point;
        text.text = num.ToString();
    }

    void ResetPoint()
    {
        num = 0;
        text.text = num.ToString();
    }
}
