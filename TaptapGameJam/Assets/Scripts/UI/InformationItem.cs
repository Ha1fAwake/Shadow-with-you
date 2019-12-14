using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationItem : MonoBehaviour
{
    Text text;
    
    public void SetText(string content)
    {
        text = gameObject.transform.Find("Text").GetComponent<Text>();
        Debug.Log(content);
        text.text = content;
    }
}
