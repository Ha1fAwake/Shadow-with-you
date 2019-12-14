using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(MyEventType.startControlShadow,OverTurnTheStage);
    }
    

    void OverTurnTheStage()
    {
        IEnumerator i = StartOverTurn();
        StartCoroutine(i);
    }

    IEnumerator StartOverTurn()
    {
        int spinFrame = GamePlayManager.Instance.stageSpinFrame;
        float anglePerFrame = 180 / spinFrame;
        for (int i = 0; i < spinFrame / 2; i++)
        {
            transform.Rotate(anglePerFrame, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        GamePlayManager.Instance.AdjustGravityScale();
        for (int i = 0; i < spinFrame/2; i++)
        {
            transform.Rotate(anglePerFrame, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        GamePlayManager.Instance.EndSpinStage();
    }
}
