using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ConfiureStage : MonoBehaviour
{
    public int stageNum;
    Object stageButton;

    private void OnEnable()
    {
        stageButton = Resources.Load<Object>("Prefabs/UI/StageButton");
    }

    public void GeneStageButton()
    {
        Debug.Log(stageNum);
        GameObject curButton;
        foreach(Transform t in transform)
        {
            t.parent = null;
        }
        for (int i = 1; i <= stageNum; i++)
        {
            curButton = Instantiate(stageButton) as GameObject;
            curButton.transform.parent = transform;
            curButton.name = i.ToString();
            curButton.GetComponentInChildren<Text>().text = i.ToString();
        }
    }
}
