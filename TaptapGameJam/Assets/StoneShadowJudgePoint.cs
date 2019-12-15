using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShadowJudgePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Stone")
        {
            GamePlayManager.Instance.stoneShadowIsReady = true;
        }
    }
}
