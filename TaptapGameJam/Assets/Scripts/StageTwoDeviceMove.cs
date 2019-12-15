using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoDeviceMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GamePlayManager.Instance.ifDeviceMove)
        {
            transform.Translate(0.002f, 0, 0);
        }
    }
}
